using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargesScript : MonoBehaviour {
	[HideInInspector]
	public int charges;
	public Text scoreText;
	public GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

		if (this.tag == "enemytower1" && gc.round>0) {
			this.charges = gc.playerCharges;
		} else if(gc.round>0){
			this.charges = gc.enemyCharges;
		}else{
			this.charges = 0;
		}


	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = this.charges.ToString ();

		if (this.tag == "enemytower1") {
			PlayerPrefs.SetInt ("playerCharges",charges);
		} else {
			PlayerPrefs.SetInt ("enemyCharges",charges);
		}
	}
}
