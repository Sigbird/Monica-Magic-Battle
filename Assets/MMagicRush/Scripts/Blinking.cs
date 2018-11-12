using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Blink());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator Blink() {
			GetComponent<Text>().enabled = false;
			yield return new WaitForSeconds(0.5f);
			GetComponent<Text>().enabled = true;
			yield return new WaitForSeconds(0.5f);
		StartCoroutine(Blink());
	}
}
