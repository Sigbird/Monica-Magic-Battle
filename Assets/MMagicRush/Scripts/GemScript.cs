using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {
	public int team;

	public GameObject gemBarUI;

	public float heroProgress;
	public GameObject heroProgressObj;
	public GameObject heroProgressBar;
	private float enemyProgress;
	public GameObject enemyProgressObj;
	public GameObject enemyProgressBar;
	public bool enabled = true;

	public Sprite[] gemsprite;
	public int gemvalue;
	public int id;
	private GameObject Hero;
	private GameObject Enemy;
	public GameObject gc;
	public GameObject flyingGem;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController");
		switch (gemvalue) {
		case 10:
			GetComponent<SpriteRenderer> ().sprite = gemsprite [0];
			break;
		case 20:
			GetComponent<SpriteRenderer> ().sprite = gemsprite [1];
			break;
		case 30:
			GetComponent<SpriteRenderer> ().sprite = gemsprite [2];
			break;
		case 40:
			GetComponent<SpriteRenderer> ().sprite = gemsprite [3];
			break;
		case 50:
			GetComponent<SpriteRenderer> ().sprite = gemsprite [4];
			break;
		default:
			break;
		}

		if (team == 1) {
			gemBarUI = GameObject.Find ("GemBarUI").gameObject;
			heroProgressObj = gemBarUI.GetComponent<GemBarUI> ().heroProgressObj;
			heroProgressBar = gemBarUI.GetComponent<GemBarUI> ().heroProgressBar;
			enemyProgressObj = gemBarUI.GetComponent<GemBarUI> ().enemyProgressObj;
			enemyProgressBar = gemBarUI.GetComponent<GemBarUI> ().enemyProgressBar;
		} else {
			gemBarUI = GameObject.Find ("GemBarUIenemy").gameObject;
			heroProgressObj = gemBarUI.GetComponent<GemBarUI> ().heroProgressObj;
			heroProgressBar = gemBarUI.GetComponent<GemBarUI> ().heroProgressBar;
			enemyProgressObj = gemBarUI.GetComponent<GemBarUI> ().enemyProgressObj;
			enemyProgressBar = gemBarUI.GetComponent<GemBarUI> ().enemyProgressBar;
		}




	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

			if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.5) {
				gemBarUI.transform.position = this.transform.position;
				heroProgressObj.SetActive (true);
				heroProgress += Time.deltaTime * 0.7f;
				heroProgressBar.GetComponent<Animator> ().SetFloat ("Blend", heroProgress);
				Debug.Log ("Entrou");
				if (heroProgress >= 1) {

					heroProgressObj.SetActive (false);
					enemyProgressObj.SetActive (false);
					heroProgress = 0;
					enemyProgress = 0;

					if (gemvalue < 40) {
						gc.GetComponent<GameController> ().MiningGem (5);
						Instantiate (flyingGem, this.transform.position, Quaternion.identity);
					} else {
						//Hero.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
					}
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().gemsPlaced = 0;
					if (team == 2) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastEnemyGem = id;
						foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemygem")) {
							Destroy (o.gameObject);
						}
					} else {
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastGem = id;
						foreach (GameObject o in GameObject.FindGameObjectsWithTag("gem")) {
							Destroy (o.gameObject);
						}
					}
				}
			} else {
				//heroProgressObj.SetActive (false);
				heroProgress = 0;
			}
				
		}

		if (GameObject.Find ("HeroEnemy") != null) {
			Enemy = GameObject.Find ("HeroEnemy");

			if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.5) {
				gemBarUI.transform.position = Camera.main.ViewportToWorldPoint (this.transform.position);
				enemyProgressObj.SetActive (true);
				enemyProgress += Time.deltaTime * 0.7f;
				enemyProgressBar.GetComponent<Animator> ().SetFloat ("Blend", heroProgress);
				if (enemyProgress >= 1) {

					heroProgressObj.SetActive (false);
					enemyProgressObj.SetActive (false);
					heroProgress = 0;
					enemyProgress = 0;

					if (gemvalue < 40) {
						gc.GetComponent<GameController> ().EnemyDiamonds += 5;
						if (Enemy.GetComponent<WPIASoldierControler> ().TryTwist () == false) {
							Enemy.GetComponent<WPIASoldierControler> ().Twist (4);
						} 
					} else {
						//Hero.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
					}
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().enemygemsPlaced = 0;

					if (team == 2) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastEnemyGem = id;
						foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemygem")) {
							Destroy (o.gameObject);
						}
					} else {
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastGem = id;
						foreach (GameObject o in GameObject.FindGameObjectsWithTag("gem")) {
							Destroy (o.gameObject);
						}
					}
				}

			} else {
				enemyProgressObj.SetActive (false);
				enemyProgress = 0;
			}
		}



	}
}
