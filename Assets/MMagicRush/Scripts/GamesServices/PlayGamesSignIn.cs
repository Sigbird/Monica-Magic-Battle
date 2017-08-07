using UnityEngine;
using YupiPlay.MMB;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesSignIn : MonoBehaviour {

    public delegate void OnLoginAction(string displayname, string username);
    public static event OnLoginAction OnLogin;

	public dreamloLeaderBoard LeaderBoard;

	// Use this for initialization
	void Start () {
		Social.localUser.Authenticate(OnAuth);  
		LeaderBoard.LoadScores ();
	}

	private void OnReport(bool success) {
		if (success) {
			Debug.Log("Load OK");
		} else {
			Debug.Log("Load Fail");
		}
	}

	private void OnAuth(bool success) {
		if (success) {
			Debug.Log("Auth OK");

            PlayerInfo.Instance.DisplayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            PlayerInfo.Instance.Username    = Social.localUser.userName;
			//Social.ReportScore (100, "CgkI4e_Ei7AREAIQBg",OnReport);
			LeaderBoard.AddScore (Social.localUser.userName, 100);
            if (OnLogin != null) {
                OnLogin(PlayerInfo.Instance.DisplayName, PlayerInfo.Instance.Username);
            }

        } else {
			Debug.Log("Auth Fail");
		}


	}

	public void OpenRanking(){
//		if (Social.localUser.authenticated) {
//			Social.ShowLeaderboardUI ();
//		} else {
//			//Social.localUser.Authenticate(OnAuth);  
//		}


	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
