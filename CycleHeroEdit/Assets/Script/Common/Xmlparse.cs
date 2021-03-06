using System;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;




public class CXmlContainer 
{

	private 			Dictionary< string, string > m_mapData;

	public CXmlContainer( )
	{
		m_mapData = new Dictionary<string, string>();
	}


	public void Destroy()
	{
		m_mapData.Clear ();

	}


	public static string GetResourcePath( )
	{
		return Application.dataPath;
	}

	
	public bool LoadXML( string path, string strID, List<string> list )
	{
		TextAsset rscTextAsset = (TextAsset)Resources.Load (path);
		if (rscTextAsset != null) 
		{

			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (new MemoryStream (rscTextAsset.bytes));
			XmlNode rootNode = xmlDoc.FirstChild;

			if( rootNode.Name == "something" )
			{
				int rscCount = rootNode.ChildNodes.Count;
				for( int i = 0; i < rscCount; i++ )
				{

					for( int j = 0; j < rootNode.ChildNodes[i].Attributes.Count; j++ )
					{

						string strtempkey = "";
						string strkey  	  = rootNode.ChildNodes [i].Attributes [0].Value;
						XmlAttribute attr = rootNode.ChildNodes [i].Attributes [j];

						if( strID == attr.Name )
						{
							list.Add( attr.Value );
						}

						strtempkey		  = strkey + "_" + attr.Name;
						m_mapData.Add( strtempkey, attr.Value );
					}

				}
			}
			return true;
		}
		return false;
	}

	// 读取元素
	string				GetString( string szName)
	{
		string strValue;
		m_mapData.TryGetValue( szName, out strValue );
		return strValue;
	}


	public string		GetString( string szName, string szField )
	{
		string strkey = szField + "_" + szName;
		string strval = GetString (strkey);

		return strval;
	}



	public string		GetString( string szName, string szField, string szDefault)
	{
		string strkey = szField + "_" + szName;
		string strval = GetString (strkey);

		return strval;
	}


	public uint  		GetDword( string szName, string szField)
	{

		string strval = GetString (szName, szField );

		return uint.Parse( strval );
	}

	public float		GetFloat( string szName, string szField,  float fDefault)
	{

		string strval = GetString (szName, szField);
		return float.Parse( strval );
	}

	public int			GetInt(  string szName, string szField,   int nDefault)
	{

		string strval = GetString (szName, szField);
		return int.Parse( strval );

	}
		 
}
