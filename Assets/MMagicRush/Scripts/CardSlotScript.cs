using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotScript : MonoBehaviour {

	//CARD INFO
	public int cardID;

	public int[] cards;

	public float holdCounter;

	[HideInInspector]
	public int[] empty;

	public Sprite[] cardsImages;

	public int[] cardlistIngame; 

	public int cardCost;

	public Text costText;

	public CardInfoScript cardInfo;


	//SPARKOBJECT
	public GameObject Spark;

	public GameObject Movable;

	public bool released;

	public GameObject HoveringObject;


	//PREFABS
	public GameObject movementMarker;

	public GameObject troop;

	//PlayerHero
	public SoldierControler PlayerHero;


	private bool projectileCreated;
	// Use this for initialization
	void Start () {
		//Pegar lista de cartas via playerprefs
//		cardlistIngame = PlayerPrefsX.GetIntArray ("CardsList");
//		cardID = cardlistIngame [Random.Range (0, cardlistIngame.Length)];
//		if (PlayerPrefs.GetString ("Tutorial") == "True") {
//			this.GetComponent<Button> ().interactable = true;
//		} else {
//			this.GetComponent<Button> ().interactable = false;
//		}
		projectileCreated = false;
		UpdateCard ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	//INICIO DO ARRASTO
	void OnMouseDrag() {
		this.released = false;
		if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
			if (projectileCreated == false) {
				projectileCreated = true;
				Movable = (GameObject)Instantiate (Spark, new Vector2 (0, 0), Quaternion.identity);
				Movable.transform.SetParent (GameObject.Find ("Canvas").transform);
				Movable.GetComponent<sparkScript> ().CardSlot = this.gameObject.GetComponent<CardSlotScript> ();
			}
			if (Movable != null)
				Movable.transform.position = Vector2.MoveTowards (Movable.transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition), 5);
		}
		holdCounter += Time.deltaTime;
	}

	//FIM DO ARRASTO
	void OnMouseUp(){
		if (holdCounter > 0.1f) {
			if (HoveringObject != null && Movable != null) {
				if (HoveringObject.tag == "Trash") {
					GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)cardCost / 2;
					UpdateCard ();
					Movable.GetComponent<sparkScript> ().DestroyItself ();
				} else {
//					if (PlayerPrefs.GetString ("Tutorial") == "False") {
						ActivateCardEffect ();
//					} else {
//						SendCard ();
//					}
				}
//			if (HoveringObject.tag == "enemysoldier1") {
//				ActivateCardEffect (HoveringObject);
//			}
//			if (HoveringObject.tag == "enemysoldier2") {
//				ActivateCardEffect (HoveringObject);
//			} 
//			if (HoveringObject.tag == "Stage") {
//				ActivateCardEffect (HoveringObject);
//			}
			
				projectileCreated = false;
			}
		} else {
			SendCard ();
			projectileCreated = false;
		}
		released = true;
	}

	//PUXA CARTA NOVA PARA O SLOT
	public void UpdateCard(){
		cards = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		if (cards.Length == 0) {
			cardID = 1;
		} else {
			if(PlayerPrefs.GetString("MovementOnly") == "True"){
				cardID = 21;
			}else{
				cardID = cards [Random.Range(0, cards.Length)];
			}
		}
		switch (cardID) {
		case 0://SEM CARTA
			cardCost = 9999;
			GetComponent<Image> ().sprite = cardsImages [0];
			break;
		 													//HABILIDADES

		case 1://ESTALO MAGICO
			cardCost = 2;
			GetComponent<Image> ().sprite = cardsImages [1];
			break;
		case 2://ESPLOSAO MAGICA
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [2];
			break;
		case 3://NEVASCA
			cardCost = 20;
			GetComponent<Image> ().sprite = cardsImages [3];
			break;
		case 4://TERREMOTO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [4];
			break;
		case 5://HORA DA SONECA
			cardCost = 75;
			GetComponent<Image> ().sprite = cardsImages [5];
			break;
		case 6://REMEDIO
			cardCost = 5;
			GetComponent<Image> ().sprite = cardsImages [6];
			break;
		case 7://CANJA
			cardCost = 25;
			GetComponent<Image> ().sprite = cardsImages [7];
			break;
		case 8://ESCUDO
			cardCost = 25;
			GetComponent<Image> ().sprite = cardsImages [8];
			break;
		case 9://GRITO DE GUERRA
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [9];
			break;
		case 10://MUNICAO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [10];
			break;

																//TROPAS

		case 11://TROPA: BIDU
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [11];
			break;
		case 12://TROPA: ASTRONAUTA
			cardCost = 3;
			GetComponent<Image> ().sprite = cardsImages [12];
			break;
		case 13://TROPA: ANJINHO
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [13];
			break;
		case 14://TROPA: JOTALHAO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [14];
			break;
		case 15://TROPA: PITECO
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [15];
			break;
		case 16://TROPA: PENADINHO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [16];
			break;
		case 17://TROPA: LOUCO
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [17];
			break;
		case 18://TROPA: SANSAO
			cardCost = 40;
			GetComponent<Image> ().sprite = cardsImages [18];
			break;
		case 19://TROPA: MINGAU
			cardCost = 110;
			GetComponent<Image> ().sprite = cardsImages [19];
			break;
		case 20://TROPA: ALFREDO
			cardCost = 150;
			GetComponent<Image> ().sprite = cardsImages [20];
			break;
																//TORRES

		case 21://TORRE: PAPEL
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [21];
			break;
		case 22://TORRE: AGUA
			cardCost = 3;
			GetComponent<Image> ().sprite = cardsImages [22];
			break;
		case 23://TORRE: DESENTUPIDORES
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [23];
			break;
		case 24://TORRE: NEVE
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [24];
			break;
		case 25://TORRE: CURA
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [25];
			break;
		case 26://TORRE: TESOURO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [26];
			break;
		case 27://TORRE: SONO
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [27];
			break;
		case 28://TORRE: ANTI TORRE
			cardCost = 40;
			GetComponent<Image> ().sprite = cardsImages [28];
			break;
		case 29://TORRE: PROTETORA
			cardCost = 110;
			GetComponent<Image> ().sprite = cardsImages [29];
			break;
		default:
			Debug.Log ("out of range");
			break;
		}
		costText.text = cardCost.ToString ();
	}


		//ATIVA EFEITO DA CARTA SOBRE O ALVO DO OBJETO MOVABLE
		public void ActivateCardEffect(){
		GameObject t;

			switch (cardID) {

									//HABILIDADES

			case 1:// ESTALO MAGICO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("damage"); // Usando implementação de arrastar
//				}

					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
					}

				UpdateCard ();
			}
				break;
			case 2:// ESPLOSAO MAGICA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraDamage");
