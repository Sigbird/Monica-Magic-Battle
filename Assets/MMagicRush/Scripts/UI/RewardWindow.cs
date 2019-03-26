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
	public GameObject[] Chests;
	private int ChestQtd;


	void Awake(){
		ChestQtd = PlayerPrefs.GetInt ("ChestsNumber");

	}
	// Use this for initialization
	void Start () {

		if (ChestQtd < 4) {
			Chests [0].SetActive (true);
			Chests [1].SetActive (false);
		} else {
			Chests [0].SetActive (false);
			Chests [1].SetActive (true);
		}

		#if !UNITY_EDITOR
		PlayerName.text = NetworkSessionManager.Instance.Match.Player.DisplayName;
		#endif

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
