﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargesScriptTowers : MonoBehaviour {
	
	public bool tutorial;

	public int charges;
	public Text scoreText;
	public GameController gc;
	public Animator uiProgressBar;

	public bool inCombat;

	private bool endgame;

	private GameObject[] enemies;
	public float progress;
	public float progressSpeed;

	public GameObject VictoryScreen;
	public GameObject HitEffect;



	// Use this for initialization
	void Start () {
		progress = 0;


		gc = GameObject.Find ("GameController").GetComponent<GameController> ();


			this.charges = 0;




	}
	
	// Update is called once per frame
	void Update () {
		

		if (tutorial == false) {
			if (this.tag == "enemytower1") {
				if (progress >= 1 && endgame == false) {
					gameObject.SetActive (false);
					//Destroy (this.gameObject);
//					GameObject.FindGameObjectWithTag ("enemytower2").GetComponent<ChargesScript> ().charges++;
//					GameObject.Find ("GameController").GetComponent<GameController> ().NextRound ();
//					endgame = true;
				}
//				PlayerPrefs.SetInt ("playerCharges", charges);
//				gc.playerCharges = this.charges;
			} else {
				if (progress >= 1 && endgame == false) {
					gameObject.SetActive (false);
					//Destroy (this.gameObject);
//					GameObject.FindGameObjectWithTag ("enemytower1").GetComponent<ChargesScript> ().charges++;
//					GameObject.Find ("GameController").GetComponent<GameController> ().NextRound ();
//					endgame = true;
				}
//				PlayerPrefs.SetInt ("enemyCharges", charges);
//				gc.enemyCharges = this.charges;
			}
		} else {
			if (this.tag == "enemytower1") {
					if (progress >= 1 && endgame == false) {
					gameObject.SetActive (false);
						//Destroy (this.gameObject);
//						if (GameObject.Find ("TutorialPanels") != null)
//							GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
//						VictoryScreen.SetActive (true);
//						Time.timeScale = 0;
//						endgame = true;
					}
//					PlayerPrefs.SetInt ("playerCharges", charges);
//					gc.playerCharges = this.charges;

			} else {
				if (progress >= 1 && endgame == false) {
					//Destroy (this.gameObject);
					gameObject.SetActive (false);
//					if (GameObject.Find ("TutorialPanels") != null)
//						GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
//					VictoryScreen.SetActive (true);
//					Time.timeScale = 0;
//					endgame = true;
				}
//				PlayerPrefs.SetInt ("enemyCharges", charges);
//				gc.enemyCharges = this.charges;
			}
		}




		if (this.tag == "enemytower1" && progress<1) {
			enemies = GameObject.FindGameObjectsWithTag ("enemysoldier2");
			foreach (GameObject en in enemies) {
				if (Vector2.Distance (en.transform.position, this.transform.position) < 3 && en.GetComponent<SpriteRenderer>().enabled == true) {
					//progress += Time.deltaTime * progressSpeed;//0.1f
					//uiProgressBar.SetFloat ("Blend", progress);
					inCombat = true;
				} else {
					inCombat = false;
				}
			}
		
		} else if (this.tag == "enemytower2"&& progress<1) {
			enemies = GameObject.FindGameObjectsWithTag ("enemysoldier1");
			foreach (GameObject en in enemies) {
				if (Vector2.Distance (en.transform.position, this.transform.position) < 3  && en.GetComponent<SpriteRenderer>().enabled == true) {
					//progress += Time.deltaTime * progressSpeed;//0.1f
					//uiProgressBar.SetFloat ("Blend", progress);
					inCombat = true;
				} else {
					inCombat = false;
				}
			}
		}


	}
}