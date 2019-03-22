using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YupiPlay;


public class SearchingMatchScript : MonoBehaviour {

	public bool noLoading;

	public Image AdvImage;

	public Image HeroImage;

	public GameObject LoadingAnim;

	public Sprite Image;

	public int SelectedCharacterID;

	public int[] selectedCardsIDs;

	public Sprite[] HeroPortraits;

	public bool cancelBattle;

	public bool multiplayer;

	public GameObject LoadingCanvas;

	public Scrollbar ProgressionBar;

	void OnEnable() {
//		if (multiplayer == false) {
			PlayerPrefs.SetString ("Multiplayer", "False");
//		} else {
//			PlayerPrefs.SetString ("Multiplayer", "True");
//		}
		cancelBattle = false;
		StartCoroutine ("Finding");
		this.SelectedCharacterID = PlayerPrefs.GetInt ("SelectedCharacter");

	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CancelSearch(){
		cancelBattle = true;
	}

	IEnumerator Finding() {
		yield return new WaitForSeconds(0.5f);
		//HeroImage.sprite = HeroPortraits [SelectedCharacterID + 1];
		yield return new WaitForSeconds(2);
		print(Time.time);
		AdvImage.sprite = Image;
		LoadingAnim.SetActive (false);
		yield return new WaitForSeconds(1);
		if (cancelBattle == false) {
			CallToBattle ();
		}
	}

	public void SetCharacterID(int x){
		PlayerPrefs.SetInt ("SelectedCharacter", x);
		//this.SelectedCharacterID = x;
	}

	public void CallToBattle(){
		PlayerPrefs.SetInt ("SelectedCharacter", SelectedCharacterID);
		PlayerPrefsX.SetIntArray ("Cards", selectedCardsIDs);
		PlayerPrefs.SetInt ("round",1);
		PlayerPrefs.SetInt ("playerCharges",0);
		PlayerPrefs.SetInt ("enemyCharges",0);
		if (noLoading) {
			SceneManager.LoadScene ("GamePlayReview");
			//SceneManager.LoadScene ("JogoOffline");
		} else {
			StartCoroutine (AsynchronousLoad ("GamePlayReview"));
			//SceneManager.LoadScene ("GamePlayReview");
			//SceneLoadingManager.LoadScene ("JogoOffline");
		}
	}

	public void RankedMatch(){
		PlayerPrefs.SetInt ("Ranked", 1);
	}

	public void UnRankedMatch(){
		PlayerPrefs.SetInt ("Ranked", 0);
	}

	IEnumerator AsynchronousLoad (string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (! ao.isDone)
		{
			LoadingCanvas.SetActive (true);
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");
			ProgressionBar.size = progress;
			// Loading completed
			if (ao.progress == 0.9f)
			{
				yield return new WaitForSeconds(1.0f);
				Debug.Log("Press a key to start");
				//if (Input.GetKeyDown(KeyCode.A))
//				if (LoadingCanvas != null) {
//					Destroy (LoadingCanvas.gameObject);
//				}
				ao.allowSceneActivation = true;
				//ao.isDone = true;
			}

			yield return null;
		}
	}

}
