//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//[CustomEditor(typeof(UnityCam))]
//public class HUGVRCamEditor : Editor
//{

//	SerializedProperty _flipImage;
//	SerializedProperty _blitLocaly;

//	SerializedProperty _resultTexture;


//	void OnEnable()
//	{
//		_flipImage = serializedObject.FindProperty("Flip");
//		_blitLocaly = serializedObject.FindProperty("BlitLocaly");
//		_resultTexture = serializedObject.FindProperty("ResultTexture");
//	}


//	public override void OnInspectorGUI()
//	{
//		serializedObject.Update();

//		EditorGUILayout.PropertyField(_flipImage);
//		EditorGUILayout.PropertyField(_blitLocaly);
//		EditorGUILayout.PropertyField(_resultTexture);

//		serializedObject.ApplyModifiedProperties();
//	}
//}
