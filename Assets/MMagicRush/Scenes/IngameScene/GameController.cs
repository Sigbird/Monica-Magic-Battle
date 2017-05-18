﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float gempertime;	

	public int playerCharges;
	public static float playerXp;

	public int enemyCharges;
	public static float enemyXp;

	public CardInfoScript cardInfo;


	[HideInInspector]
	public int round ;
	[HideInInspector]
	public int roundCounter;

	public GameObject[] endGamePanel;
	[HideInInspector]
	public int[] empty;

	public string tipo{ get; set; }
	public GameObject Soldado;
	public int Diamonds;
	public int EnemyDiamonds;
	public Sprite[] cointSprites;
	[HideInInspector]
	public int WarriorCost = 10;
	[HideInInspector]
	public int MageCost = 10;
	[HideInInspector]
	public int LanceiroCost = 10;

	[HideInInspector]
	public int Mine1Value = 0;
	private float Mine1Assist;
	[HideInInspector]
	public int Mine2Value = 0;
	private float Mine2Assist;
	// Use this for initialization
	void Awake() {
		EnemyDiamonds = 0;
		Diamonds = 0;
		enemyCharges = PlayerPrefs.GetInt ("enemyCharges");
		Debug.Log ("Enemy " + enemyCharges);
		playerCharges = PlayerPrefs.GetInt ("playerCharges");
		Debug.Log ("Player " + playerCharges);
		round = PlayerPrefs.GetInt ("round");
		Debug.Log ("Round " + round);
		if (enemyCharges == 0 && playerCharges == 0) {
			this.round = 1;
			GameController.playerXp = 0;
			GameController.enemyXp = 0;
		} else {
			GameController.playerXp = PlayerPrefs.GetFloat("PlayerXP");
			GameController.enemyXp = PlayerPrefs.GetFloat("EnemyXP");
		}

		//StartCoroutine (EnemyAI ());
	}
	
	// Update is called once per frame
	void Update () {

		if (gempertime >= 2) {
			gempertime = 0;
			Diamonds += 1;
			EnemyDiamonds += 1;
		} else {
			gempertime += Time.deltaTime;
		}

		GameObject.Find ("Diamonds").GetComponent<Text> ().text = Diamonds.ToString();
//		GameObject.Find ("DiamondsEnemy").GetComponent<Text> ().text = EnemyDiamonds.ToString();
//		GameObject.Find ("Cost1").GetComponent<Text> ().text = WarriorCost.ToString();
//		GameObject.Find ("Cost2").GetComponent<Text> ().text = LanceiroCost.ToString();
//		GameObject.Find ("Cost3").GetComponent<Text> ().text = MageCost.ToString();
//		GameObject.Find ("Mine1Text").GetComponent<Text> ().text = Mine1Value.ToString();
	//	GameObject.Find ("Mine2Text").GetComponent<Text> ().text = Mine1Value.ToString();
//		Mine1Assist += Time.deltaTime;
//		if (Mine1Assist >= 2) {
//			Mine1Assist = 0;
//			//Mine1Value = (int)Mine1Assist;
//			if(Mine1Value<3)
//			Mine1Value += 1;
//		}
//		Mine2Assist += Time.deltaTime;
//		if (Mine2Assist >= 2) {
//			Mine2Assist = 0;
//			//Mine2Value = (int)Mine1Assist;
//			if(Mine2Value<3)
//				Mine2Value += 1;
//		}
//
//		if (Mine2Assist <= 3) {
//			Mine2Assist += 1;
//			Mine2Value = (int)Mine2Assist;
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled = false;
//		} else {
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled = true;
//		}
	
//		switch (Mine1Value) {
//		case 0:
//			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().sprite = cointSprites [0];
//			break;
//		case 1:
//			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().sprite = cointSprites [1];
//			break;
//		case 2:
//			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().sprite = cointSprites [2];
//			break;
//		case 3:
//			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().sprite = cointSprites [3];
//			break;
//		}
//
//		switch (Mine2Value) {
//		case 0:
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().sprite = cointSprites [0];
//			break;
//		case 1:
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().sprite = cointSprites [1];
//			break;
//		case 2:
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().sprite = cointSprites [2];
//			break;
//		case 3:
//			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().sprite = cointSprites [3];
//			break;
//		}


	}

	public void MiningGems(int x){
	
		if (x == 1 && GameObject.Find ("Mine1Cristal").GetComponent<Image> ().enabled == true ) {
			Diamonds = Mine1Value + Diamonds;
			Mine1Value = 0;
		}else if (x == 2 && GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled == true) {
			Diamonds = Mine2Value + Diamonds;
			Mine2Value = 0;
		}

	}

	public void MiningGem(int x){

		Diamonds += x;

	}


	public void NextRound(){
		Debug.Log ("xp: "+GameController.playerXp);
		PlayerPrefs.SetFloat ("PlayerXP", GameController.playerXp);
		PlayerPrefs.SetFloat ("EnemyXP", GameController.enemyXp);
		StartCoroutine (newRound ());

	}

	IEnumerator newRound(){
		yield return new WaitForSeconds (0.1f);
		Debug.Log (enemyCharges);
		if (enemyCharges == 3) {
			PlayerPrefs.SetInt ("round", 1);
			PlayerPrefs.SetInt ("playerCharges", 0);
			PlayerPrefs.SetInt ("enemyCharges", 0);
			StartCoroutine (endGame());
		}else if(playerCharges == 3) {
			PlayerPrefs.SetInt ("round", 1);
			PlayerPrefs.SetInt ("playerCharges", 0);
			PlayerPrefs.SetInt ("enemyCharges", 0);
			StartCoroutine (endGame());
		} else {
			PlayerPrefs.SetInt ("round", round + 1);
			yield return new WaitForSeconds (2);
			SceneManager.LoadScene ("Jogo");
		}

	}

	IEnumerator endGame(){
		
		yield return new WaitForSeconds (1);

		if (playerCharges == 3 && enemyCharges <= 2) {
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("victory");
			endGamePanel [0].SetActive (true);
		} else if (enemyCharges == 3 && playerCharges <= 2) {
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("defeat");
			endGamePanel [1].SetActive (true);
		} else { // EMPATE
			Debug.Log ("EMPATE");
			this.GetComponent<AudioManager> ().StopAudio ();

			this.GetComponent<AudioManager> ().PlayAudio ("victory");
			endGamePanel [0].SetActive (true);
		}
		Time.timeScale = 0;
	}

	void OnApplicationQuit() {
		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);

		//PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
		//PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}

	public void LoadScene(string scene){
		Time.timeScale = 1;
		round = 1;
		playerCharges = 0;
		enemyCharges = 0;
		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);
		SceneManager.LoadScene (scene);
	}

	public void ClearGame(){
//		PlayerPrefs.SetInt ("round",1);
//		PlayerPrefs.SetInt ("playerCharges",0);
//		PlayerPrefs.SetInt ("enemyCharges",0);
	}

	public void CallScene(string scene){
		Time.timeScale = 1;
		if (scene == "quit") {
			Application.Quit ();
		}else if (scene == "start") {
			Time.timeScale = 1;
		} else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

}
