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

	private GameObject Movable;

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
		Destroy (Movable.gameObject);
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
	}



		public void ActivateCardEffect(){
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds -= cardCost;
			Destroy (Movable.gameObject);
			switch (cardID) {
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
			case 6:
				break;
			case 7:
				break;
			case 8:
				break;
			case 9:
				break;
			case 10:
				break;
			default:
				break;
			}

	}
}
