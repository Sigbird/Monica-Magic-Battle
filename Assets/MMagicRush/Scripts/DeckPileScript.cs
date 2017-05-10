﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPileScript : MonoBehaviour {

	public int Hand;
	public int Deck;
	public GameObject newCard;

	void Start () {
		Hand = 0;
		Deck = PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length;
		StartCoroutine (StartingHand ());
	}

	void Update () {
		
	}

	public void DrawCard(){ 
		if (Deck >= 1) {
			newCard.GetComponent<CardSlotScript> ().CardPosition = Hand;
			Instantiate (newCard, this.transform.position, Quaternion.identity);
			Hand += 10;
		}
	}

	public void DrawNewCard(int usedCard){ 
		GameObject[] cs = GameObject.FindGameObjectsWithTag ("CardSlots");
		foreach (GameObject o in cs) {
			if (o.GetComponent<CardSlotScript> ().CardPosition > usedCard) {
				o.GetComponent<CardSlotScript> ().CardPosition = o.GetComponent<CardSlotScript> ().CardPosition - 10;
			}
		}
		Hand -= 10;
		DrawCard ();
	}

	IEnumerator StartingHand(){
		yield return new WaitForSeconds (0.5f);
		DrawCard ();
		if (Hand <= 40) {
			StartCoroutine (StartingHand ());
		}
	}

}