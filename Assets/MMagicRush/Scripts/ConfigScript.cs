using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString ("MovementOnly", "False");
		PlayerPrefs.SetString ("Tutorial", "True");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AtiveTutorial(){
		PlayerPrefs.SetString ("Tutorial", "True");
	}

	public void DesativeTutorial(){
		PlayerPrefs.SetString ("Tutorial", "False");
	}

	public void AtiveMovimentOnly(){
		PlayerPrefs.SetString ("MovementOnly", "True");
	}

	public void DesativeMovimentOnly(){
		PlayerPrefs.SetString ("MovementOnly", "False");
	}
}
