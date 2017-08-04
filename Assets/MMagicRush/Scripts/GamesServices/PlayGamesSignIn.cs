using UnityEngine;
using YupiPlay.MMB;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesSignIn : MonoBehaviour {

    public delegate void OnLoginAction(string displayname, string username);
    public static event OnLoginAction OnLogin;

	// Use this for initialization
	void Start () {
		Social.localUser.Authenticate(OnAuth);        
	}

	private void OnAuth(bool success) {
		if (success) {
			Debug.Log("Auth OK");

            PlayerInfo.Instance.DisplayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            PlayerInfo.Instance.Username    = Social.localUser.userName;

            if (OnLogin != null) {
                OnLogin(PlayerInfo.Instance.DisplayName, PlayerInfo.Instance.Username);
            }

        } else {
			Debug.Log("Auth Fail");
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
