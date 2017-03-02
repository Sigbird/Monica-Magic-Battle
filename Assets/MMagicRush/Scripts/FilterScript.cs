using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FilterScript : MonoBehaviour {

	public List<Transform> childrens;

	public GameObject CardInfo;
	public GameObject CardPrefab;
	private GameObject g;

	public Sprite[] Efects;
	public Sprite[] Images;
	public Sprite[] Persons;

	void OnEnable() {
		//UpdateChildrens ();
		UpdatePlayerCardList();
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
			if(c.GetComponent<CardScript>().efeito == "Todos"){
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

	public void UpdatePlayerCardList(){
		int[] x = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");
//		foreach (GameObject card in CardPrefabs) {
//			if( ArrayUtility.Contains(x,card.GetComponent<CardScript>().CardID) ){
//			GameObject g = Instantiate (card, this.transform.position, Quaternion.identity);
//			g.transform.localScale = new Vector3 (1, 1, 1);
//			g.transform.SetParent (this.transform,false);
//				childrens.Add (g.transform);
//			}
//		}
		foreach (int cardID in x){
			switch (cardID) {
			case 1:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Estalo Magico";
				g.GetComponent<CardScript> ().descrition = "Aplica um de dano em uma unidade ininimga";
				g.GetComponent<CardScript> ().cost = "2";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 2:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Explosão Magica";
				g.GetComponent<CardScript> ().descrition = "Aplica dois de dano em todas unidades ininimgas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 3:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Nevasca";
				g.GetComponent<CardScript> ().descrition = "Aplica dois de dano em todas unidades ininimgas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 4:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Terremoto";
				g.GetComponent<CardScript> ().descrition = "Deixa Tropas Inimigas Lentas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 5:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Hora da Soneca";
				g.GetComponent<CardScript> ().descrition = "Para Tropas Inimigas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 6:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Remédio";
				g.GetComponent<CardScript> ().descrition = "Cura seu heroi";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 7:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Canja de Galinha";
				g.GetComponent<CardScript> ().descrition = "Cura toda sua tropa";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 8:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Escudo";
				g.GetComponent<CardScript> ().descrition = "Parotege suas Tropas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 9:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Grito de Guerra";
				g.GetComponent<CardScript> ().descrition = "Fortalece suas tropas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 10:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Magia";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Sem Munição";
				g.GetComponent<CardScript> ().descrition = "Paralisa torres inimigas";
				g.GetComponent<CardScript> ().cost = "10";
				g.GetComponent<CardScript> ().damage = "0";
				g.GetComponent<CardScript> ().efect = Efects[0];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			
										//TROPAS

			case 11:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Monica";
				g.GetComponent<CardScript> ().cardname = "Bidu";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Bidu para ajudar";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[1];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 12:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Astronauta";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Astronauta para ajudar";
				g.GetComponent<CardScript> ().cost = "3";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 13:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Anjinho";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Anjinho para ajudar";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 14:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Jotalhão";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade jotalhão para ajudar";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 15:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Piteco";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Piteco para ajudar";
				g.GetComponent<CardScript> ().cost = "15";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 16:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Penadinho";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Penadinho para ajudar";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 17:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Cebolinha";
				g.GetComponent<CardScript> ().cardname = "Louco";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Louco para ajudar";
				g.GetComponent<CardScript> ().cost = "100";
				g.GetComponent<CardScript> ().damage = "5";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[3];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 18:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Monica";
				g.GetComponent<CardScript> ().cardname = "Sansão";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Sansão para ajudar";
				g.GetComponent<CardScript> ().cost = "40";
				g.GetComponent<CardScript> ().damage = "3";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[1];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 19:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Magali";
				g.GetComponent<CardScript> ().cardname = "Mingau";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Mingau para ajudar";
				g.GetComponent<CardScript> ().cost = "110";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[2];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 20:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Unidade";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Alfredo";
				g.GetComponent<CardScript> ().descrition = "Chama a unidade Alfredo para ajudar";
				g.GetComponent<CardScript> ().cost = "150";
				g.GetComponent<CardScript> ().damage = "2";
				g.GetComponent<CardScript> ().efect = Efects[1];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;

										//TORRES

			case 21:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Papel";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 22:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Agua";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "3";
				g.GetComponent<CardScript> ().damage ="1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 23:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Desentupidor";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "1";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 24:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre de Neve";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 25:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre da Cura";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "15";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 26:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre do Tesouro";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost ="50";
				g.GetComponent<CardScript> ().damage = "1";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 27:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre do Sono";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost ="100";
				g.GetComponent<CardScript> ().damage = "5";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 28:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre AntiTorre";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "40";
				g.GetComponent<CardScript> ().damage = "3";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			case 29:
				g = Instantiate (CardPrefab, this.transform.position, Quaternion.identity);
				g.GetComponent<CardScript> ().efeito = "Construcao";
				g.GetComponent<CardScript> ().personagem = "Todos";
				g.GetComponent<CardScript> ().cardname = "Torre Protetora";
				g.GetComponent<CardScript> ().descrition = "Constroi a torre para ajudar no campo";
				g.GetComponent<CardScript> ().cost = "110";
				g.GetComponent<CardScript> ().damage = "5";
				g.GetComponent<CardScript> ().efect = Efects[2];
				g.GetComponent<CardScript> ().image = Images[cardID];
				g.GetComponent<CardScript> ().peson = Persons[0];
				g.transform.SetParent (this.transform,false);
				g.GetComponent<CardScript> ().cardInfo = CardInfo;
				childrens.Add (g.transform);
				break;
			}

		}
	}

//	public void UpdateChildrens(){
//		Transform[] filter = this.gameObject.GetComponentsInChildren<Transform> ();
//		foreach (Transform f in filter)
//		{
//			if(f.name == "Card"){
//			f.transform.gameObject.SetActive(true);
//			childrens.Add(f);
//			}  
//		}
//	}

}
