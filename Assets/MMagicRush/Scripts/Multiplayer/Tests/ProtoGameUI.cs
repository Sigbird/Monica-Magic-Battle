using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class ProtoGameUI : MonoBehaviour {
    public Text Player;
	public Text PlayerUI;
    public Text Enemy;
    public Text StartText;
    public Text EndText;

    public Text LivesCounter;
    // Use this for initialization
    void Start () {
#if !UNITY_EDITOR
        Player.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
		PlayerUI.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
		//Enemy.text = NetworkSessionManager.Instance.Match.Opponent.DisplayName;
#endif
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowStart() {
       // StartText.gameObject.SetActive(true);
    }

    public void HideStart() {
       // StartText.gameObject.SetActive(false);
    }

    public void ShowEnd() {
       // EndText.gameObject.SetActive(true);
    }

    public void SetLives(int lives) {
       // LivesCounter.text = "Lives: " + lives.ToString();
    }
}
