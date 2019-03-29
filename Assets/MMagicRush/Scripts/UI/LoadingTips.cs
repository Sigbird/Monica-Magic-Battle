using System.Collections;

using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LoadingTips : MonoBehaviour {
	public string[] texts;
	public Text textbox;
	// Use this for initialization
	void Start () {
		textbox.text = texts [Random.Range (0, texts.Length - 1)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
