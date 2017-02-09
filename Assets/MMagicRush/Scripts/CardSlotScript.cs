using System.Collections;
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

	private GameObject Movable;

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
		if (HoveringObject != null) {
			if (HoveringObject.tag == "Trash") {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)cardCost / 2;
				Destroy (Movable.gameObject);
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
		cardID = Random.Range (1, 11);
		switch (cardID) {
		case 1:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [0];
			break;
		case 2:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [1];
			break;
		case 3:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [2];
			break;
		case 4:
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [3];
			break;
		case 5:
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [4];
			break;
		case 6:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [0];
			break;
		case 7:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [1];
			break;
		case 8:
			cardCost = 10;
			GetComponent<Image> ().sprite = cardsImages [2];
			break;
		case 9:
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [3];
			break;
		case 10:
			cardCost = 15;
			GetComponent<Image> ().sprite = cardsImages [4];
			break;
		default:
			GetComponent<Image> ().sprite = cardsImages [0];
			break;
		}
		costText.text = cardCost.ToString ();
	}


		//ATIVA EFEITO DA CARTA SOBRE O ALVO DO OBJETO MOVABLE
		public void ActivateCardEffect(GameObject target){
			switch (cardID) {
			case 1:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
				UpdateCard ();
			}
				break;
			case 2:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				UpdateCard ();
			}
				break;
			case 3:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
				UpdateCard ();
			}
				break;
			case 4:
			//TOWER
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			Instantiate(tower,Movable.transform.position, Quaternion.identity);
			UpdateCard ();
				break;
			case 5:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("slows");
				UpdateCard ();
			}
				break;
			case 6:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
				UpdateCard ();
			}
				break;
			case 7:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				UpdateCard ();
			}
				break;
			case 8:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
				UpdateCard ();
			}
				break;
			case 9:
			//TOWER
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			Instantiate(tower,Movable.transform.position, Quaternion.identity);
			UpdateCard ();
				break;
			case 10:
			if (target.GetComponent<SoldierControler>() != null) {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
				target.GetComponent<SoldierControler> ().ReceiveEffect ("slows");
				UpdateCard ();
			}
				break;
			default:
			Debug.Log ("out of range");
			break;
			}
		Destroy (Movable.gameObject);
	}
}
