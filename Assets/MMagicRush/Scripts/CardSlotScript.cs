using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotScript : MonoBehaviour {
	public int cardID;

	public Sprite[] cardsImages;

	public int[] cardlistIngame; 

	public int cardCost;

	public GameObject Spark;

	public Text costText;

	private GameObject Movable;

	public bool released;


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

	void OnMouseUp(){
//		if (Movable != null)
//		Destroy (Movable.gameObject);
		this.released = true;
		projectileCreated = false;
	}


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



		public void ActivateCardEffect(GameObject target){
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			Destroy (Movable.gameObject);
			switch (cardID) {
			case 1:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
				break;
			case 2:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				break;
			case 3:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
				break;
			case 4:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("speed");
				break;
			case 5:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("slows");
				break;
			case 6:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("damage");
				break;
			case 7:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("healing");
				break;
			case 8:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("shield");
				break;
			case 9:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("speed");
				break;
			case 10:
			target.GetComponent<SoldierControler> ().ReceiveEffect ("slows");
				break;
			default:
				break;
			}

	}
}
