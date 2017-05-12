using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureScript : MonoBehaviour {
	private GameObject Hero;
	public float heroProgress;
	public GameObject heroProgressObj;
	public GameObject heroProgressBar;
	private GameObject Enemy;
	public float enemyProgress;
	public GameObject enemyProgressObj;
	public GameObject enemyProgressBar;
	public bool enabled = true;

	public Sprite[] sprites;
	public string bonus;
	// Use this for initialization
	void Start () {
		StartCoroutine (Begin ());
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Hero") != null) {
			Hero = GameObject.Find ("Hero");

				if (Vector2.Distance (this.transform.position, Hero.transform.position) < 0.5f && this.enabled == true) {
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

			if (Vector2.Distance (this.transform.position, Enemy.transform.position) < 0.5f && this.enabled == true) {
				enemyProgressObj.SetActive (true);
				enemyProgressBar.GetComponent<Animator> ().SetFloat ("Blend", enemyProgress);
				enemyProgress += Time.deltaTime * 0.1f;
			} else if (enemyProgress >= 0) {
				enemyProgressObj.SetActive (false);
				enemyProgress -= Time.deltaTime * 0.1f;
			}
		}

		if (enemyProgress >= 1) {
			StartCoroutine (EnemyReset ());
		} else if (heroProgress >= 1) {
			StartCoroutine (HeroReset ());
		}


	}

	IEnumerator Begin(){
		yield return new WaitForSeconds (30);
		this.enabled = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;
		if (Random.value >= 0.5) {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [0];
			this.bonus = "sight";
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [1];
			this.bonus = "speed";
		}
	}

	IEnumerator HeroReset(){
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		this.enabled = false;
		this.GetComponent<SpriteRenderer> ().enabled = false;
		heroProgress = 0;
		enemyProgress = 0;
		Hero.GetComponent<WPSoldierControler> ().ReceiveEffect (this.bonus);
		yield return new WaitForSeconds (30);
		this.enabled = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;
		if (Random.value >= 0.5) {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [0];
			this.bonus = "sight";
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [1];
			this.bonus = "speed";
		}

	}

	IEnumerator EnemyReset(){
		heroProgressObj.SetActive (false);
		enemyProgressObj.SetActive (false);
		this.enabled = false;
		this.GetComponent<SpriteRenderer> ().enabled = false;
		heroProgress = 0;
		enemyProgress = 0;
		Enemy.GetComponent<WPIASoldierControler> ().ReceiveEffect (this.bonus);
		yield return new WaitForSeconds (30);
		this.enabled = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;
		if (Random.value >= 0.5) {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [0];
			this.bonus = "sight";
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = sprites [1];
			this.bonus = "speed";
		}
	}
}
