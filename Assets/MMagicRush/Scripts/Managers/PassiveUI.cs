using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUI : MonoBehaviour {

	public int hero;
	public float counter;
	public Sprite[] imagesMonica;
	public Sprite[] imagesCebola;
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
		
		if (counter >= 1 && counter <= 2) { // primeira imagem
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [0];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [0];
		}
		if (counter > 2 && counter <= 3) { // segunda imagem
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [1];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [1];
		}
		if (counter > 3 && counter <= 4) { // terceira imagem
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [2];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [2];
		}
		if (counter > 4 && counter <= 5) { // quarta image
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [3];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [3];
		}
		if (counter > 5 && counter <= 6) { // quinta imagem
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [4];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [4];
		}
		if (counter >6) { // reset
			if(hero == 0 && imagesMonica.Length > 0)
				GetComponent<Image> ().sprite = imagesMonica [0];

			if(hero == 1 && imagesCebola.Length > 0)
				GetComponent<Image> ().sprite = imagesCebola [0];
			StartCooldown = false;
		}


	}
		
}
