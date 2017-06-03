/*
 * ----------------------------------------------------------------------------
 *          file name : CBundleManager.cs
 *          desc      : 游戏内 AssetBundles 资源管理
 *          author    : LJP
 *          
 *          log       : [ 2015-05-04 ]
 * ----------------------------------------------------------------------------         
*/
using System;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




/// <summary>
/// 版本信息
/// </summary>
class VersionFile
{
    public string       Version;        // 本本号
    public bool         ForceUpdate;    // 是否强制更新
    public string       Type;           // 版本更型， 整包(package)还是自愿(res)
    public int          Size;           // 大小
    public string       Date;           // 日期
    public string       UpdateAddress;  // 更新地址，用于不支持更新机制的渠道
}


/// <summary>
/// 版本自愿信息
/// </summary>
class VersionResFile
{
    public string       mNumber;
    public bool         mBig;
    public Dictionary<string, string>   mPathMD5;
    public Dictionary<string, int>      mSize;

    public VersionResFile()
    {
        mPathMD5        = new Dictionary<string,string>();
        mSize           = new Dictionary<string,int>();
    }
}



public class CBundleManager : View
{

    /////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 版本更新相关
    /// </summary>
    /// /////////////////////////////////////////////////////////////////////////////////////
    private string          mCurVersion;                        // 当前版本
    private string          mCurIdentifier;                     // 当前版本的 Identifier
    private VersionFile     mVersionFile = new VersionFile();   // CDN服务器版本信息
    private VersionResFile  mLocalPathMD5;
    private VersionResFile  mServerPathMD5;
    private List<string>    mUpdateAssets;                     // 需要更新的文件列表
    private List<int>       mUpdateAssetSize;                  // 记录每个更新文件的大小
    private int             mAllNeedDownLoadSize;
    private int             mCurNeedDownLoadSize;
    private int             mAllNeedDownLoadBytes;
    private int             mCurNeedDownLoadBytes;
    private bool            mComplete;
    private bool            mForceTermination;
    private string          mCurDownLoadAsset;
    private int             mCurDownLoadAssetFailedTimes;
    private int             mMaxRepeatDownLoadTimes;

    private bool            mNetworkConnections;
    private bool            mNeedCheckNetWorkConnections;
    private float           mCheckNetWorkConnectionsMaxTimes;
    /// /////////////////////////////////////////////////////////////////////////////////////////
   



