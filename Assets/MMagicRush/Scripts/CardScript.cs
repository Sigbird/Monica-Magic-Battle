using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

	public int CardID;

	public bool isactivebutton = false;
	public Image activebutton;
	public Sprite activeTrue;
	public Sprite activeFalse;

	public string efeito;
	public string personagem;

	public string cardname;
	public string descrition;
	public string cost;
	public string damage;
	public Sprite efect;
	public Sprite image;
	public Sprite peson;

	public GameObject cardInfo;

	// Use this for initialization
	void Start () {
		this.GetComponent<Image> ().sprite = image;
	}
	
	// Update is called once per frame
	void Update () {
	
	

	}

	public void OpenCardInfo(){
		cardInfo.SetActive (true);
		cardInfo.GetComponent<CardInfoScript> ().SendCard (this.gameObject);
	}



	public void SetActiveButton(){
		activebutton.sprite = activeTrue;
		isactivebutton = true;
	}

	public void SetDesativeButton(){
		activebutton.sprite = activeFalse;
		isactivebutton = false;
	}


}
