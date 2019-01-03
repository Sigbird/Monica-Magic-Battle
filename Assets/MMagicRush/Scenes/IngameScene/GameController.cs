using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public bool GameOver = false;
	private bool tutorialending = false;
	public bool tutorial;
	public bool multiplayer;
	public float gempertime;	
	private float gempertimeprogress;
	private int gempertimeMaxValue;
	private string terraintype;
	public int terrain;
	public bool victory;

	public int playerCharges;
	public static float playerXp;

	public int enemyCharges;
	public static float enemyXp;

	public CardInfoScript cardInfo;
	public GameObject CardInfoWindow;

	public int enemySpawnedGems = 0;
	public int heroSpawnedGems = 0;

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

	public Sprite[] portraits;
	public Image heroPortrait;
	public Image enemyPortrait;
	public GameObject ExitPanel;

	public GameObject[] rewardWindows;
	public GameObject loadingPosObj;

	public dreamloLeaderBoard LeaderBoard;
	public List<dreamloLeaderBoard.Score> LeaderList;

	public GameObject EnemyGameObject;
	public GameObject HeroGameObject;
	public GameObject NetClock;
	public GameObject NetGameController;
	public bool vsIAMode;

	private int[] zero;
	// Use this for initialization
	void Awake() {
		if (terrain == 1 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 2 || terrain == 2 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 1 || terrain == 3 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 0) {
			this.GetComponent<AudioManager> ().boss = true;
		} else {
			this.GetComponent<AudioManager> ().boss = false;
		}



		terraintype = PlayerPrefs.GetString ("TerrainType");
		if (terraintype == "Forest") {
			terrain = 1;
			vsIAMode = false;
		} else if (terraintype == "Winter") {
			terrain = 2;
			vsIAMode = false;
		} else if (terraintype == "Dungeon") {
			terrain = 3;
			vsIAMode = false;
		} else {
			vsIAMode = true;
		}
		//PlayerPrefsX.SetIntArray("PlayerCardsIDs",zero);
		//PlayerPrefsX.SetIntArray("SelectedCardsIDs",zero);

//		if (PlayerPrefs.GetString ("Multiplayer") == "True") {
//			if (tutorial == false) {
//				multiplayer = true;
//				NetGameController.SetActive (true);
//				NetClock.SetActive (true);
//				EnemyGameObject.GetComponent<EnemyRemoteController> ().enabled = true;
//				EnemyGameObject.GetComponent<WPIASoldierControler> ().multiplayer = true;
//				HeroGameObject.GetComponent<PlayerController> ().enabled = true;
//				HeroGameObject.GetComponent<WPSoldierControler> ().multiplayer = true;
//
//			}
//
//			EnemyGameObject.GetComponent<WPIASoldierControler> ().enabled = false;
//
//			HeroGameObject.GetComponent<WPSoldierControler> ().enabled = false;
//
//		} else {
//			if (tutorial == false) {
//				NetGameController.SetActive (false);
//				NetClock.SetActive (false);
//
//				EnemyGameObject.GetComponent<EnemyRemoteController> ().enabled = false;
//				EnemyGameObject.GetComponent<WPIASoldierControler> ().multiplayer = false;
//				HeroGameObject.GetComponent<PlayerController> ().enabled = false;
//				HeroGameObject.GetComponent<WPSoldierControler> ().multiplayer = false;
//			}
//
//			EnemyGameObject.GetComponent<WPIASoldierControler> ().enabled = true;
//		
//			HeroGameObject.GetComponent<WPSoldierControler> ().enabled = true;
//
//		}
		EnemyDiamonds = 0;
		Diamonds = 0;
		gempertimeprogress = 0;
		gempertimeMaxValue = 1;
		enemyCharges = PlayerPrefs.GetInt ("enemyCharges");
//		Debug.Log ("Enemy " + enemyCharges);
		playerCharges = PlayerPrefs.GetInt ("playerCharges");
//		Debug.Log ("Player " + playerCharges);
		round = PlayerPrefs.GetInt ("round");
//		Debug.Log ("Round " + round);
		if (enemyCharges == 0 && playerCharges == 0) {
			this.round = 1;
			GameController.playerXp = 0;
			GameController.enemyXp = 0;
		} else {
			GameController.playerXp = PlayerPrefs.GetFloat("PlayerXP");
			GameController.enemyXp = PlayerPrefs.GetFloat("EnemyXP");
		}

		heroPortrait.sprite = portraits [PlayerPrefs.GetInt ("SelectedCharacter")];

//		switch (PlayerPrefs.GetInt ("SelectedCharacter")) {
//		case 0:
//			heroPortrait.sprite = portraits [0];
//			break;
//		case 1:
//			heroPortrait.sprite = portraits [1];
//			break;
//		case 2:
//			heroPortrait.sprite = portraits [2];
//			break;
//		case 3:
//			heroPortrait.sprite = portraits [3];
//			break;
//		case 4:
//			heroPortrait.sprite = portraits [4];
//			break;
//		default:
//			heroPortrait.sprite = portraits [0];
//			break;
//		}
		if (LeaderBoard != null) {
			LeaderBoard.LoadScores ();
		}
		//StartCoroutine (EnemyAI ());
	}
	
	// Update is called once per frame
	void Update () {

		if (heroSpawnedGems >= 3)
			heroSpawnedGems = 0;

		if (enemySpawnedGems >= 3)
			enemySpawnedGems = 0;

		if (Diamonds >= 5 && tutorial == true && tutorialending == false) {
			GetComponent<TutorialController> ().StartTutorialEnding ();
			tutorialending = true;
		}

		if (gempertime >= 2 && tutorial == false && GameOver == false) {
			gempertime = 0;
			Diamonds += gempertimeMaxValue;
			EnemyDiamonds += gempertimeMaxValue;
			gempertimeprogress += 1;
		} else {
			gempertime += Time.deltaTime;
		}

		if (gempertimeprogress >= 20) {
			gempertimeprogress = 0;
			gempertimeMaxValue += 1;
		}

		if (Input.GetKey (KeyCode.Escape)) {
			ExitPanel.SetActive (true);
		}

		if (GameObject.Find ("Diamonds") != null)
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
//		Debug.Log ("xp: "+GameController.playerXp);
		PlayerPrefs.SetFloat ("PlayerXP", GameController.playerXp);
		PlayerPrefs.SetFloat ("EnemyXP", GameController.enemyXp);
		StartCoroutine (newRound ());

	}

	IEnumerator newRound(){
		yield return new WaitForSeconds (0.1f);
		StartCoroutine (endGame());
//		Debug.Log (enemyCharges);
//		if (enemyCharges == 1) {//3
//			PlayerPrefs.SetInt ("round", 1);
//			PlayerPrefs.SetInt ("playerCharges", 0);
//			PlayerPrefs.SetInt ("enemyCharges", 0);
//			StartCoroutine (endGame());
//		}else if(playerCharges == 1) {//3
//			PlayerPrefs.SetInt ("round", 1);
//			PlayerPrefs.SetInt ("playerCharges", 0);
//			PlayerPrefs.SetInt ("enemyCharges", 0);
//			StartCoroutine (endGame());
//		} else {
//			PlayerPrefs.SetInt ("round", round + 1);
//			yield return new WaitForSeconds (2);
//			SceneManager.LoadScene ("Jogo");
//		}

	}


	IEnumerator endGame(){
		CardInfoWindow.SetActive (false);
		yield return new WaitForSeconds (1);

		if (playerCharges == 1 && enemyCharges <= 0) {//3 2
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("victory");
			victory = true;
			endGamePanel [0].SetActive (true);
			if (tutorial == true) {
				GetComponent<TutorialController> ().tutorialPanels [0].SetActive (false);
				GetComponent<TutorialController> ().tutorialPanels [1].SetActive (false);
				GetComponent<TutorialController> ().tutorialPanels [2].SetActive (false);
			}
		} else if (enemyCharges == 1 && playerCharges <= 0) {//3 2
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("defeat");
			victory = false;
			//GameObject.Find ("Chest2").GetComponent<Button> ().interactable = false;
			//GameObject.Find ("Chest2").transform.Find ("Closed").GetComponent<Image> ().color = Color.gray;
			endGamePanel [1].SetActive (true);
		} else { // EMPATE
//			Debug.Log ("EMPATE");
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("selecaocarta");
			victory = true;
			endGamePanel [0].SetActive (true);

		}
		GameOver = true;
		Time.timeScale = 0;
	}

	public void EndGameMulti(int x){
		StartCoroutine (endGameMulti (x));
	}

	IEnumerator endGameMulti(int x){
		CardInfoWindow.SetActive (false);
		yield return new WaitForSeconds (1);

		if (x==0) {//3 2
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("victory");
			endGamePanel [0].SetActive (true);
			if (tutorial == true) {
				GetComponent<TutorialController> ().tutorialPanels [0].SetActive (false);
				GetComponent<TutorialController> ().tutorialPanels [1].SetActive (false);
				GetComponent<TutorialController> ().tutorialPanels [2].SetActive (false);
			}
		} else if (x==1) {//3 2
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("defeat");
			//GameObject.Find ("Chest2").GetComponent<Button> ().interactable = false;
			//GameObject.Find ("Chest2").transform.Find ("Closed").GetComponent<Image> ().color = Color.gray;
			endGamePanel [1].SetActive (true);
		} else { // EMPATE
//			Debug.Log ("EMPATE");
			this.GetComponent<AudioManager> ().StopAudio ();
			this.GetComponent<AudioManager> ().SetVolume (1);
			this.GetComponent<AudioManager> ().PlayAudio ("victory");
			endGamePanel [0].SetActive (true);
		}
		GameOver = true;
		Time.timeScale = 0;
	}

	void OnApplicationQuit() {
		PlayerPrefs.SetInt ("Lesson", 1);

		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);

		//PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
		//PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}



	public void OpenReward(int x){
		GameObject.Find ("Hero").SetActive (false);
		if(GameObject.Find ("HeroEnemy") != null)
		GameObject.Find ("HeroEnemy").SetActive (false);
		GameObject.Find ("HeroBase").SetActive (false);
		GameObject.Find ("HeroBaseEnemy").SetActive (false);

		Time.timeScale = 1;

		foreach(GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")){
			Destroy (o.gameObject);
		}
		if (multiplayer == true && tutorial==false) {
			rewardWindows [1].SetActive (true);
			if (x == 5) {
				StartCoroutine (IncrementRanking (Random.Range(-25,-50)));
				//IncrementLeaderBoard (-50);
			} else {
				StartCoroutine (IncrementRanking (Random.Range(25,50)));
				//IncrementLeaderBoard (50);
			}
			//CheckPlayerPos ();
		} else if (tutorial == true) {
			rewardWindows [x].SetActive (true);
			if (x == 0) {
				//GiveReward (3);
				//Debug.Log ("Deu Bidu");
			} else {
				GiveReward (4);
			}
		} else {
			rewardWindows [2].SetActive (true);
		}
	}

	IEnumerator IncrementRanking(int x){
		IncrementLeaderBoard (x);
		yield return new WaitForSeconds (1);
		CheckPlayerPos ();
		loadingPosObj.SetActive (false);
	}

	public void GiveReward(int x){

		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		List<int> iList = new List<int> ();

		for (int i = 0; i < original.Length; i++) {
			//iList.Add (original [i]);
			iList.Insert (i, original [i]);
		}

		switch (x) {
		case 1:
			PlayerPrefs.SetInt ("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins")+100);
			break;
		case 2:
			PlayerPrefs.SetInt ("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins")+25);
			break;
		case 3: // BIDU
			iList.Add (11);

			PlayerPrefsX.SetIntArray("PlayerCardsIDs", iList.ToArray());
			break;
		case 4://Sansão
			iList.Add (16);

			PlayerPrefsX.SetIntArray("PlayerCardsIDs", iList.ToArray());
			break;
		case 5:
			break;
		default:
			break;
		}
	}

	public void GiveCard(int x){
		int[] temp = new int[10];
		temp [0] = x;
//		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", temp);
//		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", temp);
	}




	public void ClearThisGame(){
		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);
	}

	public void IncrementLeaderBoard(int x){
		int tempScore = 0;
		LeaderList = LeaderBoard.ToListHighToLow ();
		for (int i = 0; i < LeaderList.Count; i++) {
			if (LeaderList [i].playerName == PlayerPrefs.GetString ("PlayerName")) {
				if (x >= 0) {
					LeaderBoard.AddScore (LeaderList [i].playerName, LeaderList [i].score + x);
				} else {
					tempScore = LeaderList [i].score + x;
					LeaderBoard.RemoveScore (LeaderList [i].playerName);
					LeaderBoard.AddScore (PlayerPrefs.GetString("PlayerName"), tempScore);
				}
			}
		}
	}

	public void CheckPlayerPos(){
		int lastpos = PlayerPrefs.GetInt ("LastPos");
		int actualpos = 0;
		LeaderList = LeaderBoard.ToListHighToLow ();
		for (int i = 0; i < LeaderList.Count; i++) {
			if (LeaderList [i].playerName == PlayerPrefs.GetString ("PlayerName")) {
				actualpos = i+1;
			}
		}
		if (actualpos <= 0) {
			actualpos = 1;
		}
		rewardWindows [1].GetComponent<RewardWindow> ().RankingPos.text = actualpos.ToString();
		if (actualpos > lastpos) {
			rewardWindows [1].GetComponent<RewardWindow> ().RankingStatus.sprite = rewardWindows [1].GetComponent<RewardWindow> ().RankingStatusImages [1];
		}
		if (actualpos < lastpos) {
			rewardWindows [1].GetComponent<RewardWindow> ().RankingStatus.sprite = rewardWindows [1].GetComponent<RewardWindow> ().RankingStatusImages [2];
		}
		if (actualpos == lastpos) {
			rewardWindows [1].GetComponent<RewardWindow> ().RankingStatus.sprite = rewardWindows [1].GetComponent<RewardWindow> ().RankingStatusImages [0];
		}
		if (lastpos == null) {
			rewardWindows [1].GetComponent<RewardWindow> ().RankingStatus.sprite = rewardWindows [1].GetComponent<RewardWindow> ().RankingStatusImages [0];
		}
		PlayerPrefs.SetInt ("LastPos", actualpos);

	}

	public void CallScene(string scene){
		Time.timeScale = 1;
		if (scene == "quit") {
			Application.Quit ();
		}else if (scene == "start") {
			Time.timeScale = 1;
		} else {
			SceneManager.LoadScene(scene);
		}
	}

	public void NextLevel(){
		int x = PlayerPrefs.GetInt ("ClearedLevels");
//		if (x < 11 && victory == true) {
//			x += 1;
//			PlayerPrefs.SetInt ("ClearedLevels", x);
//		}
		if (terrain == 1 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 2 && victory == true && vsIAMode == false ){
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			if (x < 12 && victory == true) {
				x += 1;
				PlayerPrefs.SetInt ("ClearedLevels", x);
			}
			PlayerPrefs.DeleteKey ("TerrainType");
			SceneManager.LoadScene ("Cinematics");


		} else if (terrain == 2 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 1 && victory == true && vsIAMode == false) {
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			if (x < 12 && victory == true) {
				x += 1;
				PlayerPrefs.SetInt ("ClearedLevels", x);
			}
			PlayerPrefs.DeleteKey ("TerrainType");
			SceneManager.LoadScene ("Cinematics");


		}else if(terrain == 3 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 0 && victory == true && vsIAMode == false){
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			if (x < 12 && victory == true) {
				x += 1;
				PlayerPrefs.SetInt ("ClearedLevels", x);
			}
			PlayerPrefs.DeleteKey ("TerrainType");
			SceneManager.LoadScene ("Cinematics");

		} else {
			if (vsIAMode == false) {
				if (x < 12 && victory == true) {
					x += 1;
					PlayerPrefs.SetInt ("ClearedLevels", x);
				}
				PlayerPrefs.DeleteKey ("TerrainType");
				SceneManager.LoadScene ("Level Select");
			} else {
				PlayerPrefs.DeleteKey ("TerrainType");
				SceneManager.LoadScene ("JogoOffline");
			}
		}
			

	}

	public void LoadScene(string scene){
		int x = PlayerPrefs.GetInt ("ClearedLevels");
		Time.timeScale = 1;
		round = 1;
		playerCharges = 0;
		enemyCharges = 0;
		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);
		if (terrain == 1 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 2 && victory == true && vsIAMode == false) {
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			SceneManager.LoadScene ("Cinematics");
		} else if (terrain == 2 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 1 && victory == true && vsIAMode == false) {
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			SceneManager.LoadScene ("Cinematics");
		}else if(terrain == 3 && EnemyGameObject.GetComponent<WPIASoldierControler> ().heroID == 0 && victory == true && vsIAMode == false){
			PlayerPrefs.SetInt ("AnimationToPlay", terrain);
			SceneManager.LoadScene ("Cinematics");
		} else {
			if (vsIAMode == false) {
				if (x < 12 && victory == true) {
					x += 1;
					PlayerPrefs.SetInt ("ClearedLevels", x);
				}
				SceneManager.LoadScene ("Level Select");
			} else {
				SceneManager.LoadScene (scene);
			}
		}
	}

	public void OpenUI(){
		if (multiplayer == false) {
			Time.timeScale = 0;
		}
		WPScript.UIopen = true;
	}

	public void CloseUI(){
		if (multiplayer == false) {
			Time.timeScale = 1;
		}
		//WPScript.UIopen = false;
		StartCoroutine (DelayUI (false));
	}

	IEnumerator DelayUI(bool x){

		yield return new WaitForSeconds (0.3f);
		WPScript.UIopen = x;
	}

}
