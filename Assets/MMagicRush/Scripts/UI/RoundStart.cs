using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay.MMB.Multiplayer;

public class RoundStart : MonoBehaviour {
	public GameObject Hero;
	public GameObject Enemy;
	public GameController GC;

	public int id;

	public Sprite[] heroSprites;
	public Image p1Sprite;
	public Image p2Sprite;
	public Text roundText;

	void Awake(){
		int enemyid = 0;
		int heroid = 1;
		if (GC.multiplayer == false) {
			
			heroid = PlayerPrefs.GetInt ("SelectedCharacter");
			enemyid = PlayerPrefs.GetInt ("Enemy");

		} else {
			
			if (BoltNetwork.IsServer) {
				heroid = MatchData.Instance.Server.Hero;
				enemyid = MatchData.Instance.Client.Hero;
			}
			if (BoltNetwork.IsClient) {
				heroid = MatchData.Instance.Client.Hero;
				enemyid = MatchData.Instance.Server.Hero;
			}

		}
		p1Sprite.sprite = heroSprites [heroid];
		p2Sprite.sprite = heroSprites [enemyid];

	}

	// Use this for initialization
	void Start () {
		//GC = GameObject.Find ("GameController").GetComponent<GameController>();
		if (GC.multiplayer == false) {
			Hero.SetActive (false);
			Enemy.SetActive (false);
		}
		//Time.timeScale = 0;

//		if (PlayerPrefs.GetInt ("SelectedCharacter") != null) {
//			id =	PlayerPrefs.GetInt ("SelectedCharacter");
//		} else {
//			id = 1;
//		}

//		switch (id) {
//		case 1:
//			p1Sprite.sprite = heroSprites [0];
//			break;
//		case 2:
//			p1Sprite.sprite = heroSprites [1];
//			break;
//		case 3:
//			p1Sprite.sprite = heroSprites [0];
//			break;
//		default:
//			break;
//		}
		roundText.text = "ROUND " + GC.round + "\n" + GC.playerCharges + "x" + GC.enemyCharges;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void StartRound(){
		this.gameObject.SetActive (false);
		if (GC.multiplayer == false) {
			Hero.SetActive (true);
			Enemy.SetActive (true);
			Time.timeScale = 1;
		}
	}
}
