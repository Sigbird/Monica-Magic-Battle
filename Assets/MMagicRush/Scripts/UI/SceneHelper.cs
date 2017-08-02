using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHelper : MonoBehaviour {
	public bool tutorial;
	public AudioClip bgMusic;
	public int[] temp;
	public int[] empty;
	public Text coinsText;
	public int coinsPurchasing;


	public GameObject[] TutorialPanels;
 	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("Lesson", 6);
		Time.timeScale = 1;

		//PlayerPrefs.SetInt ("PlayerCoins", 200);

		temp = new int[10];
		Camera.main.gameObject.GetComponent<AudioSource> ().loop = true;
		Camera.main.gameObject.GetComponent<AudioSource> ().clip = bgMusic;
		Camera.main.gameObject.GetComponent<AudioSource> ().Play ();

//		for (int i = 0; i < 10; i++) {
//			temp[i] = Random.Range(1,22);
//		}
		temp[0] = 11;

		if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length <= 0) {
			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", temp);
			if (temp.Length < 15) {
				PlayerPrefsX.SetIntArray ("SelectedCardsIDs", temp);
			}
			Debug.Log (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length);
		} else {
			if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length > 0 || PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length <= 15) {
				PlayerPrefsX.SetIntArray ("SelectedCardsIDs", PlayerPrefsX.GetIntArray ("PlayerCardsIDs"));
			}
			temp = empty;
		}
		Debug.Log (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length);
		PlayerPrefs.SetString ("Tutorial", "True");
		if(tutorial == true)
		GameObject.Find ("Canvas").GetComponent<Animator> ().SetTrigger ("menuCartas");

		if (PlayerPrefs.GetInt ("Lesson") == 7) {
			TutorialPanels [0].SetActive (true);
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

		if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length >= 12 && tutorial == true) {
			TutorialPanels [1].SetActive (false);
			TutorialPanels [2].SetActive (true);
 		}

	}

	public void SetCoinsPurchasing(int x){
		this.coinsPurchasing = x;
	}

	public void CoinPurchase() {
		int c = PlayerPrefs.GetInt ("PlayerCoins");
		PlayerPrefs.SetInt ("PlayerCoins", c + coinsPurchasing);
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
		PlayerPrefs.SetInt (x, 1);
	}

	public void CallTutorial(){
		PlayerPrefs.SetString ("ResetCards", "false");
		PlayerPrefs.SetInt ("Lesson", 1);
		SceneManager.LoadScene ("TutorialScene");
	}

	void OnApplicationQuit(){
		PlayerPrefs.SetInt ("Lesson", 1);
//		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
//		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}
}
