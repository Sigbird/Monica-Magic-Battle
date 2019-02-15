using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHelper : MonoBehaviour {
	public bool tutorial;
	//public AudioClip bgMusic;
	public int[] temp;
	public int[] empty;
	public Text coinsText;
	public Text premiumcoinsText;
	public int coinsPurchasing;
	public int premiumcoinsPurchasing;
	public dreamloLeaderBoard LeaderBoard;
	public GameObject ExitConfirmation;
	public GameObject notEnouthCoins;


	public GameObject[] TutorialPanels;

	public int[] zero;

	public float[][] cards = new float[19][];
	// Use this for initialization

 	// Use this for initialization
	void Start () {

//		cards [0] = new float[6];
//		cards [1] = new float[6];
//		cards [2] = new float[6];
//		cards [3] = new float[6];
//		cards [4] = new float[6];
//		cards [5] = new float[6];
//		cards [6] = new float[6];
//		cards [7] = new float[6];
//		cards [8] = new float[6];
//		cards [9] = new float[6];
//		cards [10] = new float[6];
//		cards [11] = new float[6];
//		cards [12] = new float[6];
//		cards [13] = new float[6];
//		cards [14] = new float[6];
//		cards [15] = new float[6];
//		cards [16] = new float[6];
//		cards [17] = new float[6];
		if (tutorial == false) {
			cards [0] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [1] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [2] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [3] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [4] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [5] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [6] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [7] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [8] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [9] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [10] = new float[6]{ 0, 0, 0, 0, 0, 0 };
			cards [11] = new float[6]{ 3, 0, 1, 1, 5, 1 };
			cards [12] = new float[6]{ 2, 0, 1, 1, 5, 1 };
			cards [13] = new float[6]{ 2, 1, 1, 0, 2, 1 };
			cards [14] = new float[6]{ 2, 1, 1, 0, 4, 1 };
			cards [15] = new float[6]{ 2, 0, 1, 1, 4, 1 };
			cards [16] = new float[6]{ 6, 3, 1, 0, 4, 1 };
			cards [17] = new float[6]{ 6, 0, 1, 6, 4, 1 };
			cards [18] = new float[6]{ 6, 0, 1, 6, 4, 1 };




			GetCardValues ();
		}
		//PlayerPrefsX.SetIntArray ("PlayerCardsIDs", zero);
		//fPlayerPrefsX.SetIntArray ("SelectedCardsIDs", zero);

		//PlayerPrefs.SetInt ("Lesson", 6);
		Time.timeScale = 1;

//		PlayerPrefs.SetInt ("PlayerCoinsPremium", 0);
//		PlayerPrefs.SetInt ("PlayerCoins", 0);

		//PlayerPrefs.SetInt ("PlayerCoins", 200);

		temp = new int[10];
//		Camera.main.gameObject.GetComponent<AudioSource> ().loop = true;
//		Camera.main.gameObject.GetComponent<AudioSource> ().clip = bgMusic;
//		Camera.main.gameObject.GetComponent<AudioSource> ().Play ();

//		if (PlayerPrefs.HasKey ("GameVolume") == false) {
//			PlayerPrefs.SetFloat ("GameVolume",1);
//		}

//		if (PlayerPrefs.GetFloat ("GameVolume") != null) {
//			audioSlider.value = PlayerPrefs.GetFloat ("GameVolume");
//		} else {
//			PlayerPrefs.SetFloat ("GameVolume",1);
//		}
//
//		if (PlayerPrefs.GetFloat ("GameVolumeEffects") != null) {
//			audioSlider.value = PlayerPrefs.GetFloat ("GameEffects");
//		} else {
//			PlayerPrefs.SetFloat ("GameVolumeEffects",1);
//		}

//		for (int i = 0; i < 10; i++) {
//			temp[i] = Random.Range(1,22);
//		}
		temp[0] = 11;

//		if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length <= 0) {
//			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", temp);
//			if (temp.Length < 15) {
//				PlayerPrefsX.SetIntArray ("SelectedCardsIDs", temp);
//			}
//			Debug.Log (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length);
//		} else {
//			if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length > 0 || PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length <= 15) {
//				PlayerPrefsX.SetIntArray ("SelectedCardsIDs", PlayerPrefsX.GetIntArray ("PlayerCardsIDs"));
//			}
//			temp = empty;
//		}
		 
		//PlayerPrefs.DeleteAll ();

		Debug.Log (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length);

		//if (PlayerPrefs.GetString ("Tutorial") == "False" || PlayerPrefs.HasKey ("Tutorial") == false) {
			StartCoroutine (StartingCards ());
			//			PurchaseCardBidu ();
//			PurchaseCardCanja ();
		//}

		PlayerPrefs.SetString ("Tutorial", "True");
		if(tutorial == true)
		GameObject.Find ("Canvas").GetComponent<Animator> ().SetTrigger ("menuCartas");

		if (PlayerPrefs.GetInt ("Lesson") == 7) {
			TutorialPanels [0].SetActive (true);
			EarnCard (11);
			PlayerPrefs.SetInt ("Lesson", 8);
		}
	}
	
	// Update is called once per frame
	void Update () {




		if (PlayerPrefs.GetInt ("PlayerCoins") > 0) {
			coinsText.text = PlayerPrefs.GetInt ("PlayerCoins").ToString ();
		} else {
			coinsText.text = "0";
		}

		if (PlayerPrefs.GetInt ("PlayerCoinsPremium") > 0) {
			premiumcoinsText.text = PlayerPrefs.GetInt ("PlayerCoinsPremium").ToString ();
		} else {
			premiumcoinsText.text = "0";
		}

		if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length >= 2 && tutorial == true) {
			TutorialPanels [1].SetActive (false);
			TutorialPanels [2].SetActive (true);
 		}

		if (Input.GetKey (KeyCode.Escape)) {
			ExitConfirmation.SetActive (true);
		}

	}

	public void OpenCoinsShop(){
		StartCoroutine (LateCoinsShop ());
	}

	IEnumerator LateCoinsShop(){
		yield return new WaitForSeconds (0.2f);
		this.coinsPurchasing = 0;
		this.premiumcoinsPurchasing = 0;
	}

	public void SetCoinsPurchasing(int x){
		this.coinsPurchasing = x;
	}

	public void SetPremiumCoinsPurchasing(int x){
		this.premiumcoinsPurchasing = x;
	}

	public void CoinPurchase() {
		int c = PlayerPrefs.GetInt ("PlayerCoins");
		int pc = PlayerPrefs.GetInt ("PlayerCoinsPremium");
		if(pc >= (coinsPurchasing/2)){
		PlayerPrefs.SetInt ("PlayerCoinsPremium", (pc - (coinsPurchasing/2)));
		PlayerPrefs.SetInt ("PlayerCoins", c + coinsPurchasing);
		}
	}

	public void PremiumCoinPurchase() {
		int pc = PlayerPrefs.GetInt ("PlayerCoinsPremium");
		PlayerPrefs.SetInt ("PlayerCoinsPremium", pc + premiumcoinsPurchasing);
	}

	public void LoadScene(string scene) {
		YupiPlay.SceneLoadingManager.LoadScene(scene);
	}

	public void CallScene(string scene) {
		SceneManager.LoadScene (scene);
	}

	public void NextLesson() {
		PlayerPrefs.SetInt ("Lesson", PlayerPrefs.GetInt ("Lesson") + 1);//5
		SceneManager.LoadScene ("TutorialScene");
	}

	public void Exit() {
		System.Diagnostics.Process.GetCurrentProcess().Kill();
	}

	public void Suspend(){
		Application.Quit();
	}

	public void PurchaseHero(string x){
		int pc = PlayerPrefs.GetInt ("PlayerCoinsPremium");
		if (pc >= 25) {
			PlayerPrefs.SetInt (x, 1);
			PlayerPrefs.SetInt ("PlayerCoinsPremium", (pc - 25));
		} else {
			notEnouthCoins.SetActive (true);
		}
	}

	public void CallTutorial(){
		PlayerPrefs.SetString ("ResetCards", "false");
		PlayerPrefs.SetInt ("Lesson", 1);
		SceneManager.LoadScene ("TutorialScene");
	}

	public void ToggleMusicOn(bool toggle){
		if (toggle == true) {
			PlayerPrefs.SetFloat ("GameVolume", 1);
		} else {
			PlayerPrefs.SetFloat ("GameVolume", 0);
		}
	}

	public void ToggleEffect(bool toggle){
		if (toggle == true) {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 1);
		} else {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 0);
		}
	}

	void OnApplicationQuit(){
		PlayerPrefs.SetInt ("Lesson", 1);
//		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
//		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}

	public void EarnCard(int cardID){
				int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

				List<int> iList = new List<int> ();

				for (int i = 0; i < original.Length; i++) {
					iList.Add (original [i]);
				}

				iList.Add (cardID);

				int[] finalArray = new int[iList.Count];

				int x = 0;
				foreach (int i in iList) {
					finalArray [x] = i;
					x++;
				}


				//		int[] finalArray = new  int[original.Length + 1 ];
				//
				//		for(int i = 0; i < original.Length; i ++ ) {
				//			finalArray[i] = original[i];
				//		}
				//
				//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;


				//ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
				PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);

				if (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length <= 15) {
				ActiveCard (cardID);
				}
	}

	public void ActiveCard(int cardID){
		
		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Add (cardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		//		int[] finalArray = new  int[original.Length + 1 ];
		//
		//		for(int i = 0; i < original.Length; i ++ ) {
		//			finalArray[i] = original[i];
		//		}

		//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	IEnumerator StartingCards(){
		PurchaseCardBidu ();
		yield return new WaitForSeconds (0.1f);
		PurchaseCardCanja ();
	}

	public void PurchaseCardBidu(){

		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		List<int> iList = new List<int> ();

		for (int i = 0; i < original.Length; i++) {
			iList.Add (original [i]);
		}

		iList.Add (11);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}
			
		//ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);



		//lastCard.GetComponent<CardScript> ().SetActiveButton ();
		int[] original2 = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList2 = new List<int>();

		for(int i = 0; i < original2.Length; i ++ ) {
			iList2.Add (original2 [i]);
		}

		iList2.Add (11);

		int[] finalArray2 = new int[iList2.Count];

		int y = 0;
		foreach (int i in iList) {
			finalArray2 [y] = i;
			y++;
		}


		//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray2);

	}

	public void PurchaseCardCanja(){

		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		List<int> iList = new List<int> ();

		for (int i = 0; i < original.Length; i++) {
			iList.Add (original [i]);
		}

		iList.Add (3);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		//ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);


		//lastCard.GetComponent<CardScript> ().SetActiveButton ();
		int[] original2 = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList2 = new List<int>();

		for(int i = 0; i < original2.Length; i ++ ) {
			iList2.Add (original2 [i]);
		}

		iList2.Add (3);

		int[] finalArray2 = new int[iList2.Count];

		int y = 0;
		foreach (int i in iList) {
			finalArray2 [y] = i;
			y++;
		}


		//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray2);
	}

	public void SetDificulty(int x){
		PlayerPrefs.SetInt ("Dificulty", x);
	}

	public void SetCardValues(){





//		cards [0] [0] = 0;
//		cards [0] [1] = 0;
//		cards [0] [2] = 0;
//		cards [0] [3] = 0;
//		cards [0] [4] = 0;
//		cards [0] [5] = 0;
//
//		//NEVASCA
//		cards [1] [0] = 0;
//		cards [1] [1] = 0;
//		cards [1] [2] = 0;
//		cards [1] [3] = 0;
//		cards [1] [4] = 0;
//		cards [1] [5] = 0;
//
//		//ESTALO
//		cards [2] [0] = 0;
//		cards [2] [1] = 0;
//		cards [2] [2] = 0;
//		cards [2] [3] = 0;
//		cards [2] [4] = 0;
//		cards [2] [5] = 0;
//
//		//CANJA
//		cards [3] [0] = 0;
//		cards [3] [1] = 0;
//		cards [3] [2] = 0;
//		cards [3] [3] = 0;
//		cards [3] [4] = 0;
//		cards [3] [5] = 0;
//
//
//		//ESPLOSAO
//		cards [4] [0] = 0;
//		cards [4] [1] = 0;
//		cards [4] [2] = 0;
//		cards [4] [3] = 0;
//		cards [4] [4] = 0;
//		cards [4] [5] = 0;
//
//		//TERREMOTO
//		cards [5] [0] = 0;
//		cards [5] [1] = 0;
//		cards [5] [2] = 0;
//		cards [5] [3] = 0;
//		cards [5] [4] = 0;
//		cards [5] [5] = 0;
//
//		//SONECA
//		cards [6] [0] = 0;
//		cards [6] [1] = 0;
//		cards [6] [2] = 0;
//		cards [6] [3] = 0;
//		cards [6] [4] = 0;
//		cards [6] [5] = 0;
//
//		//REMEDIO
//		cards [7] [0] = 0;
//		cards [7] [1] = 0;
//		cards [7] [2] = 0;
//		cards [7] [3] = 0;
//		cards [7] [4] = 0;
//		cards [7] [5] = 0;
//
//		//ESCUDO
//		cards [8] [0] = 0;
//		cards [8] [1] = 0;
//		cards [8] [2] = 0;
//		cards [8] [3] = 0;
//		cards [8] [4] = 0;
//		cards [8] [5] = 0;
//
//		//GRITO
//		cards [9] [0] = 0;
//		cards [9] [1] = 0;
//		cards [9] [2] = 0;
//		cards [9] [3] = 0;
//		cards [9] [4] = 0;
//		cards [9] [5] = 0;
//
//		//SEMMUNICAO
//		cards [10] [0] = 0;
//		cards [10] [1] = 0;
//		cards [10] [2] = 0;
//		cards [10] [3] = 0;
//		cards [10] [4] = 0;
//		cards [10] [5] = 0;
//
//		//BIDU
//		cards [11] [0] = 3;//HP
//		cards [11] [1] = 0;//DMG
//		cards [11] [2] = 1;//AKSPD
//		cards [11] [3] = 1;//RANGEDDMG
//		cards [11] [4] = 5;//SPEED
//		cards [11] [5] = 1;//LVL
//
//		//ASTRONAUTA
//		cards [12] [0] = 2;
//		cards [12] [1] = 0;
//		cards [12] [2] = 1;
//		cards [12] [3] = 1;
//		cards [12] [4] = 5;
//		cards [12] [5] = 1;
//
//		//JOTALHAO
//		cards [13] [0] = 2;
//		cards [13] [1] = 1;
//		cards [13] [2] = 1;
//		cards [13] [3] = 0;
//		cards [13] [4] = 2;
//		cards [13] [5] = 1;
//
//		//PITECO
//		cards [14] [0] = 2;
//		cards [14] [1] = 1;
//		cards [14] [2] = 1;
//		cards [14] [3] = 0;
//		cards [14] [4] = 4;
//		cards [14] [5] = 1;
//
//		//PENADINHO
//		cards [15] [0] = 2;
//		cards [15] [1] = 0;
//		cards [15] [2] = 1;
//		cards [15] [3] = 1;
//		cards [15] [4] = 4;
//		cards [15] [5] = 1;
//
//		//SANSÃO
//		cards [16] [0] = 6;
//		cards [16] [1] = 3;
//		cards [16] [2] = 1;
//		cards [16] [3] = 0;
//		cards [16] [4] = 4;
//		cards [16] [5] = 1;
//
//		//MINGAU
//		cards [17] [0] = 6;
//		cards [17] [1] = 0;
//		cards [17] [2] = 1;
//		cards [17] [3] = 6;
//		cards [17] [4] = 4;
//		cards [17] [5] = 1;

	}

	public void GetCardValues(){
		Debug.Log("card3 ");

		if (PlayerPrefs.GetFloat  ("Card11") == 0) {
			PlayerPrefs.SetFloat ("Card11", cards [1][0]);
			PlayerPrefs.SetFloat ("Card12", cards [1][1]);
			PlayerPrefs.SetFloat ("Card13", cards [1][2]);
			PlayerPrefs.SetFloat ("Card14", cards [1][3]);
			PlayerPrefs.SetFloat ("Card15", cards [1][4]);
			PlayerPrefs.SetFloat ("Card16", cards [1][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card21") == 0) {
			PlayerPrefs.SetFloat ("Card21", cards [2][0]);
			PlayerPrefs.SetFloat ("Card22", cards [2][1]);
			PlayerPrefs.SetFloat ("Card23", cards [2][2]);
			PlayerPrefs.SetFloat ("Card24", cards [2][3]);
			PlayerPrefs.SetFloat ("Card25", cards [2][4]);
			PlayerPrefs.SetFloat ("Card26", cards [2][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card31") == 0) {
			PlayerPrefs.SetFloat ("Card31", cards [3][0]);
			PlayerPrefs.SetFloat ("Card32", cards [3][1]);
			PlayerPrefs.SetFloat ("Card33", cards [3][2]);
			PlayerPrefs.SetFloat ("Card34", cards [3][3]);
			PlayerPrefs.SetFloat ("Card35", cards [3][4]);
			PlayerPrefs.SetFloat ("Card36", cards [3][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card41") == 0) {
			PlayerPrefs.SetFloat ("Card41", cards [4][0]);
			PlayerPrefs.SetFloat ("Card42", cards [4][1]);
			PlayerPrefs.SetFloat ("Card43", cards [4][2]);
			PlayerPrefs.SetFloat ("Card44", cards [4][3]);
			PlayerPrefs.SetFloat ("Card45", cards [4][4]);
			PlayerPrefs.SetFloat ("Card46", cards [4][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card51") == 0) {
			PlayerPrefs.SetFloat ("Card51", cards [5][0]);
			PlayerPrefs.SetFloat ("Card52", cards [5][1]);
			PlayerPrefs.SetFloat ("Card53", cards [5][2]);
			PlayerPrefs.SetFloat ("Card54", cards [5][3]);
			PlayerPrefs.SetFloat ("Card55", cards [5][4]);
			PlayerPrefs.SetFloat ("Card56", cards [5][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card61") == 0) {
			PlayerPrefs.SetFloat ("Card61", cards [6][0]);
			PlayerPrefs.SetFloat ("Card62", cards [6][1]);
			PlayerPrefs.SetFloat ("Card63", cards [6][2]);
			PlayerPrefs.SetFloat ("Card64", cards [6][3]);
			PlayerPrefs.SetFloat ("Card65", cards [6][4]);
			PlayerPrefs.SetFloat ("Card66", cards [6][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card71") == 0) {
			PlayerPrefs.SetFloat ("Card71", cards [7][0]);
			PlayerPrefs.SetFloat ("Card72", cards [7][1]);
			PlayerPrefs.SetFloat ("Card73", cards [7][2]);
			PlayerPrefs.SetFloat ("Card74", cards [7][3]);
			PlayerPrefs.SetFloat ("Card75", cards [7][4]);
			PlayerPrefs.SetFloat ("Card76", cards [7][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card81") == 0) {
			PlayerPrefs.SetFloat ("Card81", cards [8][0]);
			PlayerPrefs.SetFloat ("Card82", cards [8][1]);
			PlayerPrefs.SetFloat ("Card83", cards [8][2]);
			PlayerPrefs.SetFloat ("Card84", cards [8][3]);
			PlayerPrefs.SetFloat ("Card85", cards [8][4]);
			PlayerPrefs.SetFloat ("Card86", cards [8][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card91") == 0) {
			PlayerPrefs.SetFloat ("Card91", cards [9][0]);
			PlayerPrefs.SetFloat ("Card92", cards [9][1]);
			PlayerPrefs.SetFloat ("Card93", cards [9][2]);
			PlayerPrefs.SetFloat ("Card94", cards [9][3]);
			PlayerPrefs.SetFloat ("Card95", cards [9][4]);
			PlayerPrefs.SetFloat ("Card96", cards [9][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card101") == 0) {
			PlayerPrefs.SetFloat ("Card101", cards [10][0]);
			PlayerPrefs.SetFloat ("Card102", cards [10][1]);
			PlayerPrefs.SetFloat ("Card103", cards [10][2]);
			PlayerPrefs.SetFloat ("Card104", cards [10][3]);
			PlayerPrefs.SetFloat ("Card105", cards [10][4]);
			PlayerPrefs.SetFloat ("Card106", cards [10][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card111") == 0) {
			PlayerPrefs.SetFloat ("Card111", cards [11][0]);
			PlayerPrefs.SetFloat ("Card112", cards [11][1]);
			PlayerPrefs.SetFloat ("Card113", cards [11][2]);
			PlayerPrefs.SetFloat ("Card114", cards [11][3]);
			PlayerPrefs.SetFloat ("Card115", cards [11][4]);
			PlayerPrefs.SetFloat ("Card116", cards [11][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card121") == 0) {
			PlayerPrefs.SetFloat ("Card121", cards [12][0]);
			PlayerPrefs.SetFloat ("Card122", cards [12][1]);
			PlayerPrefs.SetFloat ("Card123", cards [12][2]);
			PlayerPrefs.SetFloat ("Card124", cards [12][3]);
			PlayerPrefs.SetFloat ("Card125", cards [12][4]);
			PlayerPrefs.SetFloat ("Card126", cards [12][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card131") == 0) {
			PlayerPrefs.SetFloat ("Card131", cards [13][0]);
			PlayerPrefs.SetFloat ("Card132", cards [13][1]);
			PlayerPrefs.SetFloat ("Card133", cards [13][2]);
			PlayerPrefs.SetFloat ("Card134", cards [13][3]);
			PlayerPrefs.SetFloat ("Card135", cards [13][4]);
			PlayerPrefs.SetFloat ("Card136", cards [13][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card141") == 0) {
			PlayerPrefs.SetFloat ("Card141", cards [14][0]);
			PlayerPrefs.SetFloat ("Card142", cards [14][1]);
			PlayerPrefs.SetFloat ("Card143", cards [14][2]);
			PlayerPrefs.SetFloat ("Card144", cards [14][3]);
			PlayerPrefs.SetFloat ("Card145", cards [14][4]);
			PlayerPrefs.SetFloat ("Card146", cards [14][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card151") == 0) {
			PlayerPrefs.SetFloat ("Card151", cards [15][0]);
			PlayerPrefs.SetFloat ("Card152", cards [15][1]);
			PlayerPrefs.SetFloat ("Card153", cards [15][2]);
			PlayerPrefs.SetFloat ("Card154", cards [15][3]);
			PlayerPrefs.SetFloat ("Card155", cards [15][4]);
			PlayerPrefs.SetFloat ("Card156", cards [15][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card161") == 0) {
			PlayerPrefs.SetFloat ("Card161", cards [16][0]);
			PlayerPrefs.SetFloat ("Card162", cards [16][1]);
			PlayerPrefs.SetFloat ("Card163", cards [16][2]);
			PlayerPrefs.SetFloat ("Card164", cards [16][3]);
			PlayerPrefs.SetFloat ("Card165", cards [16][4]);
			PlayerPrefs.SetFloat ("Card166", cards [16][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card171")  == 0) {
			PlayerPrefs.SetFloat ("Card171", cards [17][0]);
			PlayerPrefs.SetFloat ("Card172", cards [17][1]);
			PlayerPrefs.SetFloat ("Card173", cards [17][2]);
			PlayerPrefs.SetFloat ("Card174", cards [17][3]);
			PlayerPrefs.SetFloat ("Card175", cards [17][4]);
			PlayerPrefs.SetFloat ("Card176", cards [17][5]);
		}

		if (PlayerPrefs.GetFloat  ("Card181")  == 0) {
			PlayerPrefs.SetFloat ("Card181", cards [18][0]);
			PlayerPrefs.SetFloat ("Card182", cards [18][1]);
			PlayerPrefs.SetFloat ("Card183", cards [18][2]);
			PlayerPrefs.SetFloat ("Card184", cards [18][3]);
			PlayerPrefs.SetFloat ("Card185", cards [18][4]);
			PlayerPrefs.SetFloat ("Card186", cards [18][5]);
		}
			
	}

}
