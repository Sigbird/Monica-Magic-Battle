using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMain : MonoBehaviour {

	public GameObject[] tutorialPanels;
	public bool CardClicked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickOnCard(){
		if (CardClicked == false) {
			CardClicked = true;
			tutorialPanels [0].SetActive (false);
			tutorialPanels [1].SetActive (true);
		}
	}

//	IEnumerator Lesson1(){
//		yield return new WaitForSeconds (1);
//		tutorialPanels [0].SetActive (true);
//		yield return new WaitForSeconds (5);
//		tutorialPanels [0].SetActive (false);
//		tutorialPanels [1].SetActive (true);
//		interfaceElements [0].SetActive (true);
//		interfaceElements [1].SetActive (true);
//		WaypointSystem.GetComponent<BoxCollider2D> ().enabled = true;
//	}
}
