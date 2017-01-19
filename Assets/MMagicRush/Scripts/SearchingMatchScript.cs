using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YupiPlay;

public class SearchingMatchScript : MonoBehaviour {

	public bool noLoading;

	public Image AdvImage;

	public Sprite Image;

	void OnEnable() {
		StartCoroutine ("Finding");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Finding() {
		yield return new WaitForSeconds(2);
		print(Time.time);
		AdvImage.sprite = Image;
		yield return new WaitForSeconds(1);
		CallToBattle ();
	}

	public void CallToBattle(){
		if (noLoading) {
			SceneManager.LoadScene ("Jogo");
		} else {
			SceneLoadingManager.LoadScene ("Jogo");
		}


	}

}
