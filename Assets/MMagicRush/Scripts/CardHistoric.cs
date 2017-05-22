﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHistoric : MonoBehaviour {

	public GameObject Image;
	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ChildCount () >= 6) {
			Destroy (this.transform.GetChild(0).gameObject);
		}
		
	}

	public void AddCard(int cardid, int team){
	
		if (team == 1) {
			switch (cardid) {
			case 1:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 2:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 3:
				Image.GetComponent<Image> ().sprite = sprites [1];
				break;
			case 4:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 5:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 6:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 7:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 8:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 9:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 10:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 11:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 12:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 13:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 14:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 15:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			case 16:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			default:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			}
		} else {
			switch (cardid) {
			case 1:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 2:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 3:
				Image.GetComponent<Image> ().sprite = sprites [3];
				break;
			case 4:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 5:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 6:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 7:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 8:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 9:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 10:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 11:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 12:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 13:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 14:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 15:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			case 16:
				Image.GetComponent<Image> ().sprite = sprites [2];
				break;
			default:
				Image.GetComponent<Image> ().sprite = sprites [0];
				break;
			}
		}
		Instantiate (Image, transform);
	}

	public int ChildCount(){
		int x = 0;
		float alpha = 1;
		foreach (Transform child in transform) {
			child.GetComponent<Image> ().color = new Color(1,1,1,alpha);
			alpha -= 0.1f;
			x++;
		}
		return  x;
	}

}
