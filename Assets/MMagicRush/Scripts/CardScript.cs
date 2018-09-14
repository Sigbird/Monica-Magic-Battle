using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

	public int CardID;
	public bool tutorial;
	public bool cardshop;

	public bool isactivebutton;
	public Image activebutton;
	public Sprite activeTrue;
	public Sprite activeFalse;

	public GameObject PriceTag;
	public Text PricetagValue;


	public string efeito;
	public string personagem;

	public string cardname;
	public string descrition;
	public string Gemcost;
	public string cost;
	public string damage;
	public Image efect;
	public Sprite image;
	public Sprite peson;

	public GameObject TutorialHand;

	public GameObject cardInfo;

	// Use this for initialization
	void Start () {
		
		this.GetComponent<Image> ().sprite = image;


	}

	void OnEnable(){
		StartCoroutine (LateStart (0.1f));
	}

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		int[] enableds = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
		//if (CardShop == false) {
		foreach (int enab in enableds) {
			//Debug.Log ("Carta ativa!: " + enab +" "+ this.CardID);
			if (enab == this.CardID) {
				Debug.Log ("Carta ativa!: " + enab + this.CardID);
				//g.GetComponent<CardScript> ().activebutton.sprite = g.GetComponent<CardScript> ().activeTrue;
				isactivebutton = true;
				break;
			} else {
				isactivebutton = false;
			}
		}
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (isactivebutton == true) {
			activebutton.sprite = activeTrue;
		} else {
			activebutton.sprite = activeFalse;
		}
	

	}

	public void OpenCardInfo(){
		
		if (tutorial == true && activebutton.isActiveAndEnabled == false) {
			if(GameObject.Find ("Hand") != null)
				GameObject.Find ("Hand").SetActive (false);
			
		}else if (tutorial == true) {
			GameObject.Find ("TutorialController").GetComponent<TutorialMain> ().ClickOnCard ();

			if(GameObject.Find ("TutorialHandP2") != null)
				GameObject.Find ("TutorialHandP2").SetActive (false);

			if (GameObject.Find ("CardShop") != null)
				GameObject.Find ("CardShop").GetComponent<Button> ().interactable = true;
		}
			

		
		cardInfo.SetActive (true);
		cardInfo.GetComponent<CardInfoScript> ().SendCard (this.gameObject);
	}



	public void SetActiveButton(){
		if(activebutton != null)
		activebutton.sprite = activeTrue;
		isactivebutton = true;
	}

	public void SetDesativeButton(){
		activebutton.sprite = activeFalse;
		isactivebutton = false;
	}

	public void ActiveCard(){
		if(activebutton != null)
			activebutton.sprite = activeTrue;
		isactivebutton = true;

		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Add (this.CardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		//finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);

//		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
//
//		int[] finalArray = new  int[original.Length + 1 ];
//
//		for(int i = 0; i < original.Length; i ++ ) {
//			finalArray[i] = original[i];
//		}
//
//		finalArray[finalArray.Length - 1] = CardID;
//		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	public void DeactiveCard(){
		if (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length > 15) {// VERIFICA SE TEM PELO MENOS 15 CARTAS SELECIONADAS
			activebutton.sprite = activeFalse;
			isactivebutton = false;
			int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

			List<int> iList = new List<int> ();

			for (int i = 0; i < original.Length; i++) {
				iList.Add (original [i]);
			}

			iList.Remove (CardID);

			int[] finalArray = new int[iList.Count];

			int x = 0;
			foreach (int i in iList) {
				finalArray [x] = i;
				x++;
			}

			PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
		}
	}


}
