using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAnimationController : MonoBehaviour {

	public Animator Anim;
	public GameObject Coleções;
	public GameObject Loja;
	public GameObject Chests;
	public GameObject Ranking;
	public GameObject Opcoes;
	public GameObject[] Buttons;

	// Use this for initialization
	void Start () {
		//Anim = GetComponent<Animator> ();
		//Anim.SetTrigger ("menuChests");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressed(string bt){
	
		if (bt == "Colecao") {
			
			Buttons[0].transform.localScale = new Vector3 (1.2f, 1.2f, 1);
			Buttons[1].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[2].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[3].transform.localScale = new Vector3 (1, 1, 1);

			Buttons [0].GetComponent<Button> ().interactable = false;
			Buttons [1].GetComponent<Button> ().interactable = true;
			Buttons [2].GetComponent<Button> ().interactable = true;
			Buttons [3].GetComponent<Button> ().interactable = true;

//			if (Chests.activeSelf == true) {
//				Anim.SetTrigger ("menuChestsOff");
//			}
//			if (Loja.activeSelf == true) {
//				Anim.SetTrigger ("menuLojaOff");
//			}
//			if (Ranking.activeSelf == true) {
//				Anim.SetTrigger ("menuRankingOff");
//			}
//			if (Opcoes.activeSelf == true) {
//				Anim.SetTrigger ("menuConfigOff");
//			}
			Anim.SetTrigger ("menuColection");
		}

		if (bt == "Batalha") {
			
			Buttons[0].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[1].transform.localScale = new Vector3 (1.2f, 1.2f, 1);
			Buttons[2].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[3].transform.localScale = new Vector3 (1, 1, 1);

			Buttons [0].GetComponent<Button> ().interactable = true;
			Buttons [1].GetComponent<Button> ().interactable = false;
			Buttons [2].GetComponent<Button> ().interactable = true;
			Buttons [3].GetComponent<Button> ().interactable = true;

//			if (Coleções.activeSelf == true) {
//				Anim.SetTrigger ("menuCartasOff");
//			}
//			if (Loja.activeSelf == true) {
//				Anim.SetTrigger ("menuLojaOff");
//			}
//			if (Ranking.activeSelf == true) {
//				Anim.SetTrigger ("menuRankingOff");
//			}
//			if (Opcoes.activeSelf == true) {
//				Anim.SetTrigger ("menuConfigOff");
//			}
			Anim.SetTrigger ("menuChests");
		}

		if (bt == "Opcoes") {

			Buttons[0].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[1].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[2].transform.localScale = new Vector3 (1.2f, 1.2f, 1);
			Buttons[3].transform.localScale = new Vector3 (1, 1, 1);

			Buttons [0].GetComponent<Button> ().interactable = true;
			Buttons [1].GetComponent<Button> ().interactable = true;
			Buttons [2].GetComponent<Button> ().interactable = false;
			Buttons [3].GetComponent<Button> ().interactable = true;

//			if (Chests.activeSelf == true) {
//				Anim.SetTrigger ("menuChestsOff");
//			}
//			if (Loja.activeSelf == true) {
//				Anim.SetTrigger ("menuLojaOff");
//			}
//			if (Ranking.activeSelf == true) {
//				Anim.SetTrigger ("menuRankingOff");
//			}
//			if (Coleções.activeSelf == true) {
//				Anim.SetTrigger ("menuCartasOff");
//			}
			Anim.SetTrigger ("menuOptions");
		}

		if (bt == "Ranking") {

			Buttons[0].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[1].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[2].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[3].transform.localScale = new Vector3 (1.2f, 1.2f, 1);

			Buttons [0].GetComponent<Button> ().interactable = true;
			Buttons [1].GetComponent<Button> ().interactable = true;
			Buttons [2].GetComponent<Button> ().interactable = true;
			Buttons [3].GetComponent<Button> ().interactable = false;


//			if (Chests.activeSelf == true) {
//				Anim.SetTrigger ("menuChestsOff");
//			}
//			if (Loja.activeSelf == true) {
//				Anim.SetTrigger ("menuCLojaOff");
//			}
//			if (Coleções.activeSelf == true) {
//				Anim.SetTrigger ("menuCartasOff");
//			}
//			if (Opcoes.activeSelf == true) {
//				Anim.SetTrigger ("menuConfigOff");
//			}
			Anim.SetTrigger ("menuRanking");
		}


		if (bt == "Loja") {

			Buttons[0].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[1].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[2].transform.localScale = new Vector3 (1, 1, 1);
			Buttons[3].transform.localScale = new Vector3 (1, 1, 1);

			Buttons [0].GetComponent<Button> ().interactable = true;
			Buttons [1].GetComponent<Button> ().interactable = true;
			Buttons [2].GetComponent<Button> ().interactable = true;
			Buttons [3].GetComponent<Button> ().interactable = true;


//			if (Chests.activeSelf == true) {
//				Anim.SetTrigger ("menuChestsOff");
//			}
//			if (Loja.activeSelf == true) {
//				Anim.SetTrigger ("menuCLojaOff");
//			}
//			if (Coleções.activeSelf == true) {
//				Anim.SetTrigger ("menuCartasOff");
//			}
//			if (Opcoes.activeSelf == true) {
//				Anim.SetTrigger ("menuConfigOff");
//			}
			Anim.SetTrigger ("menuShop");
		}
	}
}
