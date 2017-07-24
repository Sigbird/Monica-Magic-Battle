using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

	public float gempertime;	

	public int playerCharges;
	public static float playerXp;

	public int enemyCharges;
	public static float enemyXp;

	public int[] empty;
	public int enemySpawnedGems = 0;
	public int heroSpawnedGems = 0;

	public int Diamonds;
	public int EnemyDiamonds;
	public Sprite[] cointSprites;

	[HideInInspector]
	public int Mine1Value = 0;
	private float Mine1Assist;
	[HideInInspector]
	public int Mine2Value = 0;
	private float Mine2Assist;

	public GameObject[] tutorialPanels;
	public GameObject[] interfaceElements;

	public GameObject WaypointSystem;
	public GameObject HeroEnemy;
	public GameObject RewardWindow;
	public GameObject RewardWindow2;
	public GameObject TutorialGem;
	public GameObject portraitPanel;
	public GameObject DeckPile;
	public GameObject tutorialArrows;

	void Awake() {
		//wwwwPlayerPrefs.SetInt ("Lesson", 5);
		//PlayerPrefs.SetString ("TutorialCoins", "false");
		Time.timeScale = 1;
		EnemyDiamonds = 0;
		Diamonds = 0;
		if (PlayerPrefs.GetInt ("Lesson") == 1)
			StartCoroutine (Lesson1 ());

		if (PlayerPrefs.GetInt ("Lesson") == 2)
			StartCoroutine (Lesson2 ());

		if (PlayerPrefs.GetInt ("Lesson") == 3)
			StartCoroutine (Lesson3 ());

		if (PlayerPrefs.GetInt ("Lesson") == 4)
			StartCoroutine (Lesson4 ());

		if (PlayerPrefs.GetInt ("Lesson") == 5)
			StartCoroutine (Lesson5 ());

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (heroSpawnedGems >= 3)
			heroSpawnedGems = 0;

		if (enemySpawnedGems >= 3)
			enemySpawnedGems = 0;

		if (gempertime >= 2) {
			gempertime = 0;
			Diamonds += 1;
			EnemyDiamonds += 1;
		} else {
			gempertime += Time.deltaTime;
		}

	}

	public void MiningGem(int x){

		Diamonds += x;

	}

	public void NextLesson(){
		if (PlayerPrefs.GetInt ("Lesson") == 1) {//Capturando a Base
			PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}else if (PlayerPrefs.GetInt ("Lesson") == 2) {//Atacando Heroi
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")){
				Destroy (o.gameObject);
			}
			PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);
			GameObject.Find ("New Sprite").SetActive (false);
			GetComponent<GameController> ().OpenReward (0);
			//RewardWindow.SetActive (true);
		}else if (PlayerPrefs.GetInt ("Lesson") == 3) {//RecebendoRecompensa
			if (PlayerPrefs.GetString ("ResetCards") != "false") {
				PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
				PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
			}
			if (PlayerPrefs.GetString ("TutorialCoins") != "true") {
				PlayerPrefs.SetString ("TutorialCoins", "true");
				PlayerPrefs.SetInt ("PlayerCoins", 1000);
			}
			PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);
			SceneManager.LoadScene ("TutorialMain");
		}else if (PlayerPrefs.GetInt ("Lesson") == 5) {//RecebendoRecompensa
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("CardSlots")){
				Destroy (o.gameObject);
			}
			foreach(GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")){
				Destroy (o.gameObject);
			}
			PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);
			GameObject.Find ("New Sprite").SetActive (false);
			GetComponent<GameController> ().OpenReward (1);
			//RewardWindow2.SetActive (true);
		}else if (PlayerPrefs.GetInt ("Lesson") == 6) {//RecebendoRecompensa
			PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);
			SceneManager.LoadScene ("Main");
		}

	}

	public void CloseConfirmation(){
		Time.timeScale = 1;
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")){
			Destroy (o.gameObject);
		}
	}

	public void OpenConfirmation(){
		Time.timeScale = 0;
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")){
			Destroy (o.gameObject);
		}
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

	void OnApplicationQuit() {
		PlayerPrefs.SetInt ("Lesson", 1);

		//PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
		//PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}

	IEnumerator Lesson1(){
		yield return new WaitForSeconds (1);
		tutorialPanels [0].SetActive (true);
		yield return new WaitForSeconds (5);
		tutorialPanels [0].SetActive (false);
		tutorialPanels [1].SetActive (true);
		interfaceElements [0].SetActive (true);
		interfaceElements [1].SetActive (true);
		WaypointSystem.GetComponent<BoxCollider2D> ().enabled = true;
	}

	IEnumerator Lesson2(){
		HeroEnemy.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		HeroEnemy.GetComponent<WPIASoldierControler> ().vida = 3;
		HeroEnemy.GetComponent<WPIASoldierControler> ().UpdateLife();
		yield return new WaitForSeconds (1);
		tutorialPanels [2].SetActive (true);
		interfaceElements [2].SetActive (true);
		WaypointSystem.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (5);
		tutorialPanels [2].SetActive (false);

	}

	IEnumerator Lesson3(){
		
		yield return new WaitForSeconds (1);
	}

	IEnumerator Lesson4(){
		yield return new WaitForSeconds (1);
	}

	IEnumerator Lesson5(){
		portraitPanel.SetActive (true);
		yield return new WaitForSeconds (1);
		TutorialGem.GetComponent<GemScript> ().EnableSingleGem ();
		tutorialPanels [3].SetActive (true);
		WaypointSystem.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (5);
		tutorialPanels [3].SetActive (false);

	}

	IEnumerator Lesson6(){
		yield return new WaitForSeconds (2);
		tutorialPanels [4].SetActive (true);
		yield return new WaitForSeconds (5);
		tutorialPanels [4].SetActive (false);
	}

	public void StartTutorialEnding(){
		HeroEnemy.SetActive (true);
		DeckPile.SetActive (true);
		StartCoroutine (Lesson6 ());
	}



}
