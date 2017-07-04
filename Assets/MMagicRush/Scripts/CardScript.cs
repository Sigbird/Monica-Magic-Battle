using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

	public int CardID;

	public bool isactivebutton = false;
	public Image activebutton;
	public Sprite activeTrue;
	public Sprite activeFalse;

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
		
	
	// Update is called once per frame
	void Update () {
	
	

	}

	public void OpenCardInfo(){
		if(GameObject.Find ("TutorialHandP2") != null)
			GameObject.Find ("TutorialHandP2").SetActive (false);
		
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
