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

		CardSlot.HoveringObject = other.gameObject;

	}

	public void DestroyItself(){
		Destroy (this.gameObject);
	}
}
