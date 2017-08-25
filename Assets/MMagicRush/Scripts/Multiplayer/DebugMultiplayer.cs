using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class DebugMultiplayer : MonoBehaviour {
	public Text Debug;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void NetPrint(string message) {
		Debug.text = Debug.text + "\n" + message;
	}

	void OnEnable() {
		NetworkSessionManager.NetPrintEvent += NetPrint;
	}

	void OnDisable() {
		NetworkSessionManager.NetPrintEvent -= NetPrint;
	}
}