//				}
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraDamage");
					}
				UpdateCard ();
			}
				break;
			case 3:// NEVASCA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("slow");
//				}
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");
					}
				UpdateCard ();
			}
				break;
			case 4:// TERREMOTO 
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraSlow");
//				}
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraSlow");
					}
				UpdateCard ();
			}
				break;
			case 5:// HORA DA SONECA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("sleep");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("sleep");
					}
				UpdateCard ();
			}
				break;
			case 6:// REMEDIO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null) {
							if (obj.GetComponent<SoldierControler> ().heroUnity == true)
								obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
						}
					}
				UpdateCard ();
			}
				break;
			case 7:// CANJA DE GALINHA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("extraHealing");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					}
				UpdateCard ();
			}
				break;
			case 8:// ESCUDO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler> () != null) {
							if (obj.GetComponent<SoldierControler> ().heroUnity == true)
								obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraHealing");
						}
					}
				UpdateCard ();
			}
				break;
			case 9:// GRITO DE GUERRA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("warShout");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("warShout");
					}
				UpdateCard ();
			}
				break;
			case 10:// FALTA MUNICAO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//target.GetComponent<TroopController> ().ReceiveEffect ("lowAmmo");
				UpdateCard ();
			}
				break;

											//TROPAS

			case 11:// TROPA: BIDU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 1;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 12:// TROPA: ASTRONAUTA
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 2;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 13:// TROPA: ANJINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 3;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 14:// TROPA: JOTALHÃO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 4;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 15:// TROPA: PITECO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 5;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 16:// TROPA: PENADINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 6;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 17:// TROPA: LOUCO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 7;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 18:// TROPA: SANSAO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 8;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 19:// TROPA: MINGAU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 9;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 20:// TROPA: ALFREDO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 10;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;

											//MOVIMENTACAO

		case 21:// MOVER
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = Instantiate (movementMarker, Movable.transform.position, Quaternion.identity);
			PlayerHero.ChangeLane (t.transform.position);
			UpdateCard ();
			break;
//			case 22:// TORRE: AGUA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 2;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 23:// TORRE: DESENTUPIDOR
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 3;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 24:// TORRE: NEVE
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 4;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 25:// TORRE: CURA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 5;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 26:// TORRE: TESOURO
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 6;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 27:// TORRE: SONO
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 7;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 28:// TORRE: ANTI TORRE
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 8;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
//			case 29:// TORRE: PROTETORA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 9;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			UpdateCard ();
//			break;
			default:
			Debug.Log ("out of range");
			break;
			}
		//Movable.GetComponent<sparkScript> ().DestroyItself ();
	}

	public void SendCard(){
		cardInfo.gameObject.SetActive (true);
		Time.timeScale = 0;
		cardInfo.DisplayCard (cardID);
		cardInfo.lastCard = this.gameObject;
	}
}
