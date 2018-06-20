using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay;

public class RewardWindow : MonoBehaviour {
	public Sprite[] RankingStatusImages;
	public Image RankingStatus;
	public Text RankingPos;
	public Text PlayerName;

	// Use this for initialization
	void Start () {
		#if !UNITY_EDITOR
		PlayerName.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
		#endif

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
