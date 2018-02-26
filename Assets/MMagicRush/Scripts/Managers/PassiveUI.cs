using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUI : MonoBehaviour {

	public int hero;
	public float counter;
	public Sprite[] imagesCooldown;
	public Sprite imageMonica;
	public Sprite imageCebola;
	public Sprite imageMagali;
	public Sprite imageCascao;
	public Sprite imageChico;
	public bool StartCooldown;

	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartCooldown) {
			counter += Time.deltaTime;
		} else {
			counter = 0;
		}

		PassiveCooldown ();

	}

	public void PassiveCooldown(){
		
		if (counter >= 1 && counter <= 3) { // primeira imagem
			if(imagesCooldown.Length > 0)
				GetComponent<Image> ().sprite = imagesCooldown [0];
		}
		if (counter > 3 && counter <= 4) { // segunda imagem
			if(imagesCooldown.Length > 0)
				GetComponent<Image> ().sprite = imagesCooldown [1];
		}
		if (counter > 4 && counter <= 5) { // terceira imagem
			if(imagesCooldown.Length > 0)
				GetComponent<Image> ().sprite = imagesCooldown [2];
		}
		if (counter > 5 && counter <= 6) { // quarta image
			if(imagesCooldown.Length > 0)
				GetComponent<Image> ().sprite = imagesCooldown [3];
		}
		if (counter > 6 && counter <= 7) { // quinta imagem
			if(hero == 0 && imageMonica != null)
				GetComponent<Image> ().sprite = imageMonica;

			if(hero == 1 && imageCebola != null)
				GetComponent<Image> ().sprite = imageCebola;

			if(hero == 2 && imageCebola != null)
				GetComponent<Image> ().sprite = imageMagali;

			if(hero == 3 && imageCebola != null)
				GetComponent<Image> ().sprite = imageCascao;

			if(hero == 4 && imageCebola != null)
				GetComponent<Image> ().sprite = imageChico;
		}
		if (counter >7) { // reset
			

			StartCooldown = false;
		}


	}
		
}
