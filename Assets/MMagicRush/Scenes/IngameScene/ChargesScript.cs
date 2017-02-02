using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargesScript : MonoBehaviour {

	public int charges;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		this.charges = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = this.charges.ToString ();

		if (this.charges == 5) {
			Time.timeScale = 0;
			GameObject.Find ("Vitória").SetActive (true);
		}
	}
}
