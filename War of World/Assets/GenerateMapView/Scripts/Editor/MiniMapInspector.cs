//----------------------------------------------
//            NJG MiniMap (NGUI)
// Copyright ?2014 Ninjutsu Games LTD.
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MapSpace;




[CustomEditor(typeof(GenerateMapInfo))]
public class GenerateMapInspecto : Editor
{
    static GenerateMapInfo m;
	int         mLayer;
	bool        saveTexture;
	string      mWorldName;

	SerializedProperty renderLayers;
    SerializedProperty boundLayers;
	void OnEnable()
	{
		renderLayers    = serializedObject.FindProperty("renderLayers");
        boundLayers     = serializedObject.FindProperty("boundLayers");
		EditorApplication.hierarchyWindowChanged += OnSceneChanged;
	}

	void OnDestroy()
	{
		EditorApplication.hierarchyWindowChanged -= OnSceneChanged;
	}

	static void OnSceneChanged()
	{
        if (GenerateMapInfo.instance != null) GenerateMapInfo.instance.UpdateBounds();
	}

	/// <summary>
	/// Draw the inspector.
	/// </summary>

	public override void OnInspectorGUI()
	{
        m = target as GenerateMapInfo;
		serializedObject.Update();
		EditorGUIUtility.labelWidth = 130f;

		GUI.SetNextControlName("empty");
		GUI.Button(new Rect(0, 0, 0, 0), "", GUIStyle.none);

		EditorGUILayout.Space();
		EditorGUILayout.Separator();			
		GUI.backgroundColor = Color.white;

		GUIStyle activeTabStyle         = new GUIStyle("ButtonMid");
		GUIStyle activeTabStyleLeft     = new GUIStyle("ButtonLeft");
		GUIStyle activeTabStyleRight    = new GUIStyle("ButtonRight");

		GUIStyle inactiveTabStyle       = new GUIStyle(activeTabStyle);
		GUIStyle inactiveTabStyleLeft   = new GUIStyle(activeTabStyleLeft);
		GUIStyle inactiveTabStyleRight  = new GUIStyle(activeTabStyleRight);

		activeTabStyle.normal           = activeTabStyle.active;
		activeTabStyleLeft.normal       = activeTabStyleLeft.active;
		activeTabStyleRight.normal      = activeTabStyleRight.active;

		GUILayout.BeginHorizontal();
        for (int i = 0, imax = (int)GenerateMapInfo.SettingsScreen._LastDoNotUse; i < imax; i++)
		{
			GUIStyle active             = activeTabStyleLeft;
			if (i > 0) active           = activeTabStyle;
            if (i == (int)GenerateMapInfo.SettingsScreen._LastDoNotUse - 1) active = activeTabStyleRight;

			GUIStyle inactive           = inactiveTabStyleLeft;
			if (i > 0) inactive         = inactiveTabStyle;
            if (i == (int)GenerateMapInfo.SettingsScreen._LastDoNotUse - 1) inactive = inactiveTabStyleRight;

            GUI.backgroundColor = m.screen == (GenerateMapInfo.SettingsScreen)i ? Color.cyan : Color.white;
            if (GUILayout.Button(((GenerateMapInfo.SettingsScreen)i).ToString(), m.screen == (GenerateMapInfo.SettingsScreen)i ? active : inactive))
			{
				GUI.FocusControl("empty");
                m.screen = (GenerateMapInfo.SettingsScreen)i;
			}
		}
		GUI.backgroundColor = Color.white;

		GUILayout.EndHorizontal();

		switch (m.screen)
		{
            case GenerateMapInfo.SettingsScreen.General:
                DrawGeneralUI();
				break;
		}
		EditorGUILayout.Separator();

		Save(false);
		serializedObject.ApplyModifiedProperties();			
	}

	void DrawGeneralUI()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
        EditorGUILayout.Separator();

        mWorldName = EditorGUILayout.TextField(new GUIContent("Level Name", "Input level name!!!."), m.mapLevel);
        if (m.mapLevel != mWorldName)
        {
            m.mapLevel = mWorldName;
            GenerateMapTools.RegisterUndo("World name", m);
        }

        GUILayout.Space(5);
        bool dontDestroy = EditorGUILayout.Toggle("Dont Destroy", m.dontDestroy);
        m.dontDestroy = dontDestroy;

        GUILayout.Space(5);
        bool showBounds = EditorGUILayout.Toggle("Display Bounds", m.showBounds);
        if (m.showBounds != showBounds)
        {
            m.showBounds = showBounds;
            m.UpdateBounds();
            GenerateMapTools.RegisterUndo("Map bounds", m);
        }


        GUILayout.Space(5);
        GenerateMapTools.BeginContents();
        EditorGUILayout.PropertyField(renderLayers, new GUIContent("Render Layers", "Which layers are going to be used for rendering."));
        EditorGUILayout.PropertyField(boundLayers, new GUIContent("Boundary Layers", "Which layers are going to be used for bounds calculation."));
        GenerateMapTools.EndContents();
        

        EditorGUILayout.BeginVertical();
        GUILayout.Space(20f);
        EditorGUIUtility.labelWidth = 100f;
        GUI.enabled = !m.generateMapTexture && !Application.isPlaying || m.generateMapTexture && Application.isPlaying;
        GUI.backgroundColor = !m.generateMapTexture || m.generateMapTexture && Application.isPlaying ? Color.green : Color.gray;
        if (GUILayout.Button(new GUIContent(m.generateMapTexture ? (Application.isPlaying ? "Regenerate" : "Click Play to generate") : "Generate New Map Texture", "Click to generate map texture"), GUILayout.Height(40f)))
        {
            Resources.UnloadUnusedAssets();
            if (m.mapTexture != null && !Application.isPlaying) NGUITools.DestroyImmediate(m.mapTexture);
            if (GenerateMapTools.GetTexture() != null)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(GenerateMapTools.GetTexture()));
            }

            GenerateMapTools.GetMainGameView().Focus();
            m.GenerateMap();
            saveTexture = true;
            GenerateMapTools.RegisterUndo("Map texture change");
        }

        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndVertical();


        if (saveTexture)
        {
            if (GenerateMapTools.GetTexture() != null)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(GenerateMapTools.GetTexture()));

            m.userMapTexture = GenerateMapTools.SaveTexture(m.userMapTexture);
            saveTexture = false;
            GenerateMapTools.RegisterUndo("Map texture change");
            if (!Application.isPlaying)
            {
                if (GenerateMapRender.instance != null) NGUITools.DestroyImmediate(GenerateMapRender.instance);
                if (GameObject.Find("_NJGMapRenderer")) NGUITools.DestroyImmediate(GameObject.Find("_NJGMapRenderer"));
            }
            Resources.UnloadUnusedAssets();
        }
    }

	static List<Camera> cameras = new List<Camera>();
	/// <summary>
	/// Sets the layer of the passed transform and all of its children
	/// </summary>

	protected static void SetLayerRecursively(Transform root, int layer)
	{
		root.gameObject.layer = layer;
		foreach (Transform child in root)
			SetLayerRecursively(child, layer);
	}

	protected void Save(bool force)
	{
		if (GUI.changed || force)
		{
            GenerateMapTools.RegisterUndo("NJG Map Settings", m);
		}
	}
}
