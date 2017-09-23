using System;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class MultiplayerTestUIMenu : MonoBehaviour {
    public Text DisplayName;
    public Text Username;
    public Text Opponent;

    public Button QuickGame;
    public Text QuickGameText;
    public Text SendModeText;

	// Use this for initialization
	void Start () {
        UpdateSendModeButton();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            NetworkSessionManager.Instance.LeaveRoom();
            SceneTestHelper.LoadMenu();
        }
    }

    public void OnQuickGameClick() {
        QuickGameText.text = "Searching...";
        QuickGame.interactable = false;
    }

    public void SetDisplayName(string displayname) {
        DisplayName.text = "DisplayName: " + displayname;
    }

    public void SetUserName(string username) {
        Username.text = "Username: " + username;
    }

    public void SetOpponentName(string opponent) {
        Opponent.text = "Opponent: " + opponent;
    }

    public void OnLogin(string displayname, string username) {
        SetDisplayName(displayname);
        SetUserName(username);
    }

    public void OnRoomConnected() {
        SetOpponentName(NetworkSessionManager.Instance.Match.Opponent.DisplayName);
    }

    public void OnRoomConnectFailure() {
        NetworkSessionManager.Instance.LeaveRoom();
        QuickGame.interactable = true;
    }

    public void OnParticipantLeft(ParticipantInfo part) {
        NetworkSessionManager.Instance.LeaveRoom();
        QuickGame.interactable = true;
    }

    public void OnEnable() {
        PlayGamesSignIn.OnLogin += OnLogin;
        NetworkSessionManager.RoomConnectedSuccessEvent += OnRoomConnected;
        NetworkSessionManager.RoomConnectedFailureEvent += OnRoomConnectFailure;
        NetworkSessionManager.ParticipantLeftRoomEvent  += OnParticipantLeft;
    }

    public void OnDisable() {
        PlayGamesSignIn.OnLogin -= OnLogin;
        NetworkSessionManager.RoomConnectedSuccessEvent -= OnRoomConnected;
        NetworkSessionManager.RoomConnectedFailureEvent -= OnRoomConnectFailure;
        NetworkSessionManager.ParticipantLeftRoomEvent  -= OnParticipantLeft;
    }

    public void PlayAgainsAI() {
        var opponent = new ParticipantInfo((new Guid()).ToString(), "AIOpponent");
        NetworkSessionManager.Instance.Reset();
        NetworkSessionManager.Instance.SetAIMatch(ParticipantInfo.GetPlayerAgainstAI(), opponent, true);
        SceneTestHelper.LoadTestGame();
    }

    public void ToggleSendMode() {
        NetworkSessionManager.Instance.Reliable = !NetworkSessionManager.Instance.Reliable;
        UpdateSendModeButton();
    }

    public void UpdateSendModeButton() {
        if (NetworkSessionManager.Instance.Reliable) {
            SendModeText.text = "Reliable";
        } else {
            SendModeText.text = "Unreliable";
        }
    }    
}
