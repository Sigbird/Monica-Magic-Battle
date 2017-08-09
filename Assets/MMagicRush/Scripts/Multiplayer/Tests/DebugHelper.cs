using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour {
    public Text LogText;

    public static DebugHelper Instance;

    private void Awake() {
        Instance = this;
    }

    public void Append(string msg) {
        LogText.text = LogText.text + "\n" + msg;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
