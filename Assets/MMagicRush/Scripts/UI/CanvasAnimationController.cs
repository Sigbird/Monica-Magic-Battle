using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationController : MonoBehaviour {

	public Animator Anim;
	public GameObject Coleções;
	public GameObject Loja;
	public GameObject Chests;
	public GameObject Ranking;
	public GameObject Opcoes;

	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animator> ();
		Anim.SetTrigger ("menuChests");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressed(string bt){
	
		if (bt == "Colecao") {
			if (Chests.activeSelf == true) {
				Anim.SetTrigger ("menuChestsOff");
			}
			if (Loja.activeSelf == true) {
				Anim.SetTrigger ("menuLojaOff");
			}
			if (Ranking.activeSelf == true) {
				Anim.SetTrigger ("menuRankingOff");
			}
			if (Opcoes.activeSelf == true) {
				Anim.SetTrigger ("menuConfigOff");
			}
			Anim.SetTrigger ("menuCartas");
		}

		if (bt == "Batalha") {
			if (Coleções.activeSelf == true) {
				Anim.SetTrigger ("menuCartasOff");
			}
			if (Loja.activeSelf == true) {
				Anim.SetTrigger ("menuLojaOff");
			}
			if (Ranking.activeSelf == true) {
				Anim.SetTrigger ("menuRankingOff");
			}
			if (Opcoes.activeSelf == true) {
				Anim.SetTrigger ("menuConfigOff");
			}
			Anim.SetTrigger ("menuChests");
		}

		if (bt == "Opcoes") {
			if (Chests.activeSelf == true) {
				Anim.SetTrigger ("menuChestsOff");
			}
			if (Loja.activeSelf == true) {
				Anim.SetTrigger ("menuLojaOff");
			}
			if (Ranking.activeSelf == true) {
				Anim.SetTrigger ("menuRankingOff");
			}
			if (Coleções.activeSelf == true) {
				Anim.SetTrigger ("menuCartasOff");
			}
			Anim.SetTrigger ("menuConfig");
		}

		if (bt == "Ranking") {
			if (Chests.activeSelf == true) {
				Anim.SetTrigger ("menuChestsOff");
			}
			if (Loja.activeSelf == true) {
				Anim.SetTrigger ("menuCLojaOff");
			}
			if (Coleções.activeSelf == true) {
				Anim.SetTrigger ("menuCartasOff");
			}
			if (Opcoes.activeSelf == true) {
				Anim.SetTrigger ("menuConfigOff");
			}
			Anim.SetTrigger ("menuRanking");
		}
			
	}
}
