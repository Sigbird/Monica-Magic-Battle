using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FilterScript : MonoBehaviour {

	public bool CardShop;


	public List<Transform> childrens;

	public GameObject CardInfo;
	public GameObject CardPrefab;
	private GameObject g;
	public int[] b;
	public int[] x;
	private int[] cs;
	private int[] empty;
	public Sprite[] Efects;
	public Sprite[] Images;
	public Sprite[] Persons;
	public List<int> ids = new List<int>();

	void OnEnable() {
		EnableCards ();
	
	}

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {

	}

	public void FilterEfect(string t){
		foreach (Transform c in childrens)
		{
			if(c.GetComponent<CardScript>().efeito != t){
				c.gameObject.SetActive(false);
			} 
			if(c.GetComponent<CardScript>().efeito == t){
				c.gameObject.SetActive(true);
			}
			if(t == "Todos"){
				c.gameObject.SetActive(true);
			}
		}
	}

	public void FilterCharacter(string t){
		foreach (Transform c in childrens)
		{
			if(c.GetComponent<CardScript>().personagem != t){
				c.gameObject.SetActive(false);
			} 
			if(c.GetComponent<CardScript>().personagem == t){
				c.gameObject.SetActive(true);
			}
			if(c.GetComponent<CardScript>().personagem == "Todos"){
				c.gameObject.SetActive(true);
			}
		}
	}

	public void AddNewCard(){
		
	}

	public void EnableCards(){
		int[] a = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		if (b.Length == a.Length && a.Length != 0) {
			//UpdatePlayerCardList ();
		} else {
			ResetCards ();
			UpdatePlayerCardList ();
		} 

	}

	public void UpdatePlayerCardList(){
		
		cs = new int[30];
		for (int i = 1; i <= 20; i++) {
			cs [i] = i;
			//Debug.Log (i);
		}

		if (CardShop) {
			b = cs;
		} else {
			int[] a = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");
			b = a;
		}

		int[] x = b.Distinct ().ToArray ();

		int[] enableds = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

//		foreach (GameObject card in CardPrefabs) {
//			if( ArrayUtility.Contains(x,card.GetComponent<CardScript>().CardID) ){
//			GameObject g = Instantiate (card, this.transform.position, Quaternion.identity);
//			g.transform.localScale = new Vector3 (1, 1, 1);
//			g.transform.SetParent (this.transform,false);
//				childrens.Add (g.transform);
//			}
//		}
		int minimun = 0;
		foreach (int cardID in x){
			switch (cardID) {
			case 1:
				
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 1;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Nevasca";
				g.GetComponent<CardScript> ().descrition = "Aplica dois de dano em todas unidades ininimgas";
				g.GetComponent<CardScript> ().descritionLong = "Nevasca \n Congela tropas e herois inimigos causando dano leve a estes temporariamente.";
				g.GetComponent<CardScript> ().Gemcost = "25";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "50";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				if (CardShop == false) {
					foreach (int enab in enableds) {
						if (enab == cardID) {
							Debug.Log ("Cartas " + enab);
							//g.GetComponent<CardScript> ().activebutton.sprite = g.GetComponent<CardScript> ().activeTrue;
							g.GetComponent<CardScript> ().isactivebutton = true;
						} else {
							g.GetComponent<CardScript> ().isactivebutton = false;
						}
					}
				}

				break;
			case 2:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 2;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Estalo Magico";
				g.GetComponent<CardScript> ().descrition = "Aplica um de dano em uma unidade ininimga";
				g.GetComponent<CardScript> ().descritionLong = "Estalo Magico \n Causa um dano repentino na unidade heroica do adversário.";
				g.GetComponent<CardScript> ().cost = "25";
				g.GetComponent<CardScript> ().Gemcost = "10";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "25";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 3:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 3;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Canja de Galinha";
				g.GetComponent<CardScript> ().descrition = "Cura toda sua tropa";
				g.GetComponent<CardScript> ().descritionLong = "Canja de Galinha \n Canja faz muito bem pra saude, alimenta e recupera as energias do seu heroi e amigos.";
				g.GetComponent<CardScript> ().cost = "100";
				g.GetComponent<CardScript> ().Gemcost = "75";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 4:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 4;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Explosão Mágica";
				g.GetComponent<CardScript> ().descrition = "Aplica Dano a Todas as Unidades";
				g.GetComponent<CardScript> ().descritionLong = "Explosão Magica \n Grande explosão mágica causando dano a todas as unidades no raio.";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().Gemcost = "2";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "10";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 5:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 5;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Terremoto";
				g.GetComponent<CardScript> ().descrition = "Abala o chão sob o adversário";
				g.GetComponent<CardScript> ().descritionLong = "Terremoto \n Tremor repentino que pode causar dano a todas as tropas adversarias alem de dificultar sua movimentação.";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().Gemcost = "10";
				g.GetComponent<CardScript> ().damage = "3";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "50";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 6:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 6;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Hora da Soneca";
				g.GetComponent<CardScript> ().descrition = "Poe o heroi adversário para dormir.";
				g.GetComponent<CardScript> ().descritionLong = "Hora da soneca \n Provoca um sono repentino no heroi adversário botando ele para tirar um cochilinho.";
				g.GetComponent<CardScript> ().cost = "75";
				g.GetComponent<CardScript> ().Gemcost = "25";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "75";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 7:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 7;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Remédio";
				g.GetComponent<CardScript> ().descrition = "Cura toda sua tropa em 2 pontos";
				g.GetComponent<CardScript> ().descritionLong = "Remedio \n Bom e velho remedinho caseiro da vóvó, recupera boa parte da vida do seu heroi.";
				g.GetComponent<CardScript> ().cost = "5";
				g.GetComponent<CardScript> ().Gemcost = "15";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "5";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 8:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 8;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Escudo";
				g.GetComponent<CardScript> ().descrition = "Parotege suas Tropas em 3 pontos";
				g.GetComponent<CardScript> ().descritionLong = "Escudo \n Bloqueia temporariamente parte do dano recebido por seu personagem.";
				g.GetComponent<CardScript> ().cost = "25";
				g.GetComponent<CardScript> ().Gemcost = "15";
				g.GetComponent<CardScript> ().damage = "50";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "25";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 9:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 9;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Grito de Guerra";
				g.GetComponent<CardScript> ().descrition = "Fortalece suas tropas";
				g.GetComponent<CardScript> ().descritionLong = "Grito de Guerra \n Chama toda a molecada e vamos com tudo pra cima deles! Aumenta motivação de suas tropas..";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().Gemcost = "15";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "10";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 10:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 10;
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Sem Munição";
				g.GetComponent<CardScript> ().descrition = "Paralisa torres inimigas";
				g.GetComponent<CardScript> ().descritionLong = "Sem Munição \n Sabota e deixa as torres do adversário sem munição.";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().Gemcost = "15";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect.sprite = Efects [0];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "10";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			
										//TROPAS

			case 11:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 11;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Monica";
				g.GetComponent<CardScript> ().cardname = "Bidu";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Bidu para ajudar";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().Gemcost = "5";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [1];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "1";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 12:
				
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 12;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Astronauta";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Astronauta para ajudar";
				g.GetComponent<CardScript> ().cost = "3";
				g.GetComponent<CardScript> ().Gemcost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 13: //removido (Anjinho)
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 13;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Jotalhão";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade jotalhão para ajudar";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().Gemcost = "75";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 14:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 14;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Piteco";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Piteco para ajudar";
				g.GetComponent<CardScript> ().cost = "15";
				g.GetComponent<CardScript> ().Gemcost = "75";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 15:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 15;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Penadinho";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Penadinho para ajudar";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().Gemcost = "125";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 16:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 16;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Monica";
				g.GetComponent<CardScript> ().cardname = "Sansão";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Sansão para ajudar";
				g.GetComponent<CardScript> ().cost = "40";
				g.GetComponent<CardScript> ().Gemcost = "50";
				g.GetComponent<CardScript> ().damage = "3";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [1];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 17: // Mingau
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 17;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Magali";
				g.GetComponent<CardScript> ().cardname = "Mingau";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Mingau para ajudar";
				g.GetComponent<CardScript> ().cost = "110";
				g.GetComponent<CardScript> ().Gemcost = "150";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [2];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 18: // Cranicola
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 18;
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Magali";
				g.GetComponent<CardScript> ().cardname = "Cranicola";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Cranicola para ajudar";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().Gemcost = "5";
				g.GetComponent<CardScript> ().damage = "120";
				g.GetComponent<CardScript> ().efect.sprite = Efects [1];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [2];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "50";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			

										//TORRES

			case 21:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 21;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Papel";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 22:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 22;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Agua";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "3";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 23:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 23;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Desentupidor";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 24:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 24;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Neve";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 25:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 25;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre da Cura";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "15";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 26:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 26;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre do Tesouro";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 27:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 27;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre do Sono";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "100";
				g.GetComponent<CardScript> ().damage = "5";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 28:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 28;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre AntiTorre";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "40";
				g.GetComponent<CardScript> ().damage = "3";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			case 29:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().CardID = 29;
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre Protetora";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "110";
				g.GetComponent<CardScript> ().damage = "5";
				g.GetComponent<CardScript> ().efect.sprite = Efects [2];
				g.GetComponent<CardScript> ().image = Images [cardID];
				g.GetComponent<CardScript> ().peson = Persons [0];
				g.transform.SetParent (this.transform, false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				if (CardShop) {
					g.GetComponent<CardScript> ().PriceTag.SetActive (true);
					g.GetComponent<CardScript> ().PricetagValue.text = "100";
					g.GetComponent<CardScript> ().activebutton.gameObject.SetActive (false);
				}
				childrens.Add (g.transform);
				break;
			}
			if (CardShop == false) {
//				foreach (int enab in enableds) {
//					if (enab == cardID) {
//						Debug.Log ("Cartas " + enab);
//						//g.GetComponent<CardScript> ().activebutton.sprite = g.GetComponent<CardScript> ().activeTrue;
//						//g.GetComponent<CardScript> ().isactivebutton = true;
//					}
//				}
			}

//			if (minimun <= 15 && CardShop == false) {
//				g.GetComponent<CardScript> ().ActiveCard ();
//				minimun++;
//			}

		}
		if (CardShop == false) {
			CheckForDoubles ();
		}
	}

	public void CheckForDoubles(){
		Transform[] filter = this.gameObject.GetComponentsInChildren<Transform> ();
		//int[] ids;
		foreach (Transform f in filter)
		{
			if (f.name == "Card(Clone)") {
				if (f.GetComponent<CardScript> ().CardID == 11) {
					if (ids.Contains (f.GetComponent<CardScript> ().CardID)) {
						Destroy (f.gameObject);
					} else {
						ids.Add (f.GetComponent<CardScript> ().CardID);
					}
				}
			}
//			if(f.name == "Card(Clone)" || f.name == "Card" ){
//				if(ids.Contains(f.GetComponent<CardScript>().CardID)){
//					Destroy (f.gameObject);
//				}else{
//					ids.Add(f.GetComponent<CardScript>().CardID);
//				}
//
//			}  
		}
		ids.Clear ();
	}

	public void ResetCards(){
		
				Transform[] filter = this.gameObject.GetComponentsInChildren<Transform> ();
				foreach (Transform f in filter)
				{
					if(f.name == "Card(Clone)"){
						Destroy (f.gameObject);
						x = empty;
						childrens.Clear ();
						
					}  
				}
	}

	public void ClearChildrens(){
//		Transform[] filter = this.gameObject.GetComponentsInChildren<Transform> ();
//		foreach (Transform f in filter)
//		{
//			if(f.name == "Card(Clone)"){
//				Destroy (f.gameObject);
//				x = empty;
//				childrens.Clear ();
//			}  
//		}
	}

}
