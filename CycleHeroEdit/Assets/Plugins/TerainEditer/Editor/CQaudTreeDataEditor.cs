/*
 * ------------------------------------------------------------------------------
 * 
 *          desc    : 介于 Patchs 的四叉树数据格式
 *  
 * 
 * ------------------------------------------------------------------------------
*/
using System;
using System.Xml;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;





public class CQaudTreeDataEditor
{

    // 读取文件的句柄
    private List<GameObject>  _gamelist = new List<GameObject>();
    
    /// ----------------------------------------------------------------------------------
    /// <summary>
    /// 读取 pacth 对应的四叉树文件
    /// </summary>
    /// ----------------------------------------------------------------------------------
    public void OpenFromFile( string strFile )
    {
        if (string.IsNullOrEmpty(strFile))
            return;

        if (!File.Exists(strFile))
            return;
    }

    /// ----------------------------------------------------------------------------------
    /// <summary>
    /// 写四叉树文件的头
    /// </summary>
    /// ----------------------------------------------------------------------------------
    public void GenQuadTreeXML( string strPath )
    {
        int nNums = _gamelist.Count;
        if (nNums <= 0)
            return;
        XmlDocument xmlDoc  = new XmlDocument();
        XmlElement root     = xmlDoc.CreateElement("root");

        XmlElement qaudroot = xmlDoc.CreateElement("header");
        qaudroot.SetAttribute("name", "QuadNodes");
        qaudroot.SetAttribute("value", nNums.ToString());
        root.AppendChild( qaudroot );
        root                = qaudroot;

        XmlElement QaudNode = xmlDoc.CreateElement("QaudNode");
        QaudNode.SetAttribute("name", "QaudNode");
       
        GenQuadTreeNode2XML(xmlDoc, QaudNode);

        xmlDoc.Save( strPath );
    }

    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 把本 patch 的 GameObject 序列化到文件中
    /// </summary>
    /// --------------------------------------------------------------------------------
    private void GenQuadTreeNode2XML( XmlDocument xmlDoc, XmlElement root )
    {
        for( int i = 0; i < _gamelist.Count; i++ )
        {
            AddGameObjectToParent( xmlDoc, root, _gamelist[i] );
        }
    }

    /// --------------------------------------------------------------------------------
    /// <summary>
    /// 把本 GameObject 的属性 序列化到文件中
    /// </summary>
    /// --------------------------------------------------------------------------------
    private void AddGameObjectToParent( XmlDocument xmlDoc, XmlElement rootxml, GameObject obj )
    {

        XmlElement gameObject = xmlDoc.CreateElement("gameObject");
        gameObject.SetAttribute("id", NEWUUID() );
        gameObject.SetAttribute("layer", LayerMask.LayerToName(obj.layer));
        gameObject.SetAttribute("asset", PrefabUtility.GetPrefabParent(obj).name + ".unity3d");


        XmlElement transform        = xmlDoc.CreateElement("transform");
        XmlElement position         = xmlDoc.CreateElement("position");
        XmlElement position_x       = xmlDoc.CreateElement("x");
        position_x.InnerText        = obj.transform.position.x + "";
        XmlElement position_y       = xmlDoc.CreateElement("y");
        position_y.InnerText        = obj.transform.position.y + "";
        XmlElement position_z       = xmlDoc.CreateElement("z");
        position_z.InnerText        = obj.transform.position.z + "";

        position.AppendChild(position_x);
        position.AppendChild(position_y);
        position.AppendChild(position_z);

        XmlElement rotation         = xmlDoc.CreateElement("rotation");
        XmlElement rotation_x       = xmlDoc.CreateElement("x");
        rotation_x.InnerText        = obj.transform.rotation.eulerAngles.x + "";
        XmlElement rotation_y       = xmlDoc.CreateElement("y");
        rotation_y.InnerText        = obj.transform.rotation.eulerAngles.y + "";
        XmlElement rotation_z       = xmlDoc.CreateElement("z");
        rotation_z.InnerText        = obj.transform.rotation.eulerAngles.z + "";

        rotation.AppendChild(rotation_x);
        rotation.AppendChild(rotation_y);
        rotation.AppendChild(rotation_z);

        XmlElement scale            = xmlDoc.CreateElement("scale");
        XmlElement scale_x          = xmlDoc.CreateElement("x");
        scale_x.InnerText           = obj.transform.localScale.x + "";
        XmlElement scale_y          = xmlDoc.CreateElement("y");
        scale_y.InnerText           = obj.transform.localScale.y + "";
        XmlElement scale_z          = xmlDoc.CreateElement("z");
        scale_z.InnerText           = obj.transform.localScale.z + "";

        scale.AppendChild(scale_x);
        scale.AppendChild(scale_y);
        scale.AppendChild(scale_z);

        transform.AppendChild(position);
        transform.AppendChild(rotation);
        transform.AppendChild(scale);

        gameObject.AppendChild(transform);
        rootxml.AppendChild(gameObject);

        if (PrefabUtility.GetPrefabParent(obj) != null)
            return;

        foreach (Transform child in obj.transform)
        {
            AddGameObjectToParent(xmlDoc, gameObject, child.gameObject);
        }
    }

    string NEWUUID()
    {
        return System.Guid.NewGuid().ToString();
    }
}