using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHistoric : MonoBehaviour {

	public GameObject Image;
	public Sprite[] spritesHero;
	public Sprite[] spritesEnemy;

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
			Image.GetComponent<Image> ().sprite = spritesHero [cardid];

		} else {
			Image.GetComponent<Image> ().sprite = spritesEnemy [cardid];

		}
		//if(cardid == 1 || cardid == 3 || cardid == 6 || cardid == 11 || cardid == 16 ||cardid == 20)
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
