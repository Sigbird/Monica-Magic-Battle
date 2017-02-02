using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkScript : MonoBehaviour {
	public CardSlotScript CardSlot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Trash") {
			GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += CardSlot.cardCost;
			CardSlot.UpdateCard ();
			Destroy (this.gameObject);
		}
		if (other.tag == "enemysoldier1") {
			CardSlot.ActivateCardEffect ();
			CardSlot.UpdateCard ();
		}
		if (other.tag == "enemysoldier2") {
			CardSlot.ActivateCardEffect ();
			CardSlot.UpdateCard ();
		}

	}
}
