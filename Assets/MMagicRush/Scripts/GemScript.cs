using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {
	public int team;

	private GameObject Hero;
	private GameObject Enemy;
	public GameObject gc;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

			if (team == 1) {
				if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.3) {
					gc.GetComponent<GameController>().MiningGem (10);
					Destroy (this.gameObject);
				}

			}
		}

		if (GameObject.Find ("HeroEnemy") != null) {
			Enemy = GameObject.Find ("HeroEnemy");

			if( team == 2) {
				if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.3) {
					Destroy (this.gameObject);
				}
			}
		}



	}
}
