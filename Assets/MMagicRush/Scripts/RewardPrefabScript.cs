using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPrefabScript : MonoBehaviour {

	public int IdPos;
	public Sprite[] images;
	public SceneHelper Controller;

	// Use this for initialization
	void Start () {
		int pc = PlayerPrefs.GetInt ("PlayerCoinsPremium");
		int sc = PlayerPrefs.GetInt ("PlayerCoins");
		int x = Random.Range (0, images.Length - 1);
		this.GetComponent<SpriteRenderer> ().sprite = images [x];
		switch (x) {
		case 0:
			PlayerPrefs.SetInt ("PlayerCoinsPremium", pc + 30);
			break;
		case 1:
			PlayerPrefs.SetInt ("PlayerCoinsPremium", pc + 50);
			break;
		case 2:
			PlayerPrefs.SetInt ("PlayerCoinsPremium", pc + 100);
			break;
		case 3:
			PlayerPrefs.SetInt ("PlayerCoins", sc + 25);
			break;
		case 4:
			PlayerPrefs.SetInt ("PlayerCoins", sc + 50);
			break;
		case 5:
			PlayerPrefs.SetInt ("PlayerCoins", sc + 100);
			break;
		case 6:
			Controller.CardQuantity [11] += 1; //Bidu
			Controller.EarnCard(11);
			break;
		case 7:
			Controller.CardQuantity [3] += 1; //Canja
			Controller.EarnCard(3);
			break;
		case 8:
			Controller.CardQuantity [18] += 1; //Cranicola
			Controller.EarnCard(18);
			break;
		case 9:
			Controller.CardQuantity [5] += 1; //Terremoto
			Controller.EarnCard(5);
			break;
		case 10:
			Controller.CardQuantity [2] += 1; //Estalo
			Controller.EarnCard(2);
			break;
		case 11:
			Controller.CardQuantity [4] += 1; //Boom
			Controller.EarnCard(4);
			break;
		case 12:
			Controller.CardQuantity [15] += 1; //Penadinho
			Controller.EarnCard(15);
			break;
		case 13:
			Controller.CardQuantity [17] += 1; //Mingau
			Controller.EarnCard(17);
			break;
		case 14:
			Controller.CardQuantity [14] += 1; //piteco
			Controller.EarnCard(14);
			break;
		case 15:
			Controller.CardQuantity [16] += 1; // sansão
			Controller.EarnCard(16);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (IdPos == 0) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (0, 2,0), Time.deltaTime * 10);
		}
		if (IdPos == 1) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (-1.7f, 2,0), Time.deltaTime * 10);
		}
		if (IdPos == 2) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (1.7f, 2,0), Time.deltaTime * 10);
		}
	}
}
