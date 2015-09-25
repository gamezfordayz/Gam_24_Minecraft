using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapBuilder))]
public class MapBuilderEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		MapBuilder myScript = (MapBuilder)target;
		if(GUILayout.Button("Build Map"))
		{
			myScript.BuildMap();
		}
		if(GUILayout.Button("Destroy All Map"))
		{
			myScript.KillWorld();
		}
	}
}
