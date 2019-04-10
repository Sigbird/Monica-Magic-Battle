﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargesScript : MonoBehaviour {
	
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
	public HealtBar HealtBarTower;
	public HealtBarSolid HealtBarSolid;
	public float vida;
	public float vidaMax;
	public GameObject HitAnimationObject;
	public bool NewMechanic;
	public GameObject SplashEffect;

	private TowerEntity towerEntity;


	void Awake() {
		towerEntity = GetComponent<TowerEntity>();
	}
	// Use this for initialization
	void Start () {

		vidaMax = 550;
		vida = 550;
		progress = 0;
		uiProgressBar.SetFloat ("Blend", progress);

		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

		if (this.tag == "enemytower1" && gc.round>0) {
			this.charges = gc.playerCharges;
		} else if(gc.round>0){
			this.charges = gc.enemyCharges;
		}else{
			this.charges = 0;
		}


//		if (HealtBarTower != null) {
//
//			//this.vida = Mathf.RoundToInt ((progress * 100) / 25);
//			this.HealtBarTower.gameObject.SetActive (true);
//			UpdateLife ();
//			this.HealtBarTower.RefreshMaxLIfe ();
//		}


	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = this.charges.ToString ();
		if (HealtBarSolid != null) {
			HealtBarSolid.atualValue = vida;
			HealtBarSolid.maxValue = vidaMax;
		}
//		if (endgame == true) {
//			NetGameController.Instance.EndGame ();
//		}

		if (NewMechanic) {

			if (tutorial == false) {
				if (this.tag == "enemytower1") {
					if (vida<=0 && endgame == false) {
						//GameObject.FindGameObjectWithTag ("enemytower2").GetComponent<ChargesScript> ().charges++;
						gc.Player2Score = 3;
						gc.NextRound ();
						endgame = true;
					}
					//PlayerPrefs.SetInt ("playerCharges", charges);
					gc.playerCharges = this.charges;
				} else {
					if (vida<=0 && endgame == false) {
						//GameObject.FindGameObjectWithTag ("enemytower1").GetComponent<ChargesScript> ().charges++;
						gc.Player1Score = 3;
						gc.NextRound ();
						endgame = true;
					}
					//PlayerPrefs.SetInt ("enemyCharges", charges);
					gc.enemyCharges = this.charges;
				}
			} else {
				if (this.tag == "enemytower1") {
					if (tutorial == false) {
						if (vida<=0 && endgame == false) {
							if (GameObject.Find ("TutorialPanels") != null)
								GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
							VictoryScreen.SetActive (true);
							Time.timeScale = 0;
							endgame = true;
						}
						//PlayerPrefs.SetInt ("playerCharges", charges);
						gc.playerCharges = this.charges;
					}
				} else {
					if (vida<=0 && endgame == false) {
						if (GameObject.Find ("TutorialPanels") != null)
							GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
						VictoryScreen.SetActive (true);
						Time.timeScale = 0;
						endgame = true;
					}
					//PlayerPrefs.SetInt ("enemyCharges", charges);
					gc.enemyCharges = this.charges;
				}
			}
		
		} else {
		
			if (tutorial == false) {
				if (this.tag == "enemytower1") {
					if (progress >= 1 && endgame == false) {
						GameObject.FindGameObjectWithTag ("enemytower2").GetComponent<ChargesScript> ().charges++;
						GameObject.Find ("GameController").GetComponent<GameController> ().NextRound ();
						endgame = true;
					}
					PlayerPrefs.SetInt ("playerCharges", charges);
					gc.playerCharges = this.charges;
				} else {
					if (progress >= 1 && endgame == false) {
						GameObject.FindGameObjectWithTag ("enemytower1").GetComponent<ChargesScript> ().charges++;
						GameObject.Find ("GameController").GetComponent<GameController> ().NextRound ();
						endgame = true;
					}
					PlayerPrefs.SetInt ("enemyCharges", charges);
					gc.enemyCharges = this.charges;
				}
			} else {
				if (this.tag == "enemytower1") {
					if (tutorial == false) {
						if (progress >= 1 && endgame == false) {
							if (GameObject.Find ("TutorialPanels") != null)
								GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
							VictoryScreen.SetActive (true);
							Time.timeScale = 0;
							endgame = true;
						}
						PlayerPrefs.SetInt ("playerCharges", charges);
						gc.playerCharges = this.charges;
					}
				} else {
					if (progress >= 1 && endgame == false) {
						if (GameObject.Find ("TutorialPanels") != null)
							GameObject.Find ("TutorialPanels").transform.gameObject.SetActive (false);
						VictoryScreen.SetActive (true);
						Time.timeScale = 0;
						endgame = true;
					}
					PlayerPrefs.SetInt ("enemyCharges", charges);
					gc.enemyCharges = this.charges;
				}
			}

		}






		if (this.tag == "enemytower1" && progress<1) {
			enemies = GameObject.FindGameObjectsWithTag ("enemysoldier2");
			foreach (GameObject en in enemies) {
				if (Vector2.Distance (en.transform.position, this.transform.position) < 3 && en.GetComponent<SpriteRenderer>().enabled == true) {
					//progress += Time.deltaTime * progressSpeed;//0.1f
					uiProgressBar.SetFloat ("Blend", progress);
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
					uiProgressBar.SetFloat ("Blend", progress);
					inCombat = true;
				} else {
					inCombat = false;
				}
			}
		}


	}

	public void ReceiveDamage(float x){

		this.vida -= x;
		towerEntity.SetVida(this.vida);
		
		if (NewMechanic) {
			UpdateLife ();
		}

		Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);

	}


	public void ReceiveDamage(float x, bool explosion){

		this.vida -= x;
		//UpdateLife ();
		if (explosion) {
			Instantiate (SplashEffect, this.transform.position, Quaternion.identity);
		} else {
			Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);
		}

	}

	public void UpdateLife(){

//		this.HealtBarTower.Life = this.vida;
//		this.HealtBarTower.MaxLife = this.vidaMax;
//		this.HealtBarTower.UpdateHealtbars();
	}

}
