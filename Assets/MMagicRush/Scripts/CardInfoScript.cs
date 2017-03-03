using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

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
		efect.sprite = o.GetComponent<CardScript> ().efect;
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
		int[] temp = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");
		ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("PlayerCardsIDs", temp);
		this.gameObject.SetActive (false);
	}

	public void ActiveCard(){
		lastCard.GetComponent<CardScript> ().SetActiveButton ();
		int[] temp = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
		ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", temp);
	}

	public void DeactiveCard(){
		lastCard.GetComponent<CardScript> ().SetDesativeButton ();
		int[] temp = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
		ArrayUtility.Remove<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", temp);
	}

}