    private delegate void           HandleFinishDownload(WWW www);
    public static Dictionary<string, Asset> AssetCache = new Dictionary<string, Asset>();


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 根据AssetName 加载相应的资源，并创建对象
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    public GameObject CreateObjByBundle(string bundleName, int nResType, bool bAsynLoad = false )
    {

        if (string.IsNullOrEmpty(bundleName))
            return null;


        Asset bundle        = null;
        GameObject prefab   = null;
        bool bIsCache       = AssetCache.ContainsKey( bundleName );
        if ( bIsCache )
        {
            bundle          = AssetCache[bundleName];
        }
        else
        {
            bundle          = new Asset();
            bundle.type     = (Asset.TYPE)nResType;
            bundle.LoadBundle(bundleName, bAsynLoad);
        }

        if (bAsynLoad )
        {
            if (bundle.bundle != null)
            {
                prefab = bundle.bundle.mainAsset as GameObject;
            }
        }
        else
        {
            if( bundle.mainAsset != null )
            {
                prefab = UnityEngine.GameObject.Instantiate(bundle.mainAsset) as GameObject;
            }
        }


        return prefab;
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 从服务器上下载当前版本信息
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private IEnumerator downLoadCheckVersion(string url, HandleFinishDownload finishFun)
    {

        WWW www = new WWW(url);
        yield return www;


        /// 网络出问题
        if( !string.IsNullOrEmpty( www.error ) )
        {
            Common.DEBUG_MSG( www.error );
            www.Dispose();
        }
        else
        {
            /// 访问操作执行成功
            if (finishFun != null)
                finishFun(www);

            www.Dispose();
        }
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 更新完成处理
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private void OnUpdateComplete( )
    {
        AppDelegate pCompent = gameObject.AddComponent<AppDelegate>() as AppDelegate;
        mComplete            = true;
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 下载 Asset 自愿
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private void DownLoadNeedUpdateAsset( )
    {
        if( 0 == mUpdateAssets.Count )
        {
            StartCoroutine( DownLoadAsset( Common.getServerPath("Version.bytes"), delegate( WWW serverVersion)
            {
                OnUpdateComplete();
            }));
            return;
        }

        string LoadAsset = mUpdateAssets[0];
        mUpdateAssets.RemoveAt(0);
        mCurNeedDownLoadBytes += mUpdateAssetSize[0];
        mUpdateAssetSize.RemoveAt(0);


        StartCoroutine(DownLoadAsset(Common.getServerPath(LoadAsset), delegate(WWW w)
        {
            //将下载的资源替换为本地的资源
            ReplaceLocalRes(LoadAsset, w.bytes);
            DownLoadNeedUpdateAsset();
        }));
    }

    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 替换掉本地文件
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private void ReplaceLocalRes(string file, byte[] data)
    {

        string localPath;
        //Andrio跟IOS环境使用沙箱目录
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            localPath = string.Format("{0}/{1}", Application.persistentDataPath, file);
        }
        //Window下使用assetbunlde资源目录
        else
        {
            localPath = Common.assetbundleFilePath + file;
            Debug.LogError("Replace" + localPath);
        }

        Common.CheckFolder( Common.getPath(localPath));
        FileStream stream = new FileStream(localPath, FileMode.OpenOrCreate);

        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 下载 Asset 自愿
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private IEnumerator DownLoadAsset( string url, HandleFinishDownload finishFun )
    {

        if (mCurDownLoadAsset == url)
            ++mCurDownLoadAssetFailedTimes;
        else
        {
            mCurDownLoadAssetFailedTimes = 0;
            mCurDownLoadAsset = url;
        }

        if (mCurDownLoadAssetFailedTimes > mMaxRepeatDownLoadTimes)
        {
            mForceTermination = true;
            yield break;
        }


        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Common.DEBUG_MSG(www.error);
            www.Dispose();

            //检测网络连接
            yield return StartCoroutine(checkNetworkConnections());

            StartCoroutine(DownLoadAsset(url, finishFun));
        }
        else
        {
            if (finishFun != null)
            {
                finishFun(www);
            }
            www.Dispose();
        }
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 检查网络状态
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private IEnumerator checkNetworkConnections()
    {
        mNeedCheckNetWorkConnections = true;
        //检测网络
        string url = Common.getServerPath("checkNetworkConnections.bytes");
        float star = System.DateTime.Now.Second;
        while (true)
        {
            WWW www = new WWW( Common.checkUrl(ref url));
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (www.text == "true")
                {
                    mNeedCheckNetWorkConnections = false;
                    www.Dispose();
                    break;
                }
            }
            www.Dispose();
            float end = System.DateTime.Now.Second - star;
            if (end > mCheckNetWorkConnectionsMaxTimes)
                mNetworkConnections = false;
        }
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 更新游戏的版本
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    public void UpdateVersion()
    {
       
        mCurVersion     = "1.0.0";

        //获取当前版本Identifier
        mCurIdentifier  = "com.cmge.blzhyz";

        //版本检测文件地址
        string checkVersionAddress = "http://zhyzcdn.joygame.cn/version.xml";
        //加载版本Version.xml信息
        StartCoroutine(downLoadCheckVersion(checkVersionAddress, delegate(WWW serverVersion)
        {
            //解析版本信息
            parseVersionFile(serverVersion.text, mCurIdentifier, ref mVersionFile);
            if (mVersionFile.Version == null)
            {
                Debug.Log("解析版本错误, 更新版本错误");
                return;
            }
            
            //版本不相同，更新
            if (mVersionFile.Version != mCurVersion)
            {
                //如果是整包,并且渠道不支持更新，显示更新提示菜单
                if (mVersionFile.Type == "package" && mVersionFile.UpdateAddress != "")
                {
                    //隐藏信息窗口
                   
                }
                //如果是资源，直接更新
                else if (mVersionFile.Version == "resource")
                {
                    updateResource();
                }
            }
            //版本相同
            else
            {
                OnUpdateComplete();
            }
        }));
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 热更新，更新资源
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    public void updateResource()
    {
        mLocalPathMD5           = new VersionResFile();
        mServerPathMD5          = new VersionResFile();
        mUpdateAssets           = new List<string>();
        mUpdateAssetSize        = new List<int>();
        mAllNeedDownLoadSize    = 0;
        mCurNeedDownLoadSize    = 0;
        mAllNeedDownLoadBytes   = 0;
        mCurNeedDownLoadBytes   = 0;
        mComplete = false;

        mCurDownLoadAsset       = "";
        mCurDownLoadAssetFailedTimes = 0;
        mMaxRepeatDownLoadTimes = 5;
        mForceTermination       = false;

        mNetworkConnections                 = true;
        mNeedCheckNetWorkConnections        = false;
        mCheckNetWorkConnectionsMaxTimes    = 10;

        //1.加载本地资源version.xml信息
        StreamReader localReader = new StreamReader( Common.Open("Version"), System.Text.Encoding.Default);
        parseResourceVersionFile(localReader.ReadToEnd(), ref mLocalPathMD5);
        localReader.Close();

        //2.加载服务器version.xml信息
        StartCoroutine(DownLoadAsset(Common.getServerPath("Version.bytes"), delegate(WWW resourceVersion)
        {
            parseResourceVersionFile(resourceVersion.text, ref mServerPathMD5);
            if (needUpdate(mLocalPathMD5, mServerPathMD5, mUpdateAssets, mUpdateAssetSize))
            {
               
                mAllNeedDownLoadSize = mUpdateAssets.Count;
                //4.下载最新资源
                DownLoadNeedUpdateAsset();
            }
            else
            {
                mComplete = true;
                OnUpdateComplete();
                Debug.LogError("Complete update");
            }
        }));
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 解析版本资源信息
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private void parseResourceVersionFile(string text, ref VersionResFile file)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlElement root = doc.DocumentElement;
        file.mNumber = root.GetAttribute("Number");
        file.mBig = Convert.ToBoolean(root.GetAttribute("Big"));
        IEnumerator iter = root.GetEnumerator();
        while (iter.MoveNext())
        {
            XmlElement child = iter.Current as XmlElement;
            file.mPathMD5.Add(child.GetAttribute("fpath"), child.GetAttribute("md5"));
            file.mSize.Add(child.GetAttribute("fpath"), Convert.ToInt32(child.GetAttribute("size")));
        }

        Debug.LogError("paser version fil okk" + text);
    }

    
    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 解析版本信息, 并且适配渠道
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private void parseVersionFile(string text, string channel, ref VersionFile version)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlElement root = doc.DocumentElement;

        //获取最近的版本信息            
        IEnumerator sdkIter = root.GetEnumerator();
        while (sdkIter.MoveNext())
        {
            XmlElement sdkChild     = sdkIter.Current as XmlElement;
            string channelName      = sdkChild.GetAttribute("name");
            if (channelName == channel)
            {
                //版本更新地址，用于不支持更新的渠道
                string updateAddress = sdkChild.GetAttribute("updateAddress"); ;
                IEnumerator iter = sdkChild.GetEnumerator();
                if (iter.MoveNext())
                {
                    XmlElement child        = iter.Current as XmlElement;
                    version.Version         = child.GetAttribute("version");
                    version.ForceUpdate     = bool.Parse(child.GetAttribute("forceUpdate"));
                    version.Type            = child.GetAttribute("type");
                    version.Size            = int.Parse(child.GetAttribute("size"));
                    version.UpdateAddress   = updateAddress;
                    break;
                }
            }
        }
    }


    ///----------------------------------------------------------------------------------------------
    /// <summary>
    /// 判断是否需要更新
    /// </summary>
    ///----------------------------------------------------------------------------------------------
    private bool needUpdate(VersionResFile local, VersionResFile server, List<string> update, List<int> updateSize)
    {
        //后期采用先判定版本号
        foreach (KeyValuePair<string, string> file in server.mPathMD5)
        {
            if (local.mPathMD5.ContainsKey(file.Key))
            {
                if (local.mPathMD5[file.Key] != file.Value)
                {
                    update.Add(file.Key);
                    mAllNeedDownLoadBytes += server.mSize[file.Key];
                    updateSize.Add(server.mSize[file.Key]);
                }
            }
            else
            {
                update.Add(file.Key);
                mAllNeedDownLoadBytes += server.mSize[file.Key];
                updateSize.Add(server.mSize[file.Key]);
            }
        }

        //测试
        foreach (string str in update)
        {
            Debug.LogError("need update" + str);
        }
        return update.Count > 0 ? true : false;
    }
}


