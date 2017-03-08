﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour {
	public AudioClip bgMusic;
	public int[] temp;
	public int[] empty;
	// Use this for initialization
	void Start () {
		temp = new int[10];
		Camera.main.gameObject.GetComponent<AudioSource> ().loop = true;
		Camera.main.gameObject.GetComponent<AudioSource> ().clip = bgMusic;
		Camera.main.gameObject.GetComponent<AudioSource> ().Play ();
		for (int i = 0; i < 10; i++) {
			temp[i] = Random.Range(1,29);
		}
		if (PlayerPrefsX.GetIntArray ("PlayerCardsIDs").Length <= 0) {
			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", temp);
		} else {
			temp = empty;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadScene(string scene) {
		YupiPlay.SceneLoadingManager.LoadScene(scene);
	}

	public void Exit() {
		Application.Quit ();
	}


	void OnApplicationQuit(){
		//PlayerPrefsX.SetIntArray ("PlayerCardsIDs", empty);
		//PlayerPrefsX.SetIntArray ("SelectedCardsIDs", empty);
	}
}
