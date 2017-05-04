using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {
	public int team;

	public Sprite[] gemsprite;
	public int gemvalue;
	public int id;
	private GameObject Hero;
	private GameObject Enemy;
	public GameObject gc;
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
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

			//if (team == 1) {
				if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.3) {
					if (gemvalue < 40) {
						gc.GetComponent<GameController> ().MiningGem (25);
					} else {
						//Hero.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
					}
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().gemsPlaced = 0;
				if (team == 2) {
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

			//}
		}

		if (GameObject.Find ("HeroEnemy") != null) {
			Enemy = GameObject.Find ("HeroEnemy");

			//if( team == 2) {
				if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.3) {
					if (gemvalue < 40) {
						if (Enemy.GetComponent<WPIASoldierControler> ().TryTwist () == false) {
							Enemy.GetComponent<WPIASoldierControler> ().Twist (4);
						} 
					} else {
						//Hero.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
					}
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().usedValues.Clear ();
					GameObject.Find ("Terreno").GetComponent<GemsRespawn> ().enemygemsPlaced = 0;

				if (team == 2) {
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
			//}
		}



	}
}
