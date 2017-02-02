using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class returnbutton : MonoBehaviour {
	public GameObject Hero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Hero.GetComponent<SoldierControler> ().state == SoldierControler.STATE.RETREAT) {
			GetComponent<Image> ().enabled = false;
		} else {
			GetComponent<Image> ().enabled = true;
		}
	}
}
