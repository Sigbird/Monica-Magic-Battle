using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour {

	public float timerCounter;
	public int parse;
	public int team;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Text> ().enabled == true) {
			parse = (int)timerCounter;
			this.GetComponent<Text> ().text = parse.ToString ();
		}
		
	}

	public void ActiveRespawnTimer(float x){
		this.GetComponent<Text> ().enabled = true;
		timerCounter = x;
		StartCoroutine (Timer());
	}

	public IEnumerator Timer(){
		if (timerCounter > 0) {
			yield return new WaitForSeconds (1);
			if (team == 1) {
				timerCounter -= 0.25f;
			} else {
				timerCounter -= 0.5f;
			}
			StartCoroutine (Timer ());
		} else {
			this.GetComponent<Text> ().enabled = false;
		} 
	}

}
