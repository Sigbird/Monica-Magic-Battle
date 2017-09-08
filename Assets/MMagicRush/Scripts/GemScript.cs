using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {
	public int team;
	public int sync;

	public GameObject gemBarUI;

	public GameObject[] AvaiaBleGems;
	public GameObject ActualGem;

	public float heroProgress;
	public GameObject heroProgressObj;
	public GameObject heroProgressBar;
	private float enemyProgress;
	public GameObject enemyProgressObj;
	public GameObject enemyProgressBar;
	public bool enabled = true;

	public bool canceled = false;

	public GameObject arrowModel;
	public Sprite[] gemsprite;
	public GameObject SRender;
	public int gemvalue;
	public int id;
	private GameObject Hero;
	private GameObject Enemy;
	public GameObject gc;
	public GameObject flyingGem;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController");
//		switch (gemvalue) {
//		case 10:
//			GetComponent<SpriteRenderer> ().sprite = gemsprite [0];
//			break;
//		case 20:
//			GetComponent<SpriteRenderer> ().sprite = gemsprite [1];
//			break;
//		case 30:
//			GetComponent<SpriteRenderer> ().sprite = gemsprite [2];
//			break;
//		case 40:
//			GetComponent<SpriteRenderer> ().sprite = gemsprite [3];
//			break;
//		case 50:
//			GetComponent<SpriteRenderer> ().sprite = gemsprite [4];
//			break;
//		default:
//			break;
//		}

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

		//StartCoroutine (Begin ());
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		this.enabled = false;
		ActualGem = AvaiaBleGems [0];
		ActualGem.SetActive (false);
		//SRender.SetActive (false);
		heroProgress = 0;
		enemyProgress = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

			if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.5 && this.enabled == true) {
				gemBarUI.transform.position = this.transform.position;
				heroProgressObj.SetActive (true);
				heroProgress += Time.deltaTime * 1.5f;
				heroProgressBar.GetComponent<Animator> ().SetFloat ("Blend", heroProgress);
				Debug.Log ("Entrou");
				if (heroProgress >= 1) {
						
					if (ActualGem.transform.name == "GO_greenGem") {
						gc.GetComponent<GameController> ().MiningGem (5);
						Instantiate (flyingGem, this.transform.position, Quaternion.identity);
					}

					if (ActualGem.transform.name == "GO_greenGemYellow") {
						gc.GetComponent<GameController> ().MiningGem (1);
						if (GameObject.Find ("HeroEnemy").GetComponent<SpriteRenderer>().enabled == true) {
							TrowArrow (GameObject.Find ("HeroEnemy"));
							GameObject.Find ("HeroEnemy").GetComponent<WPIASoldierControler> ().ReceiveDamage (1);
						}
					}

					if (ActualGem.transform.name == "GO_greenGemPurple") {
						gc.GetComponent<GameController> ().MiningGem (1);
						Hero.GetComponent<WPSoldierControler> ().vida += 3;
						Hero.GetComponent<WPSoldierControler> ().UpdateLife ();
					}



//					if (team == 1) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
//						foreach (GameObject o in GameObject.FindGameObjectsWithTag("gem")) {
//							Destroy (this.gameObject);
//						}
//					}

					if(team == 2)
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().ResetEnemyGems ();

					if(team == 1)
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().ResetHeroGems ();
					//StartCoroutine (HeroReset ());
//					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
//					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().gemsPlaced = 0;
//
//					if (team == 2) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
//						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastEnemyGem = id;
//						foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemygem")) {
//							Destroy (this.gameObject);
//						}
//					} else {
//						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastGem = id;
//						//foreach (GameObject o in GameObject.FindGameObjectsWithTag("gem")) {
//							Destroy (this.gameObject);
//						//}
//					}
				}
			} else {
				//heroProgressObj.SetActive (false);
				heroProgress = 0;
			}
				
		}

		if (GameObject.Find ("HeroEnemy") != null) {
			Enemy = GameObject.Find ("HeroEnemy");

			if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.5 && this.enabled == true) {
				gemBarUI.transform.position = Camera.main.ViewportToWorldPoint (this.transform.position);
				enemyProgressObj.SetActive (true);
				enemyProgress += Time.deltaTime * 0.7f;
				enemyProgressBar.GetComponent<Animator> ().SetFloat ("Blend", heroProgress);
				if (enemyProgress >= 1) {
						
					if(ActualGem.transform.name == "GO_greenGem")
						gc.GetComponent<GameController> ().EnemyDiamonds += 5;

					if (ActualGem.transform.name == "GO_greenGemYellow") {
						gc.GetComponent<GameController> ().EnemyDiamonds += 1;
						if (GameObject.Find ("Hero").GetComponent<SpriteRenderer>().enabled == true) {
							TrowArrow (GameObject.Find ("Hero"));
							GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().ReceiveDamage (1);
						}
					}

					if (ActualGem.transform.name == "GO_greenGemPurple") {
						gc.GetComponent<GameController> ().EnemyDiamonds += 1;
						Enemy.GetComponent<WPIASoldierControler> ().vida += 3;
						Enemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
					}

//					if (team == 2) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
//						foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemygem")) {
//							Destroy (this.gameObject);
//						}
//					}
					if(team == 2)
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().ResetEnemyGems ();

					if(team == 1)
						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().ResetHeroGems ();
					//StartCoroutine (HeroReset ());
//					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
//					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().enemygemsPlaced = 0;
//
//					if (team == 2) {//GRAVA A POSIÇÂO QUE FOI PEGA E LIMPA AS GEMAS
//						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastEnemyGem = id;
//						//foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemygem")) {
//							Destroy (this.gameObject);
//						//}
//					} else {
//						GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().lastGem = id;
//						//foreach (GameObject o in GameObject.FindGameObjectsWithTag("gem")) {
//							Destroy (this.gameObject);
//						//}
//					}
				}

			} else {
				enemyProgressObj.SetActive (false);
				enemyProgress = 0;
			}
		}



	}

	public void TrowArrow(GameObject target){

		GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (target != null) {
			arrow.GetComponent<ArrowScript> ().target = target;
		} else {
			Destroy (arrow);
		}

	}

	public void EnableSingleGem(){
		this.enabled = true;
		ActualGem = AvaiaBleGems [0];
		ActualGem.SetActive (true);
	}

	public void EnableRandomGem(){
		if (team == 1) {
			//ActualGem = AvaiaBleGems [Random.Range (0, AvaiaBleGems.Length - 1)];
			ActualGem = AvaiaBleGems [gc.GetComponent<GameController> ().heroSpawnedGems];
			ActualGem.SetActive (true);
			gc.GetComponent<GameController> ().heroSpawnedGems += 1;
		} else {
			ActualGem = AvaiaBleGems [gc.GetComponent<GameController> ().enemySpawnedGems];
			ActualGem.SetActive (true);
			gc.GetComponent<GameController> ().enemySpawnedGems += 1;
		}
	}

	public void BeginInterface(){
		StartCoroutine (Begin ());
	}

	IEnumerator Begin(){
		yield return new WaitForSeconds (0.1f);
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		this.enabled = false;
		ActualGem = AvaiaBleGems [3];
		ActualGem.SetActive (false);
		//SRender.SetActive (false);
		heroProgress = 0;
		enemyProgress = 0;

		if(sync == 1)
		yield return new WaitForSeconds (2);
		if(sync == 2)
			yield return new WaitForSeconds (5);
		if(sync == 3)
			yield return new WaitForSeconds (9);
		if(sync == 4)
			yield return new WaitForSeconds (3);
		if(sync == 5)
			yield return new WaitForSeconds (6);
		if(sync == 6)
			yield return new WaitForSeconds (10);
		if(sync == 7)
			yield return new WaitForSeconds (4);

			this.enabled = true;

		if(ActualGem.name == "EmptyGem")
			EnableRandomGem ();
		

		//SRender.SetActive (true);
	}

	public void GemReset(){
		
//			heroProgressObj.SetActive (false);
//			enemyProgressObj.SetActive (false);
			this.enabled = false;
			//ActualGem = AvaiaBleGems [3];
//			ActualGem.SetActive (false);
			//SRender.SetActive (false);
			heroProgress = 0;
			enemyProgress = 0;
	}

	IEnumerator HeroReset(){
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		this.enabled = false;
		ActualGem.SetActive (false);
		//SRender.SetActive (false);
		heroProgress = 0;
		enemyProgress = 0;

		yield return new WaitForSeconds (30);

		this.enabled = true;
		EnableRandomGem ();
		//SRender.SetActive (true);
	}

//	IEnumerator EnemyReset(){
////		heroProgressObj.SetActive (false);
////		enemyProgressObj.SetActive (false);
//		this.enabled = false;
////		this.GetComponent<SpriteRenderer> ().enabled = false;
////		heroProgress = 0;
////		enemyProgress = 0;
//		yield return new WaitForSeconds (30);
//		//this.enabled = true;
//		this.GetComponent<SpriteRenderer> ().enabled = true;
//	}


}
