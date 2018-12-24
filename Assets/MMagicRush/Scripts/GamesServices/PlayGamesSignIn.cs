using UnityEngine;
using YupiPlay.MMB;
using GooglePlayGames;
using UnityEngine.UI;

public class PlayGamesSignIn : MonoBehaviour {

    public delegate void OnLoginAction(string displayname, string username);
    public static event OnLoginAction OnLogin;
	public Button bt_vsJogador;

	public dreamloLeaderBoard LeaderBoard;

	// Use this for initialization
	void Start () {
		//Social.localUser.Authenticate(OnAuth); 
		Login();
		LeaderBoard.LoadScores ();
	}

	private void OnReport(bool success) {
		if (success) {
			Debug.Log("Load OK");
		} else {
			Debug.Log("Load Fail");
		}
	}

	public static void Login()
	{
		if(Social.Active == null)
		{
			Debug.LogError("plataforma inativa");
			return;
		}
		if (Social.localUser.authenticated)
		{
			return; // verificando se já está logado
		}
		Social.localUser.Authenticate(OnAuth);
	}


	public static void OnAuth(bool success) {
		if (success) {
			Debug.Log("Auth OK");
			if (Social.localUser.authenticated) {
				Debug.Log("Auth OK");
			}
            PlayerInfo.Instance.DisplayName = PlayGamesPlatform.Instance.GetUserDisplayName();
            PlayerInfo.Instance.Username    = Social.localUser.userName;
			PlayerPrefs.SetString ("PlayerName", Social.localUser.userName);
			//Social.ReportScore (100, "CgkI4e_Ei7AREAIQBg",OnReport);
//			LeaderBoard.AddScore (Social.localUser.userName, 100);
//			bt_vsJogador.interactable = true;
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
