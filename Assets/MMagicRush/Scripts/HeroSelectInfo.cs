using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectInfo : MonoBehaviour {
	public int x;
	public Button purchaseButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (x) {
		case 1:
			//			if (PlayerPrefs.GetInt ("monica") == 1) {
			//				purchaseButton.interactable = false;
			//				purchasedImage.SetActive (true);
			//			} else {
			//				purchaseButton.interactable = true;
			//				purchasedImage.SetActive (false);
			//			}
			break;
		case 2:
			//			if (PlayerPrefs.GetInt ("cebolinha") == 1) {
			//				purchaseButton.interactable = false;
			//				purchasedImage.SetActive (true);
			//			} else {
			//				purchaseButton.interactable = true;
			//				purchasedImage.SetActive (false);
			//			}
			break;
		case 3: 
			if (PlayerPrefs.GetInt ("magali") == 1) {
				purchaseButton.interactable = false;
			} else {
				purchaseButton.interactable = true;
			}
			break;
		case 4: 
			if (PlayerPrefs.GetInt ("cascao") == 1) {
				purchaseButton.interactable = false;
			} else {
				purchaseButton.interactable = true;
			}
			break;
		case 5:
			if (PlayerPrefs.GetInt ("chico") == 1) {
				purchaseButton.interactable = false;
			} else {
				purchaseButton.interactable = true;
			}
			break;
		default:
			break;
		}
	}
}
