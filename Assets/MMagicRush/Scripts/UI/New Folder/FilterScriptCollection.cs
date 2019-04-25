using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterScriptCollection : MonoBehaviour {

	public bool All;
	public int[] selectedCards;
	public int[] playercards;
	private int[] emptyarray;


	public CardScript[] Cards;

	// Use this for initialization
	void Start () {
		


		selectedCards = CleanArray(PlayerPrefsX.GetIntArray ("SelectedCardsIDs"));

		playercards = CleanArray(PlayerPrefsX.GetIntArray ("PlayerCardsIDs"));

		if (All) {
			UpdatePlayerCards ();
		} else {
			UpdateSelected ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public int[] CleanArray(int[] arr){
		



		List<int> list2 = new List<int>();
		foreach (int x in arr) {
			if (!list2.Contains (x)) {
				list2.Add (x);
			}
		}

		return list2.ToArray ();

	}

	public List<int> CleanArraytoList(int[] arr){




		List<int> list2 = new List<int>();
		foreach (int x in arr) {
			if (!list2.Contains (x)) {
				list2.Add (x);
			}
		}

		return list2;

	}

	public void UpdatePlayerCardsDelayed () {
		StartCoroutine (DelayedUpdateCards ());
	}

	public void UpdatePlayerCards () {
		List<int> Playercardslist2 = CleanArraytoList(PlayerPrefsX.GetIntArray ("PlayerCardsIDs"));


		for (int i = 0; i < Cards.Length; i++) {
			if (Playercardslist2.Contains(Cards [i].CardID)) {
				Cards [i].disabledcard = false;
				Cards [i].UpdateCollectionCard ();

			} else {
				Cards [i].disabledcard = true;
				Cards [i].UpdateCollectionCard ();
				Debug.Log ("Fora");
			}
		}



	}

	IEnumerator DelayedUpdateCards(){
		yield return new WaitForSeconds (1);
		List<int> Playercardslist2 = CleanArraytoList(PlayerPrefsX.GetIntArray ("PlayerCardsIDs"));


		for (int i = 0; i < Cards.Length; i++) {
			if (Playercardslist2.Contains(Cards [i].CardID)) {
				Cards [i].disabledcard = false;
				Cards [i].UpdateCollectionCard ();

			} else {
				Cards [i].disabledcard = true;
				Cards [i].UpdateCollectionCard ();
				Debug.Log ("Fora");
			}
		}

	}

	public void UpdateSelectedDelayed () {
		StartCoroutine (DelayedUpdateSelectedCards ());
	}

	public void UpdateSelected () {
		selectedCards = CleanArray(PlayerPrefsX.GetIntArray ("SelectedCardsIDs"));
		List<int> list1 = new List<int>();

		for (int i = 0; i < Cards.Length; i++) {
			
			Cards [i].GetComponent<Button> ().interactable = false;
			Cards [i].CardID = 0;
			Cards [i].UpdateCollectionCard ();
		}
		for (int i = 0; i < Cards.Length; i++) {

			if(i< selectedCards.Length){
				Cards [i].GetComponent<Button> ().interactable = true;
				list1.Add (selectedCards [i]);
				Cards [i].CardID = selectedCards [i];
				Cards [i].UpdateCollectionCard ();
			}

			}


	}

	IEnumerator DelayedUpdateSelectedCards(){
		yield return new WaitForSeconds (1);
		selectedCards = CleanArray(PlayerPrefsX.GetIntArray ("SelectedCardsIDs"));
		List<int> list1 = new List<int>();

		for (int i = 0; i < Cards.Length; i++) {

			Cards [i].GetComponent<Button> ().interactable = false;
			Cards [i].CardID = 0;
			Cards [i].UpdateCollectionCard ();
		}
		for (int i = 0; i < Cards.Length; i++) {

			if(i< selectedCards.Length){
				Cards [i].GetComponent<Button> ().interactable = true;
				list1.Add (selectedCards [i]);
				Cards [i].CardID = selectedCards [i];
				Cards [i].UpdateCollectionCard ();
			}

		}
	}
}
