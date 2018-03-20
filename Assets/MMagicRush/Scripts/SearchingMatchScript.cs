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

	void OnEnable() {
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
			SceneManager.LoadScene ("JogoMulti");
		} else {
			SceneLoadingManager.LoadScene ("JogoMulti");
		}
	}

	public void RankedMatch(){
		PlayerPrefs.SetInt ("Ranked", 1);
	}

	public void UnRankedMatch(){
		PlayerPrefs.SetInt ("Ranked", 0);
	}

}
