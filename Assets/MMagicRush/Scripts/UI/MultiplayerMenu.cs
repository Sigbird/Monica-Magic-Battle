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
		#if UNITY_ANDROID
		GoogleMultiplayer.QuickGame();
		#endif
		#if UNITY_IOS
		#endif
	}

    public void Invite() {
        #if UNITY_ANDROID
        GoogleMultiplayer.InviteToGame();
        #endif
    }

    public void Inbox() {
        #if UNITY_ANDROID
        GoogleMultiplayer.AcceptFromInbox();
        #endif
    }
}
