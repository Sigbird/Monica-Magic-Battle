using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotScript : MonoBehaviour {

	//UI ELEMENTS

	public SpriteRenderer UIframe;
	public SpriteRenderer UIribbon;
	public SpriteRenderer UIgems;
	public SpriteRenderer UIilustration;
	public SpriteRenderer Uibg;
	public Canvas UItexts;
	public SpriteRenderer UIcategory;


	//CARD INFO

	public float avaiablePosition;
	public float unavaiablePosition;


	public int CardPosition;

	public int cardID;

	public int[] cards;

	public float holdCounter;

	[HideInInspector]
	public int[] empty;

	public Sprite[] cardsImages;

	public int[] cardlistIngame; 

	public int cardCost;

	public Text costText;

	public Text nameText;

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
		cardInfo = GameObject.Find ("GameController").GetComponent<GameController> ().cardInfo;
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

		UpdatePosition ();
		
	}

	//INICIO DO ARRASTO
	void OnMouseDrag() {
		Debug.Log (cardID);
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
					GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
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
		int[] numbers = new int[3];
		numbers [0] = 3;
		numbers [1] = 11;
		numbers [2] = 17;

		if (cards.Length == 0) {
			cardID = 1;
		} else {
//			if(PlayerPrefs.GetString("MovementOnly") == "True"){
//				cardID = 21;
//			}else{
//				cardID = cards [Random.Range(0, cards.Length)];
//			}
			cardID = numbers[Random.Range(0,2)];
		}
		switch (cardID) {
		case 0://SEM CARTA
			cardCost = 9999;
			UIilustration.sprite = cardsImages [0];
			break;
		 													//HABILIDADES

		case 1://ESTALO MAGICO
			cardCost = 2;
			nameText.text = "Estalo Magico";
			UIilustration.sprite = cardsImages [1];
			break;
		case 2://ESPLOSAO MAGICA
			cardCost = 10;
			nameText.text = "Esplosão Magica";
			UIilustration.sprite = cardsImages [2];
			break;
		case 3://NEVASCA
			cardCost = 20;
			nameText.text = "Nevasca";
			UIilustration.sprite = cardsImages [3];
			break;
		case 4://TERREMOTO
			cardCost = 50;
			nameText.text = "Terremoto";
			UIilustration.sprite = cardsImages [4];
			break;
		case 5://HORA DA SONECA
			cardCost = 75;
			nameText.text = "Soneca";
			UIilustration.sprite = cardsImages [5];
			break;
		case 6://REMEDIO
			cardCost = 5;
			nameText.text = "Remédio";
			UIilustration.sprite = cardsImages [6];
			break;
		case 7://CANJA
			cardCost = 25;
			nameText.text = "Canja";
			UIilustration.sprite = cardsImages [7];
			break;
		case 8://ESCUDO
			cardCost = 25;
			nameText.text = "Escudo Mágico";
			UIilustration.sprite = cardsImages [8];
			break;
		case 9://GRITO DE GUERRA
			cardCost = 100;
			nameText.text = "Grito de Guerra";
			UIilustration.sprite = cardsImages [9];
			break;
		case 10://MUNICAO
			cardCost = 50;
			nameText.text = "Sem Muniçao";
			UIilustration.sprite = cardsImages [10];
			break;

																//TROPAS

		case 11://TROPA: BIDU
			cardCost = 5;
			nameText.text = "Bidu";
			UIilustration.sprite = cardsImages [11];
			break;
		case 12://TROPA: ASTRONAUTA
			cardCost = 3;
			nameText.text = "Astronauta";
			UIilustration.sprite = cardsImages [12];
			break;
		case 13://TROPA: ANJINHO
			cardCost = 1;
			nameText.text = "Anjinho";
			UIilustration.sprite = cardsImages [13];
			break;
		case 14://TROPA: JOTALHAO
			cardCost = 50;
			nameText.text = "Jotalhão";
			UIilustration.sprite = cardsImages [14];
			break;
		case 15://TROPA: PITECO
			cardCost = 15;
			nameText.text = "Piteco";
			UIilustration.sprite = cardsImages [15];
			break;
		case 16://TROPA: PENADINHO
			cardCost = 50;
			nameText.text = "Penadinho";
			UIilustration.sprite = cardsImages [16];
			break;
		case 17://TROPA: LOUCO
			cardCost = 100;
			nameText.text = "Louco";
			UIilustration.sprite = cardsImages [17];
			break;
		case 18://TROPA: SANSAO
			cardCost = 40;
			nameText.text = "Sansao";
			UIilustration.sprite = cardsImages [18];
			break;
		case 19://TROPA: MINGAU
			cardCost = 110;
			nameText.text = "Mingau";
			UIilustration.sprite = cardsImages [19];
			break;
		case 20://TROPA: ALFREDO
			cardCost = 150;
			nameText.text = "Alfredo";
			UIilustration.sprite = cardsImages [20];
			break;
																//TORRES

		case 21://Mover
			cardCost = 1;
			UIilustration.sprite = cardsImages [21];
			break;
		case 22://TORRE: AGUA
			cardCost = 3;
			UIilustration.sprite = cardsImages [22];
			break;
		case 23://TORRE: DESENTUPIDORES
			cardCost = 1;
			UIilustration.sprite = cardsImages [23];
			break;
		case 24://TORRE: NEVE
			cardCost = 50;
			UIilustration.sprite = cardsImages [24];
			break;
		case 25://TORRE: CURA
			cardCost = 15;
			UIilustration.sprite = cardsImages [25];
			break;
		case 26://TORRE: TESOURO
			cardCost = 50;
			UIilustration.sprite = cardsImages [26];
			break;
		case 27://TORRE: SONO
			cardCost = 100;
			UIilustration.sprite = cardsImages [27];
			break;
		case 28://TORRE: ANTI TORRE
			cardCost = 40;
			UIilustration.sprite = cardsImages [28];
			break;
		case 29://TORRE: PROTETORA
			cardCost = 110;
			UIilustration.sprite = cardsImages [29];
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

				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
				break;
			case 3:// NEVASCA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("slow");
//				}
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("slow");

						if (obj.GetComponent<WPIASoldierControler> () != null)
						obj.GetComponent<WPIASoldierControler> ().ReceiveEffect ("slow");
					
					}
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
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
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
				break;
			case 10:// FALTA MUNICAO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//target.GetComponent<TroopController> ().ReceiveEffect ("lowAmmo");
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
				break;

											//TROPAS

			case 11:// TROPA: BIDU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 1;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 12:// TROPA: ASTRONAUTA
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 2;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 13:// TROPA: ANJINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 3;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 14:// TROPA: JOTALHÃO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 4;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 15:// TROPA: PITECO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 5;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 16:// TROPA: PENADINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 6;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 17:// TROPA: LOUCO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 7;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 18:// TROPA: SANSAO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 8;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 19:// TROPA: MINGAU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 9;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
			case 20:// TROPA: ALFREDO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 10;
			if (Random.Range (1, 3) == 1) {
				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			} else {
				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;

											//MOVIMENTACAO

		case 21:// MOVER
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = Instantiate (movementMarker, Movable.transform.position, Quaternion.identity);
			PlayerHero.ChangeLane (t.transform.position);
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
//			case 22:// TORRE: AGUA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 2;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 23:// TORRE: DESENTUPIDOR
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 3;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 24:// TORRE: NEVE
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 4;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 25:// TORRE: CURA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 5;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 26:// TORRE: TESOURO
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 6;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 27:// TORRE: SONO
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 7;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 28:// TORRE: ANTI TORRE
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 8;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
//			case 29:// TORRE: PROTETORA
//			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//			t = tower;
//			t.GetComponent<TowerController> ().towerID = 9;
//			Instantiate(t,Movable.transform.position, Quaternion.identity);
//			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//			break;
			default:
			Debug.Log ("out of range");
			break;
			}
		//Movable.GetComponent<sparkScript> ().DestroyItself ();
	}

	public void UpdatePosition(){
		

		switch (CardPosition) {
		case 0:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -2.3f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -2.3f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
			}
			break;
		case 10:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.7f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.7f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
			}
			break;
		case 20:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.3f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.3f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
			}
			break;
		case 30:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.7f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.7f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
			}
			break;
		case 40:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.3f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.3f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
			}
			break;
		case 50:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (0.3f, -4), Time.deltaTime * 3);
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (0.3f, -4.1f), Time.deltaTime * 3);
			}
			break;
		default:
			break;
		}
		Uibg.sortingOrder = 99 - CardPosition;
		UIilustration.sortingOrder = 100 - CardPosition;
		UIframe.sortingOrder = 101 - CardPosition;
		UIribbon.sortingOrder = 102 - CardPosition;
		UIgems.sortingOrder = 103 - CardPosition;
		UItexts.sortingOrder = 104 - CardPosition;
		UIcategory.sortingOrder = 105 - CardPosition;
		//StartCoroutine (SwitchCollider ());


	}

//	IEnumerator SwitchCollider(){
//		yield return new WaitForSeconds (1);
//		if (this.GetComponent<BoxCollider2D> ().isTrigger == true) {
//			this.GetComponent<BoxCollider2D> ().isTrigger = false;
//		} else {
//			this.GetComponent<BoxCollider2D> ().isTrigger = true;
//		}
//		StartCoroutine (SwitchCollider ());
//	}
		

	public void SendCard(){
		cardInfo.gameObject.SetActive (true);
		Time.timeScale = 0;
		cardInfo.DisplayCard (cardID);
		cardInfo.lastCard = this.gameObject;
		WPScript.UIopen = true;
	}
}
