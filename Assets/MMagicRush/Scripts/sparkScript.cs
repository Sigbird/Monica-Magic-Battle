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

	void OnTriggerStay2D(Collider2D other) {
		if (CardSlot.released) {
			if (other.tag == "Trash") {
				GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)CardSlot.cardCost/2;
				CardSlot.UpdateCard ();
				Destroy (this.gameObject);
			}
			if (other.tag == "enemysoldier1") {
				CardSlot.ActivateCardEffect (other.gameObject);
				CardSlot.UpdateCard ();
				Destroy (this.gameObject);
			}
			if (other.tag == "enemysoldier2") {
				CardSlot.ActivateCardEffect (other.gameObject);
				CardSlot.UpdateCard ();
				Destroy (this.gameObject);
			} 
			if (other.tag == "Stage") {
				Destroy (this.gameObject);
			}
		}
	}
}
