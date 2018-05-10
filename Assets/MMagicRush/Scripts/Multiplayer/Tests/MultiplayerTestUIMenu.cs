﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class MultiplayerTestUIMenu : MonoBehaviour {
    public Text DisplayName;
    public Text Username;
	public Text UsernameBatalha;
	public Text UsernameBatalhaMulti;
    public Text Opponent;

    public Button QuickGame;
    public Text QuickGameText;

	public GameObject VsIAPannel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnQuickGameClick() {
        QuickGameText.text = "Searching...";
		//StartCoroutine (TimeLimit());
        //QuickGame.interactable = false;
    }

    public void SetDisplayName(string displayname) {
       // DisplayName.text = "DisplayName: " + displayname;
		DisplayName.text = displayname;
    }

    public void SetUserName(string username) {
       // Username.text = "Username: " + username;
		Username.text = username;
		UsernameBatalha.text = username;
		UsernameBatalhaMulti.text = username;
    }

    public void SetOpponentName(string opponent) {
      //  Opponent.text = "Opponent: " + opponent;
		Opponent.text = opponent;
    }

    public void OnLogin(string displayname, string username) {
        SetDisplayName(displayname);
        SetUserName(username);
    }

    public void OnRoomConnected() {
        SetOpponentName(NetworkSessionManager.Instance.Match.Opponent.DisplayName);
    }

    public void OnEnable() {
        PlayGamesSignIn.OnLogin += OnLogin;
        NetworkSessionManager.RoomConnectedSuccessEvent += OnRoomConnected;
    }

    public void OnDisable() {
        PlayGamesSignIn.OnLogin -= OnLogin;
        NetworkSessionManager.RoomConnectedSuccessEvent -= OnRoomConnected;
    }

	IEnumerator TimeLimit(){

		yield return new WaitForSeconds (3);
		VsIAPannel.SetActive (true);
	}
}
