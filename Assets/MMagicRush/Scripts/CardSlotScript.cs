﻿using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotScript : MonoBehaviour {

	//CARD INFO
	public int cardID;

	public Sprite[] cardsImages;

	public int[] cardlistIngame; 

	public int cardCost;

	public Text costText;


	//SPARKOBJECT
	public GameObject Spark;

	public GameObject Movable;

	public bool released;

	public GameObject HoveringObject;


	//PREFABS
	public GameObject tower;

	public GameObject troop;


	private bool projectileCreated;
	// Use this for initialization
	void Start () {
		//Pegar lista de cartas via playerprefs
//		cardlistIngame = PlayerPrefsX.GetIntArray ("CardsList");
//		cardID = cardlistIngame [Random.Range (0, cardlistIngame.Length)];
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
	}

	//FIM DO ARRASTO
	void OnMouseUp(){
		if (HoveringObject != null && Movable != null ) {
			if (HoveringObject.tag == "Trash") {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)cardCost / 2;
				UpdateCard ();
				Movable.GetComponent<sparkScript> ().DestroyItself ();
			}
			if (HoveringObject.tag == "enemysoldier1") {
				ActivateCardEffect (HoveringObject);
			}
			if (HoveringObject.tag == "enemysoldier2") {
				ActivateCardEffect (HoveringObject);
			} 
			if (HoveringObject.tag == "Stage") {
				ActivateCardEffect (HoveringObject);
			}
		
			projectileCreated = false;
		}
	}

	//PUXA CARTA NOVA PARA O SLOT
	public void UpdateCard(){
		cardID = Random.Range (13, 21);
		switch (cardID) {
		case 1://ESTALO MAGICO
			cardCost = 2;
			GetComponent<Image> ().sprite = cardsImages [0];
			break;
		case 2://ESPLOSAO MAGICA
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [1];
			break;
		case 3://NEVASCA
			cardCost = 20;
			GetComponent<Image> ().sprite = cardsImages [2];
			break;
		case 4://TERREMOTO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [3];
			break;
		case 5://HORA DA SONECA
			cardCost = 75;
			GetComponent<Image> ().sprite = cardsImages [4];
			break;
		case 6://REMEDIO
			cardCost = 5;
			GetComponent<Image> ().sprite = cardsImages [5];
			break;
		case 7://CANJA
			cardCost = 25;
			GetComponent<Image> ().sprite = cardsImages [6];
			break;
		case 8://ESCUDO
			cardCost = 25;
			GetComponent<Image> ().sprite = cardsImages [7];
			break;
		case 9://GRITO DE GUERRA
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [8];
			break;
		case 10://MUNICAO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [9];
			break;
		case 11://TORRE
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [10];
			break;
		case 12://TROPA: BIDU
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [11];
			break;
		case 13://TROPA: ASTRONAUTA
			cardCost = 3;
			GetComponent<Image> ().sprite = cardsImages [12];
			break;
		case 14://TROPA: ANJINHO
			cardCost = 1;
			GetComponent<Image> ().sprite = cardsImages [13];
			break;
		case 15://TROPA: JOTALHAO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [14];
			break;
		case 16://TROPA: PITECO
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [15];
			break;
		case 17://TROPA: PENADINHO
			cardCost = 50;
			GetComponent<Image> ().sprite = cardsImages [16];
			break;
		case 18://TROPA: LOUCO
			cardCost = 100;
			GetComponent<Image> ().sprite = cardsImages [17];
			break;
		case 19://TROPA: SANSAO
			cardCost = 40;
			GetComponent<Image> ().sprite = cardsImages [18];
			break;
		case 20://TROPA: MINGAU
			cardCost = 110;
			GetComponent<Image> ().sprite = cardsImages [19];
			break;
		case 21://TROPA: ALFREDO
			cardCost = 150;
			GetComponent<Image> ().sprite = cardsImages [20];
			break;
		default:
			Debug.Log ("out of range");
			break;
		}
		costText.text = cardCost.ToString ();
	}


		//ATIVA EFEITO DA CARTA SOBRE O ALVO DO OBJETO MOVABLE
		public void ActivateCardEffect(GameObject target){
		GameObject t;
			switch (cardID) {
			case 1:// ESTALO MAGICO
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				if (target.GetComponent<SoldierControler> ().team == 2) {
					target.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
				}
				UpdateCard ();
			}
				break;
			case 2:// ESPLOSAO MAGICA
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				if (target.GetComponent<SoldierControler> ().team == 2) {
					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraDamage");
				}
				UpdateCard ();
			}
				break;
			case 3:// NEVASCA
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				if (target.GetComponent<SoldierControler> ().team == 2) {
					target.GetComponent<SoldierControler> ().ReceiveEffect ("slow");
				}
				UpdateCard ();
			}
				break;
			case 4:// TERREMOTO 
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				if (target.GetComponent<SoldierControler> ().team == 2) {
					target.GetComponent<SoldierControler> ().ReceiveEffect ("extraSlow");
				}
				UpdateCard ();
			}
				break;
			case 5:// HORA DA SONECA
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("sleep");
				UpdateCard ();
			}
				break;
			case 6:// REMEDIO
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				UpdateCard ();
			}
				break;
			case 7:// CANJA DE GALINHA
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("extraHealing");
				UpdateCard ();
			}
				break;
			case 8:// ESCUDO
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
				UpdateCard ();
			}
				break;
			case 9:// GRITO DE GUERRA
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("warShout");
				UpdateCard ();
			}
				break;
			case 10:// FALTA MUNICAO
			if (target.GetComponent<TroopController>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				//target.GetComponent<TroopController> ().ReceiveEffect ("lowAmmo");
				UpdateCard ();
			}
				break;
			case 11:// TORRE
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			Instantiate(tower,Movable.transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 12:// TROPA: BIDU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 1;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 13:// TROPA: ASTRONAUTA
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 2;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 14:// TROPA: ANJINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 3;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 15:// TROPA: JOTALHÃO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 4;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 16:// TROPA: PITECO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 5;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 17:// TROPA: PENADINHO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId =6;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 18:// TROPA: LOUCO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 7;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 19:// TROPA: SANSAO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 8;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 20:// TROPA: MINGAU
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 9;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			case 21:// TROPA: ALFREDO
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			t = troop;
			t.GetComponent<SoldierControler> ().troopId = 10;
			Instantiate(t,GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			UpdateCard ();
			break;
			default:
			Debug.Log ("out of range");
			break;
			}
		Movable.GetComponent<sparkScript> ().DestroyItself ();
	}
}
