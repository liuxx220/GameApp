using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EasyFingerGestues))]
public class GUIEasyFingerInspector : Editor{
	
	private Texture2D gradientTexture;

	void OnEnable(){
			
		//EasyFingerGestues t = (EasyFingerGestues)target;

	}
	
	public override void OnInspectorGUI(){
		
		EasyFingerGestues t = (EasyFingerGestues)target;
		t.showProperties = HTEditorToolKit.DrawTitleFoldOut( t.showProperties,"Finger Gestues properties");
		if (t.showProperties){
			t.enable = EditorGUILayout.Toggle("Enable Gestues",t.enable);

			HTEditorToolKit.DrawSeparatorLine();
			EditorGUILayout.Separator();
			
			t.InputAreaCenter 		= EditorGUILayout.Vector2Field("Area Center",t.InputAreaCenter);
			t.InputAreaSize 		= EditorGUILayout.Vector2Field("Area radius",t.InputAreaSize);

			EditorGUILayout.Separator();
		}
	}
}
