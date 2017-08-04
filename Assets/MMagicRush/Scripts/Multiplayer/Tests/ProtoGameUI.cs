using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class ProtoGameUI : MonoBehaviour {
    public Text Player;
    public Text Enemy;
	// Use this for initialization
	void Start () {
        Player.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
        Player.text = NetworkSessionManager.Instance.Match.Opponent.DisplayName;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
