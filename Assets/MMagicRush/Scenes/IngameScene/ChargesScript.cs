using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargesScript : MonoBehaviour {

	public int charges;
	public Text scoreText;
	public GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

		if (this.tag == "enemysoldier1" && gc.round>0) {
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

		if (this.charges == 2) {
			Time.timeScale = 0;
			GameObject.Find ("Vitória").SetActive (true);
		}

		if (this.tag == "enemysoldier1") {
			gc.playerCharges = this.charges;
		} else {
			gc.enemyCharges = this.charges;
		}
	}
}
