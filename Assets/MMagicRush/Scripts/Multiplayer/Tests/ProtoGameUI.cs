using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class ProtoGameUI : MonoBehaviour {
    public Text Player;
    public Text Enemy;
    public Text StartText;
    public Text EndText;

    public Text LivesCounter;

    public Text MessageDebug;
    public Text EnemyDebug;

    public Text LagIndicator;

    public static ProtoGameUI Instance;

    void Awake() {
        Instance = this;
    }

    void Start () {
#if !UNITY_EDITOR
        Player.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
        Enemy.text  = NetworkSessionManager.Instance.Match.Opponent.DisplayName;
#endif
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowStart() {
        StartText.gameObject.SetActive(true);
    }

    public void HideStart() {
        StartText.gameObject.SetActive(false);
    }

    public void ShowEnd() {
        EndText.gameObject.SetActive(true);
    }

    public void SetLives(int lives) {
        LivesCounter.text = "Lives: " + lives.ToString();
    }

    public void PrintPlayer(string msg) {
        MessageDebug.text = msg;
    }

    public void PrintEnemy(string msg) {
        EnemyDebug.text = msg;
    }

    public void PrintLag(ulong turn) {
        LagIndicator.text = "lag turn " + turn;
    }
}
