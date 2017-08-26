using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGridGenerator))]
public class GridGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameGridGenerator myScript = (GameGridGenerator) target;
        if (GUILayout.Button("Create Grid")) {
            myScript.GenerateGrid();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
