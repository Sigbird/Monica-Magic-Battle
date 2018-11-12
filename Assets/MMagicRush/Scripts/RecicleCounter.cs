using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecicleCounter : MonoBehaviour {
	public CardInfoScript cardinfo;
	public int count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UseRecicle(){
		this.count++;
		if (this.count >= 2) {
			GetComponent<Button> ().interactable = false;
		}
	}
}
