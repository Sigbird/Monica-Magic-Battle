using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;
using YupiPlay.MMB.Lockstep;

public class ProtoGameUI : MonoBehaviour {
    public Text Player;
    public Text Enemy;
    public Text StartText;
    public Text EndText;

    public Text LivesCounter;

    public Text MessageDebug;
    public Text EnemyDebug;

    public Text LagIndicator;
    public Text ReliableLatency;
    public Text UnreliableLatency;

    public static ProtoGameUI Instance;

    void Awake() {
        Instance = this;
    }

    void Start () {        
        if (NetworkSessionManager.Instance.Match != null) {            
            Player.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
            Enemy.text = NetworkSessionManager.Instance.Match.Opponent.DisplayName;
        }
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

    public void PrintLagMsg(string msg) {
        LagIndicator.text = msg;
    }

    public void ClearLagMsg() {
        LagIndicator.text = "";
    }

    public void PrintReliableLatency(int rtt) {
        var ping = (int) rtt / 2;
        ReliableLatency.text = "Rel rtt: " + rtt + " ping: " + ping;
    }

    public void PrintUnreliableLatency(int rtt) {
        var ping = (int)rtt / 2;
        UnreliableLatency.text = "Unr rtt: " + rtt + " ping: " + ping;
    }

    void OnEnable() {
        NetClock.ClearLagMsgEvent += ClearLagMsg;
        NetClock.PrintLagMsgEvent += PrintLagMsg;
        NetworkSessionManager.NetPrintInputEvent += PrintEnemy;
        NetworkSessionManager.NetPrintOutputEvent += PrintPlayer;
        NetworkSessionManager.ReliableLatencyEvent += PrintReliableLatency;
        NetworkSessionManager.UnreliableLatencyEvent += PrintUnreliableLatency;
    }

    void OnDisable() {
        NetClock.ClearLagMsgEvent -= ClearLagMsg;
        NetClock.PrintLagMsgEvent -= PrintLagMsg;
        NetworkSessionManager.NetPrintInputEvent -= PrintEnemy;
        NetworkSessionManager.NetPrintOutputEvent -= PrintPlayer;
        NetworkSessionManager.ReliableLatencyEvent -= PrintReliableLatency;
        NetworkSessionManager.UnreliableLatencyEvent -= PrintUnreliableLatency;
    }
}
