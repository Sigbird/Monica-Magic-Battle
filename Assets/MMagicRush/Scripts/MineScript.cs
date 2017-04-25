using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineScript : MonoBehaviour {

	private GameObject Hero;
	private float heroProgress;
	public GameObject heroProgressObj;
	public GameObject heroProgressBar;
	private GameObject Enemy;
	private float enemyProgress;
	public GameObject enemyProgressObj;
	public GameObject enemyProgressBar;

	public int Mine;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

			if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.5f) {
				heroProgressObj.SetActive (true);
				heroProgressBar.GetComponent<Animator> ().SetFloat ("Blend", heroProgress);
				heroProgress += Time.deltaTime * 0.1f;
			}else if (heroProgress >= 0) {
				heroProgressObj.SetActive (false);
				heroProgress -= Time.deltaTime * 0.1f;
			}
		}

		if (GameObject.Find ("HeroEnemy") != null) {
			Enemy = GameObject.Find ("HeroEnemy");

			if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.5f) {
				enemyProgressObj.SetActive (true);
				enemyProgressBar.GetComponent<Animator> ().SetFloat ("Blend", enemyProgress);
				enemyProgress += Time.deltaTime * 0.1f;
			} else if (enemyProgress >= 0) {
				enemyProgressObj.SetActive (false);
				enemyProgress -= Time.deltaTime * 0.1f;
			}
		}

		if (enemyProgress >= 1) {
			EnemyTeleport ();
		} else if (heroProgress >= 1) {
			HeroTeleport ();
		}


	}

	public void HeroTeleport(){
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		heroProgress = 0;
		enemyProgress = 0;
		if (Mine == 1) {
			Hero.transform.position = new Vector2 (-1.86f, 1.07f);
		} else {
			Hero.transform.position = new Vector2 (1.93f, -0.34f);
		}
	}

	public void EnemyTeleport(){
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		heroProgress = 0;
		enemyProgress = 0;
		if (Mine == 1) {
			Enemy.transform.position = new Vector2 (-1.86f, 1.07f);
		} else {
			Enemy.transform.position = new Vector2 (1.93f, -0.34f);
		}

	}
}
