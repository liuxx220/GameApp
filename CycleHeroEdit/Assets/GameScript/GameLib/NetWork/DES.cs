/*
 * ----------------------------------------------------------------------
 *          网络协议的解密和解密
 * 
 * 
 * ----------------------------------------------------------------------
*/
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;




public class DES 
{

    /// <summary>
    /// 加密和解密字符串的Key
    /// </summary>
    public static string StringKey
    {
        get;
        set;
    }

    public static byte[] ByteKey
    {
        get;
        set;
    }

    static DES()
    {

    }
   
}

