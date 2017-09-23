using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay;

public class MultiplayerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void QuickGame() {
#if UNITY_EDITOR
        NetworkSessionManager.Instance.Reset();
        NetworkSessionManager.Instance.Match = null;
        SceneTestHelper.LoadTestGame();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
		GoogleMultiplayer.QuickGame();
#endif
#if UNITY_IOS
#endif
    }

    public void Invite() {
#if UNITY_ANDROID && !UNITY_EDITOR
        GoogleMultiplayer.InviteToGame();
#endif
    }

    public void Inbox() {
#if UNITY_ANDROID && !UNITY_EDITOR
        GoogleMultiplayer.AcceptFromInbox();
#endif
    }

    public void Quit() {
#if UNITY_ANDROID && !UNITY_EDITOR
		GoogleMultiplayer.Quit();
#endif
    }
}
