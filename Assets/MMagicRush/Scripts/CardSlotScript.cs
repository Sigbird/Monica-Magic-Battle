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
	public SpriteRenderer UIilustrationAnim;
	public SpriteRenderer Uibg;
	public Canvas UItexts;
	public SpriteRenderer UIcategory;

	public CardHistoric UIHistoric;

	public Shader GrayShader;


	//CARD INFO

	public float avaiablePosition;
	public float unavaiablePosition;


	public int CardPosition;

	public int cardID;

	public int tutorialID;

	public int[] cards;

	public GameObject[] effectsAnimation;

	public float holdCounter;

	[HideInInspector]
	public int[] empty;

	public Sprite[] cardsImages;

	public Sprite[] cardsImagesBW;

	public Sprite[] cardFront;

	public Sprite[] cardFrontBW;

	public int[] cardlistIngame; 

	public int cardCost;

	public Text costText;

	public Text nameText;

	public CardInfoScript cardInfo;

	public bool beeingDraged;

	public AudioManager audioManager;


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

	private Color defaultUiBG;
	private Color defaultUIilustration;
	private Color defaultUIilustrationAnim;
	private Color defaultUIframe;
	private Color defaultUUIribbon;
	private Color defaultUIgems;


	private bool projectileCreated;
	// Use this for initialization
	void Start () {
		defaultUiBG = Uibg.color;
		defaultUIilustration = UIilustration.color; 
		defaultUIilustrationAnim = UIilustrationAnim.color;
		defaultUIframe = UIframe.color;
		defaultUUIribbon = UIribbon.color;
		defaultUIgems = UIgems.color;

		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
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

		if (beeingDraged == false) {
			transform.localScale = new Vector3(0.1f,0.1f,1);
			Uibg.color = new Color (1, 1, 1, 1);
			UIilustration.color = new Color (1, 1, 1, 1);
			UIframe.color = new Color (1, 1, 1, 1);
			UIribbon.color = new Color (1, 1, 1, 1);
			UIgems.color = new Color (1, 1, 1, 1);
			UIcategory.color = new Color (1, 1, 1, 1);
			UpdatePosition ();
		} else {
			transform.localScale = new Vector3(0.15f,0.15f,1);
			Uibg.color = new Color (1, 1, 1, 0.5f);
			UIilustration.color = new Color (1, 1, 1, 0.5f);
			UIframe.color = new Color (1, 1, 1, 0.5f);
			UIribbon.color = new Color (1, 1, 1, 0.5f);
			UIgems.color = new Color (1, 1, 1, 0.5f);
			UIcategory.color = new Color (1, 1, 1, 0.5f);
			transform.position = Vector2.MoveTowards (this.transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition), 5);
		}
		
	}

	//INICIO DO ARRASTO
	void OnMouseDrag() {
		if (StaticController.instance.GameController.GameOver == false) {
//			if (cardID == 1) {
//				UIilustrationAnim.gameObject.SetActive (true);
//				UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill1", true);
//			}
//			if (cardID == 3) {
//				UIilustrationAnim.gameObject.SetActive (true);
//				UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill2", true);
//			}
//			if (cardID == 7) {
//				UIilustrationAnim.gameObject.SetActive (true);
//				UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill3", true);
//			}
			GameObject.Find ("EnemyArea").GetComponent<Image> ().enabled = true;
			Debug.Log (cardID);
			this.released = false;
			beeingDraged = true;
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
	}

	//FIM DO ARRASTO
	void OnMouseUp(){

//		UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill1", false);
//		UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill2", false);
//		UIilustrationAnim.transform.GetComponent<Animator> ().SetBool ("Skill3", false);
//		UIilustrationAnim.gameObject.SetActive (false);


		GameObject.Find ("EnemyArea").GetComponent<Image> ().enabled = false;
		if (holdCounter > 0.1f) {
			if (HoveringObject != null && Movable != null) {
//				if (HoveringObject.tag == "Trash") {
//					GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)cardCost / 2;
//					GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
//					Movable.GetComponent<sparkScript> ().DestroyItself ();
//				} else 
				if(Movable.transform.position.y> -2f && cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
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
			//if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				SendCard ();
			//}
			projectileCreated = false;
		}
		released = true;
		beeingDraged = false;
		holdCounter = 0;
	}

	//PUXA CARTA NOVA PARA O SLOT
	public void UpdateCard(){
		cards = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
		int[] numbers = new int[6];
		numbers [0] = 1;
		numbers [1] = 3;
		numbers [2] = 7;
		numbers [3] = 11;
		numbers [4] = 16;
		numbers [5] = 20;
		//Debug.Log (cards.Length);
		if (cards.Length == 0) {
			cardID = 1;
		} else {
//			if(PlayerPrefs.GetString("MovementOnly") == "True"){
//				cardID = 21;
//			}else{
//				cardID = cards [Random.Range(0, cards.Length)];
//			}
			//cardID = numbers[Random.Range(0,6)];

//			if (tutorialID != 0)
//			cardID = tutorialID;
			//while(cards [Random.Range(0, cards.Length)] != 
			cardID = cards [Random.Range(0, cards.Length)];
		}
		switch (cardID) {
		case 0://SEM CARTA
			
			cardCost = 9999;
			UIilustration.sprite = cardsImages [0];
			UpdateCard();
			break;
		 													//HABILIDADES

		case 1://ESTALO MAGICO
			cardCost = 25;
			nameText.text = "Nevasca";
			UIilustration.sprite = cardsImages [1];
			break;
		case 2://ESPLOSAO MAGICA
			cardCost = 10;
			nameText.text = "Estalo Magico";
			UIilustration.sprite = cardsImages [2];
			break;
		case 3://NEVASCA
			cardCost = 75;
			nameText.text = "Canja";
			UIilustration.sprite = cardsImages [3];
			break;
		case 4://TERREMOTO
			cardCost = 5;
			nameText.text = "Explosão";
			UIilustration.sprite = cardsImages [4];
			break;
		case 5://HORA DA SONECA
			cardCost = 50;
			nameText.text = "Terremoto";
			UIilustration.sprite = cardsImages [5];
			break;
		case 6://REMEDIO
			cardCost = 125;
			nameText.text = "Soneca";
			UIilustration.sprite = cardsImages [6];
			break;
		case 7://CANJA
			cardCost = 75;
			nameText.text = "Remédio";
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
		case 13://TROPA: JOTALHAO
			cardCost = 50;
			nameText.text = "Jotalhão";
			UIilustration.sprite = cardsImages [14];
			break;
		case 14://TROPA: PITECO
			cardCost = 15;
			nameText.text = "Piteco";
			UIilustration.sprite = cardsImages [15];
			break;
		case 15://TROPA: PENADINHO
			cardCost = 50;
			nameText.text = "Penadinho";
			UIilustration.sprite = cardsImages [16];
			break;
		case 16://TROPA: SANSAO
			cardCost = 5;
			nameText.text = "Sansao";
			UIilustration.sprite = cardsImages [18];
			break;
		case 17://TROPA: MINGAU
			cardCost = 5;
			nameText.text = "Mingau";
			UIilustration.sprite = cardsImages [19];
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
		if (GameObject.Find ("HistoricContent") != null) {
			GameObject.Find ("HistoricContent").GetComponent<CardHistoric> ().AddCard (cardID, 1);
		}
		
			switch (cardID) {

									//HABILIDADES

			case 1:// ESTALO MAGICO -> NEVASCA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {//NEVASCA
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
/*Effect*/		Instantiate (effectsAnimation [1], new Vector3 (0, 0, 0), Quaternion.identity);
				audioManager.PlayAudio ("chuva2");
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
			case 2:// ESPLOSAO MAGICA -> ESTALO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {//ESTALO
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//				if (target.GetComponent<SoldierControler> ().team == 2) {
				//					target.GetComponent<SoldierControler> ().ReceiveEffect ("damage"); // Usando implementação de arrastar
				//				}

				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
//					if (obj.GetComponent<SoldierControler>() != null) 
//						Instantiate (effectsAnimation [2], obj.transform);
//						obj.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
					if (obj.GetComponent<WPIASoldierControler> () != null) {
						Instantiate (effectsAnimation [2], obj.transform);
/*Effect*/				obj.GetComponent<WPIASoldierControler> ().ReceiveEffect ("damage");
						audioManager.PlayAudio ("shot");
					}
				}
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
				break;
			case 3:// NEVASCA -> CANJA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {//CANJA
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//				target.GetComponent<SoldierControler> ().ReceiveEffect ("extraHealing");
/*Effect*/		Instantiate (effectsAnimation [3], new Vector3 (0, 0, 0), Quaternion.identity);
				audioManager.PlayAudio ("alivio");
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
					if (obj.GetComponent<WPSoldierControler>() != null) 
						obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
				}
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}	
			break;
			case 4:// TERREMOTO -> BIDU -> EXPLOSAOMAGICA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				if (target.GetComponent<SoldierControler> ().team == 2) {
//					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraDamage");
//				}
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					if (obj.GetComponent<WPIASoldierControler> () != null) {
						obj.GetComponent<WPIASoldierControler> ().ReceiveEffect ("damage");
					}
					if (obj.GetComponent<SoldierControler> () != null) {
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
							}
					}
/*Effect*/		Instantiate (effectsAnimation [4], new Vector3 (0, 0, 0), Quaternion.identity);
				audioManager.PlayAudio ("cabrum");
				Camera.main.gameObject.GetComponent<CameraShake> ().ShakeCamera ();
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
			break;
			case 5:// HORA DA SONECA -> PENADINHO -> TERREMOTO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
	//				if (target.GetComponent<SoldierControler> ().team == 2) {
	//					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraSlow");
	//				}
/*Effect*/		Instantiate (effectsAnimation [5], new Vector3 (0, 0, 0), Quaternion.identity);
				audioManager.PlayAudio ("terremoto");
				Camera.main.gameObject.GetComponent<CameraShake> ().ShakeCamera ();
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					if (obj.GetComponent<SoldierControler> () != null) {
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraSlow");
					}
					if (obj.GetComponent<WPIASoldierControler> () != null) {
						obj.GetComponent<WPIASoldierControler> ().ReceiveEffect ("extraSlow");
					}
					}
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}

				break;
			case 6:// REMEDIO -> ALFREDO -> HORADASONECA
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
//				target.GetComponent<SoldierControler> ().ReceiveEffect ("sleep");
/*Effect*/		Instantiate (effectsAnimation [6], new Vector3 (0, 0, 0), Quaternion.identity);
				audioManager.PlayAudio ("suspiro");
					foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
						if (obj.GetComponent<SoldierControler>() != null) 
						obj.GetComponent<SoldierControler> ().ReceiveEffect ("sleep");
					if (obj.GetComponent<WPIASoldierControler>() != null) 
						obj.GetComponent<WPIASoldierControler> ().ReceiveEffect ("sleep");
					}
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}

				break;
			case 7:// CANJA DE GALINHA -> REMEDIO
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//				target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
//					if (obj.GetComponent<SoldierControler> () != null) {
//							obj.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
//					}
					if (obj.GetComponent<WPSoldierControler> () != null) {
/*Effect*/					Instantiate (effectsAnimation [7], new Vector3 (0, 0, 0), Quaternion.identity);
							audioManager.PlayAudio ("alivio");
							obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("healing");
					}
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
//						if (obj.GetComponent<SoldierControler> () != null) {
//								Instantiate (effectsAnimation [8], obj.transform);
//								obj.GetComponent<SoldierControler> ().ReceiveEffect ("extraHealing");
//						}
						if (obj.GetComponent<WPSoldierControler> () != null) {
/*Effect*/						Instantiate (effectsAnimation [8], obj.transform);
								audioManager.PlayAudio ("tadan");
								obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("extraHealing");
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
					if (obj.GetComponent<WPSoldierControler> () != null) 
/*Effect*/				Instantiate (effectsAnimation [9], obj.transform);
					audioManager.PlayAudio ("nervosa");
					obj.GetComponent<WPSoldierControler> ().ReceiveEffect ("warShout");
				}
				GameObject.Find ("DeckPile").GetComponent<DeckPileScript> ().DrawNewCard (CardPosition);
				Destroy (this.gameObject);
			} else {
				GameObject.Find ("DeckPile").GetComponent<DeckPileScript> ().DrawNewCard (CardPosition);
				Destroy (this.gameObject);
			}
				break;
			case 10:// FALTA MUNICAO
			if (GameObject.Find("HeroBaseEnemy") != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//target.GetComponent<TroopController> ().ReceiveEffect ("lowAmmo");
				GameObject.Find("HeroBaseEnemy").GetComponent<BaseDefense>().haveAmmo = false;
/*Effect*/		Instantiate (effectsAnimation [10], GameObject.Find("HeroBaseEnemy").transform);
				audioManager.PlayAudio ("nao");
				GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
				Destroy (this.gameObject);
			}
				break;

											//TROPAS

		case 11:// TROPA: BIDU
			if(GameObject.Find ("GameController").GetComponent<GameController> ().tutorial == true){
				GameObject.Find ("GameController").GetComponent<TutorialController> ().tutorialArrows.SetActive (true);
			}
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 1;//BIDU
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 12:// TROPA: ASTRONAUTA
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 2;
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
//			if (Random.Range (1, 3) == 1) {
//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
//			} else {
//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 13:// TROPA: JOTALHÃO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 4;
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 14:// TROPA: PITECO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 5;
			Instantiate(t,Movable.transform.position, Quaternion.identity);
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 15:// TROPA: PENADINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 6;//PENADINHO
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 16:// TROPA: SANSAO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 8;
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
			GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(CardPosition);
			Destroy (this.gameObject);
			break;
		case 17:// TROPA: MINGAU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 9;
			if (Movable != null) {
				Instantiate (t, Movable.transform.position, Quaternion.identity);
			} else {
				Instantiate (t, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			}
			//			if (Random.Range (1, 3) == 1) {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop1").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 1;;
			//			} else {
			//				Instantiate (t, GameObject.Find ("HeroSpawTroop2").transform.position, Quaternion.identity).GetComponent<SoldierControler>().lane = 2;;
			//			}
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
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.2f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -1.2f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);	
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;		
			}
			break;
		case 10:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x - 0.05f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x - 0.05f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;		
			}
			break;
		case 20:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x + 1.1f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x + 1.1f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;	
			}
			break;
		case 30:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x + 2.25f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x + 2.25f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;		
			}
			break;
		case 40:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.3f, Camera.main.transform.position.y -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (Camera.main.transform.position.x -0.3f, Camera.main.transform.position.y -4.2f), Time.deltaTime * 3);
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;	
			}
			break;
		case 50:
			if (cardCost <= GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (0.3f, -4), Time.deltaTime * 3);
				Uibg.sprite = cardFront [2];
				UIilustration.sprite = cardsImages[cardID];
				//UIilustration.color = defaultUIilustration;
				UIilustrationAnim.color = defaultUIilustrationAnim;
				UIframe.sprite = cardFront [1];
				UIribbon.sprite = cardFront [0];
				UIgems.color = defaultUIgems;
			} else {
				transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (0.3f, -4.1f), Time.deltaTime * 3);
				Uibg.sprite = cardFrontBW [2];
				UIilustration.sprite = cardsImagesBW[cardID];
				//UIilustration.color = Color.gray;//
				UIilustrationAnim.color = Color.gray;
				UIframe.sprite = cardFrontBW [1];
				UIribbon.sprite = cardFrontBW [0];
				UIgems.color = Color.gray;	
			}
			break;
		default:
			break;
		}
		Uibg.sortingOrder = 98 - CardPosition;
		UIilustration.sortingOrder = 99 - CardPosition;
		UIilustrationAnim.sortingOrder = 100 - CardPosition;
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
