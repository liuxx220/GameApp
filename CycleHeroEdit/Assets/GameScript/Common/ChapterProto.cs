using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;





//--------------------------------------------------------------------------------
// 章节数据结构定义
//--------------------------------------------------------------------------------
class tagChapterProto
{
	
	public int			chapterID;
	public int 			iEnterLevel;
	
	public string  		strName;
	public string 		strBG;
}

//--------------------------------------------------------------------------------
// 章节的地图数据结构定义
//--------------------------------------------------------------------------------
class tagInstanceProto
{

	public int			dwID;
	public int			chapterID;
	public int 			iEnterLevel;
	public int 			iPosX;
	public int 			iPosY;

	public string  		strMapName;
	public string 		strIcon;
}
