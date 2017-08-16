//----------------------------------------------
//            NJG MiniMap (NGUI)
// Copyright � 2014 Ninjutsu Games LTD.
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;






public class GenerateMapTools : MonoBehaviour
{
	static public string contentFolder = "NinjutsuGames/Mini MiniMap";

	
	/// <summary>
	/// Create an undo point for the specified objects.
	/// </summary>

	static public void RegisterUndo(string name, params Object[] objects)
	{
		if (objects != null && objects.Length > 0)
		{

			UnityEditor.Undo.RecordObjects(objects, name);
			foreach (Object obj in objects)
			{
				if (obj == null) continue;
				EditorUtility.SetDirty(obj);
			}
		}
	}


	static public Texture2D SaveTexture(Texture2D tex)
	{
		if (tex == null) return null;
		byte[] bytes = tex.EncodeToPNG();
		string path = GetPath();

		string[] arr = EditorApplication.currentScene.Split('/');
		string scene = arr[arr.Length - 1].Replace(".unity", string.Empty);

		string fileName = scene + ".png";
		System.IO.File.WriteAllBytes(path + fileName, bytes);
		
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		string newPath = "Assets/" + contentFolder + "/_Generated Content/" + fileName;
		Texture2D tx = (Texture2D)AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture2D));
		EditorGUIUtility.PingObject(tx);
		return tx;
	}

	static public Texture2D GetTexture()
	{
		string[] arr = EditorApplication.currentScene.Split('/');
		string scene = arr[arr.Length - 1].Replace(".unity", string.Empty);
		string fileName = scene + ".png";
		return (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/"+contentFolder + "/_Generated Content/" + fileName, typeof(Texture2D));
	}

	static public Vector2 GetGameViewSize()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
		return (Vector2)Res;
	}

	static public EditorWindow GetMainGameView()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetMainGameView.Invoke(null, null);
		return (EditorWindow)Res;
	}

	static string GetPath()
	{
		string path = Application.dataPath + "/" + contentFolder + "/_Generated Content/";
		if (!System.IO.Directory.Exists(path))
		{
			System.IO.Directory.CreateDirectory(path);
			AssetDatabase.Refresh();
		}

		return path;
	}


	/// <summary>
	/// Create a checker-background texture
	/// </summary>

	static Texture2D CreateCheckerTex(Color c0, Color c1)
	{
		Texture2D tex = new Texture2D(16, 16);
		tex.name = "[Generated] Checker Texture";
		tex.hideFlags = HideFlags.DontSave;

		for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
		for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
		for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
		for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

		tex.Apply();
		tex.filterMode = FilterMode.Point;
		return tex;
	}

    /// <summary>
    /// Begin drawing the content area.
    /// </summary>

    static public void BeginContents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(4f);
        EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
        GUILayout.BeginVertical();
        GUILayout.Space(2f);
    }

    /// <summary>
    /// End drawing the content area.
    /// </summary>

    static public void EndContents()
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(3f);
        GUILayout.EndHorizontal();
        GUILayout.Space(3f);
    }
}
