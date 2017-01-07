using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	

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
