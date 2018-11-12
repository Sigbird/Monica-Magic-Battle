using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShopInfo : MonoBehaviour {
	public int x;
	public Button purchaseButton;
	public GameObject purchasedImage;
	public Button purchaseable;
	// Use this for initialization
	void Start () {
	//	PlayerPrefs.SetInt ("magali", 0);
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
				purchaseable.interactable = false;
				purchaseButton.interactable = true;
				purchasedImage.SetActive (true);
			} else if (PlayerPrefs.GetInt ("magali") != 1 ){
				purchaseable.interactable = true;
				purchaseButton.interactable = false;
				purchasedImage.SetActive (false);
			}
			break;
		case 4: 
			if (PlayerPrefs.GetInt ("cascao") == 1) {
				purchaseable.interactable = false;
				purchaseButton.interactable = true;
				purchasedImage.SetActive (true);
			} else if (PlayerPrefs.GetInt ("cascao") != 1) {
				purchaseable.interactable = true;
				purchaseButton.interactable = false;
				purchasedImage.SetActive (false);
			}
			break;
		case 5:
			if (PlayerPrefs.GetInt ("chico") == 1) {
				purchaseable.interactable = false;
				purchaseButton.interactable = true;
				purchasedImage.SetActive (true);
			} else {
				purchaseable.interactable = true;
				purchaseButton.interactable = false;
				purchasedImage.SetActive (false);
			}
			break;
		default:
			break;
		}
	}
}
