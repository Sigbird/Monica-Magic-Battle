using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardInfoScript : MonoBehaviour {

	public Text cardname;
	public Text descrition;
	public Text cost;
	public Text damage;
	public Image efect;
	public Image image;
	public Image character;
	public GameObject lastCard;

	public GameObject enableButton;
	public GameObject disableButton;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendCard(GameObject o){
		lastCard = o;
		cardname.text = o.GetComponent<CardScript> ().cardname;
		descrition.text = o.GetComponent<CardScript> ().descrition;
		cost.text = o.GetComponent<CardScript> ().cost;
		damage.text = o.GetComponent<CardScript> ().damage;
		efect.sprite = o.GetComponent<CardScript> ().efect.sprite;
		image.sprite = o.GetComponent<CardScript> ().image;
		character.sprite = o.GetComponent<CardScript> ().peson;

		if(enableButton != null && disableButton != null)
		if (o.GetComponent<CardScript> ().isactivebutton) {
			enableButton.SetActive (true);
			disableButton.SetActive(false);
		} else {
			enableButton.SetActive (false);
			disableButton.SetActive(true);
		}

	}

	public void BuyCard(){
		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		int[] finalArray = new  int[original.Length + 1 ];

		for(int i = 0; i < original.Length; i ++ ) {
			finalArray[i] = original[i];
		}

		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;


		//ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);
		this.gameObject.SetActive (false);
	}

	public void RemoveCard(){

		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		if (original.Length > 5) {

			List<int> iList = new List<int> ();


			for (int i = 0; i < original.Length; i++) {
				iList.Add (original [i]);
			}

			iList.Remove (lastCard.GetComponent<CardScript> ().CardID);

			int[] finalArray = new int[iList.Count];

			int x = 0;
			foreach (int i in iList) {
				finalArray [x] = i;
				x++;
			}

			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);
			DeactiveCard ();
			Destroy (lastCard.gameObject);
			this.gameObject.SetActive (false);
		}
	}

	public void ActiveCard(){
		lastCard.GetComponent<CardScript> ().SetActiveButton ();
		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		int[] finalArray = new  int[original.Length + 1 ];

		for(int i = 0; i < original.Length; i ++ ) {
			finalArray[i] = original[i];
		}

		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	public void DeactiveCard(){
		lastCard.GetComponent<CardScript> ().SetDesativeButton ();
		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Remove (lastCard.GetComponent<CardScript> ().CardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}



}
