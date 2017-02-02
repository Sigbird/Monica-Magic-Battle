using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayGamesSignIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Social.localUser.Authenticate(OnAuth);
	}

	private void OnAuth(bool success) {
		if (success) {
			Debug.Log("Auth OK");
		} else {
			Debug.Log("Auth Fail");
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
