using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour {

	public GameObject troop;
	//public int cardID;
	public int cardCost;
	public int[] cardlist;




	void Start () {
		cardlist = new int[5];

		for (int i = 0; i < 5; i++) {
			int x = Random.Range (1, 16);
			cardlist [i] = x;
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool ActivateCardEffect(string CardType){
		GameObject t;
		int id = 0;

			if(CardType == "Magic"){
			foreach (int c in cardlist) {
				if (CheckCost(c) <= GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds && c < 11) {
					id = c;
				}
			}
			}else{
			foreach (int c in cardlist) {
				if (CheckCost(c) <= GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds && c >= 11) {
					id = c;
				}
			}
			}

		if (id > 0) {

			//HABILIDADES
			switch (id) {
			case 1:// ESTALO MAGICO
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
					}
				}
				break;
			case 2:// ESPLOSAO MAGICA
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraDamage");
					}
				}
				break;
			case 3:// NEVASCA
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");

						if (obj.GetComponent<WPSoldierControler> () != null)
							obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("slow");

					}

				}
				break;
			case 4:// TERREMOTO 
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					//GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");

						if (obj.GetComponent<WPSoldierControler> () != null)
							obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("slow");

					}

				}
				break;
			case 5:// HORA DA SONECA
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					//GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");

						if (obj.GetComponent<WPSoldierControler> () != null)
							obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("slow");

					}

				}
				break;
			case 6:// REMEDIO
				if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					}

				}
				break;
			case 7:// CANJA DE GALINHA
				if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					}

				}
				break;
			case 8:// ESCUDO
				if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					}

				}
				break;
			case 9:// GRITO DE GUERRA
				if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("warShout");
					}

				}
				break;
			case 10:// FALTA MUNICAO
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
					GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
					GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null)
							obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");

						if (obj.GetComponent<WPSoldierControler> () != null)
							obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("slow");

					}

				}
				break;

			//TROPAS

			case 11:// TROPA: BIDU
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 1;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 12:// TROPA: ASTRONAUTA
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 2;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 13:// TROPA: ANJINHO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 3;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 14:// TROPA: JOTALHÃO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 4;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 15:// TROPA: PITECO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 5;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 16:// TROPA: PENADINHO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 6;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 17:// TROPA: LOUCO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 7;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 18:// TROPA: SANSAO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 8;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 19:// TROPA: MINGAU
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 9;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			case 20:// TROPA: ALFREDO
				GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds -= CheckCost(id);
				t = troop;
				t.GetComponent<SoldierControler> ().troopId = 10;
				if (Random.Range (1, 3) == 1) {
					Instantiate (t, GameObject.Find ("EnemySpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 1;

				} else {
					Instantiate (t, GameObject.Find ("EnemySpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler> ().lane = 2;

				}

				break;
			default:
				Debug.Log ("out of range");
				break;
			}
			return true;
		} else {
			return false;
		}
	}

	public int CheckCost(int cardid)
	{
		switch (cardid) {
		case 1:
			return 2;
			break;
		case 2:
			return 10;
			break;
		case 3:
			return  20;
			break;
		case 4:
			return  50;
			break;
		case 5:
			return  75;
			break;
		case 6:
			return  5;
			break;
		case 7:
			return  25;
			break;
		case 8:
			return  25;
			break;
		case 9:
			return  100;
			break;
		case 10:
			return  50;
			break;
		case 11:
			return  40;
			break;
		case 12:
			return  50;
			break;
		case 13:
			return  40;
			break;
		case 14:
			return  50;
			break;
		case 15:
			return  60;
			break;
		case 16:
			return  50;
			break;
		default:
			break;
		}
		return 0;
	}

}
