using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

	public int CardID;
	public bool tutorial;
	public bool cardshop;
	public bool collectionCard;

	public bool isactivebutton;
	public Image activebutton;
	public Sprite activeTrue;
	public Sprite activeFalse;

	public GameObject PriceTag;
	public Text PricetagValue;


	public string efeito;
	public string personagem;

	public string cardname;
	public string descrition;
	public string descritionLong;
	public string Gemcost;
	public string cost;
	public string damage;
	public Image efect;
	public Sprite image;
	public Sprite peson;

	public Sprite[] Efects;
	public Sprite[] Images;
	public Sprite[] ImagesBW;
	public Sprite[] Persons;
	public GameObject CardInfo;
	public bool disabledcard;


	public GameObject TutorialHand;

	public GameObject cardInfo;

	// Use this for initialization
	void Start () {
		
		this.GetComponent<Image> ().sprite = image;


	}

	void OnEnable(){
//		if (collectionCard) {
//			UpdateCollectionCard ();
//		}
		StartCoroutine (LateStart (0.1f));
	}

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		int[] enableds = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
		//if (CardShop == false) {
		foreach (int enab in enableds) {
			//Debug.Log ("Carta ativa!: " + enab +" "+ this.CardID);
			if (enab == this.CardID) {
				Debug.Log ("Carta ativa!: " + enab + this.CardID);
				//g.GetComponent<CardScript> ().activebutton.sprite = g.GetComponent<CardScript> ().activeTrue;
				isactivebutton = true;
				break;
			} else {
				isactivebutton = false;
			}
		}
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (isactivebutton == true) {
			activebutton.sprite = activeTrue;
		} else {
			activebutton.sprite = activeFalse;
		}
	
		GetComponent<Image> ().sprite = image;

	}

	public void OpenCardInfo(){
		
		if (tutorial == true && activebutton.isActiveAndEnabled == false) {
			if(GameObject.Find ("Hand") != null)
				GameObject.Find ("Hand").SetActive (false);
			
		}else if (tutorial == true) {
			GameObject.Find ("TutorialController").GetComponent<TutorialMain> ().ClickOnCard ();

			if(GameObject.Find ("TutorialHandP2") != null)
				GameObject.Find ("TutorialHandP2").SetActive (false);

			if (GameObject.Find ("CardShop") != null)
				GameObject.Find ("CardShop").GetComponent<Button> ().interactable = true;
		}
			

		
		cardInfo.SetActive (true);
		cardInfo.GetComponent<CardInfoScript> ().SendCard (this.gameObject);
	}

	public void UpdateCollectionCard(){
		
		switch (this.CardID) {
		case 0:
			CardID = 0;
			efeito = "Magia";
			personagem = "Todos";
			cardname = "Nevasca";
			descrition = "Aplica dois de dano em todas unidades ininimgas";
			descritionLong = "Nevasca \n Congela tropas e herois inimigos causando dano leve a estes temporariamente.";
			Gemcost = "25";
			cost = "50";
			damage = "2";
			efect.sprite = Efects [3];
			if (!disabledcard) {
				image = Images [CardID];
			} else {
				image = ImagesBW [CardID];
			}
			peson = Persons [0];
			
			break;
		case 1:
			CardID = 1;
			efeito = "Magia";
			personagem = "Todos";
			cardname = "Nevasca";
			descrition = "Aplica dois de dano em todas unidades ininimgas";
			descritionLong = "Nevasca \n Congela tropas e herois inimigos causando dano leve a estes temporariamente.";
			Gemcost = "25";
			cost = "50";
			damage = "2";
			efect.sprite = Efects [0];
			if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
			peson = Persons [0];
			
				break;
			case 2:
				CardID = 2;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Estalo Magico";
				descrition = "Aplica um de dano em uma unidade ininimga";
				descritionLong = "Estalo Magico \n Causa um dano repentino na unidade heroica do adversário.";
				cost = "25";
				Gemcost = "10";
				damage = "1";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			

				break;
			case 3:
				CardID = 3;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Canja de Galinha";
				descrition = "Cura toda sua tropa";
				descritionLong = "Canja de Galinha \n Canja faz muito bem pra saude, alimenta e recupera as energias do seu heroi e amigos.";
				cost = "100";
				Gemcost = "75";
				damage = "0";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 4:
				CardID = 4;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Explosão Mágica";
				descrition = "Aplica Dano a Todas as Unidades";
				descritionLong = "Explosão Magica \n Grande explosão mágica causando dano a todas as unidades no raio.";
				cost = "10";
				Gemcost = "2";
				damage = "1";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
				
				break;
			case 5:
				CardID = 5;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Terremoto";
				descrition = "Abala o chão sob o adversário";
				descritionLong = "Terremoto \n Tremor repentino que pode causar dano a todas as tropas adversarias alem de dificultar sua movimentação.";
				cost = "50";
				Gemcost = "10";
				damage = "3";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 6:
				CardID = 6;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Hora da Soneca";
				descrition = "Poe o heroi adversário para dormir.";
				descritionLong = "Hora da soneca \n Provoca um sono repentino no heroi adversário botando ele para tirar um cochilinho.";
				cost = "75";
				Gemcost = "25";
				damage = "0";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 7:
				CardID = 7;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Remédio";
				descrition = "Cura toda sua tropa em 2 pontos";
				descritionLong = "Remedio \n Bom e velho remedinho caseiro da vóvó, recupera boa parte da vida do seu heroi.";
				cost = "5";
				Gemcost = "15";
				damage = "0";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 8:
				CardID = 8;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Escudo";
				descrition = "Parotege suas Tropas em 3 pontos";
				descritionLong = "Escudo \n Bloqueia temporariamente parte do dano recebido por seu personagem.";
				cost = "25";
				Gemcost = "15";
				damage = "50";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 9:
				CardID = 9;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Grito de Guerra";
				descrition = "Fortalece suas tropas";
				descritionLong = "Grito de Guerra \n Chama toda a molecada e vamos com tudo pra cima deles! Aumenta motivação de suas tropas..";
				cost = "10";
				Gemcost = "15";
				damage = "0";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 10:
				CardID = 10;
				efeito = "Magia";
				personagem = "Todos";
				cardname = "Sem Munição";
				descrition = "Paralisa torres inimigas";
				descritionLong = "Sem Munição \n Sabota e deixa as torres do adversário sem munição.";
				cost = "10";
				Gemcost = "15";
				damage = "0";
				efect.sprite = Efects [0];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;

				//TROPAS

			case 11:
				CardID = 11;
				efeito = "Unidade";
				personagem = "Monica";
				cardname = "Bidu";
				descrition = "Chama a unidade Bidu para ajudar";
				cost = "1";
				Gemcost = "5";
				damage = "1";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [1];
			
				break;
			case 12:

				CardID = 12;
				efeito = "Unidade";
				personagem = "Todos";
				cardname = "Astronauta";
				descrition = "Chama a unidade Astronauta para ajudar";
				cost = "3";
				Gemcost = "1";
				damage = "1";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 13: //removido (Anjinho)
				CardID = 13;
				efeito = "Unidade";
				personagem = "Todos";
				cardname = "Jotalhão";
				descrition = "Chama a unidade jotalhão para ajudar";
				cost = "50";
				Gemcost = "75";
				damage = "1";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 14:
				CardID = 14;
				efeito = "Unidade";
				personagem = "Todos";
				cardname = "Piteco";
				descrition = "Chama a unidade Piteco para ajudar";
				cost = "15";
				Gemcost = "75";
				damage = "1";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 15:
				CardID = 15;
				efeito = "Unidade";
				personagem = "Todos";
				cardname = "Penadinho";
				descrition = "Chama a unidade Penadinho para ajudar";
				cost = "50";
				Gemcost = "125";
				damage = "1";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [0];
			
				break;
			case 16:
				CardID = 16;
				efeito = "Unidade";
				personagem = "Monica";
				cardname = "Sansão";
				descrition = "Chama a unidade Sansão para ajudar";
				cost = "40";
				Gemcost = "50";
				damage = "3";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [1];
			
				break;
			case 17: // Mingau
				CardID = 17;
				efeito = "Unidade";
				personagem = "Magali";
				cardname = "Mingau";
				descrition = "Chama a unidade Mingau para ajudar";
				cost = "110";
				Gemcost = "150";
				damage = "2";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];

			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [2];
			
				break;
			case 18: // Cranicola
				CardID = 18;
				efeito = "Unidade";
				personagem = "Magali";
				cardname = "Cranicola";
				descrition = "Chama a unidade Cranicola para ajudar";
				cost = "50";
				Gemcost = "5";
				damage = "120";
				efect.sprite = Efects [1];
				if (!disabledcard) {
				GetComponent<Button> ().interactable = true;
				image = Images [CardID];
			} else {
				GetComponent<Button> ().interactable = false;
				image = ImagesBW [CardID];
			}
				peson = Persons [2];
			
				break;

			default:

			CardID = 0;
			efeito = "Magia";
			personagem = "Todos";
			cardname = "Nevasca";
			descrition = "Aplica dois de dano em todas unidades ininimgas";
			descritionLong = "Nevasca \n Congela tropas e herois inimigos causando dano leve a estes temporariamente.";
			Gemcost = "25";
			cost = "50";
			damage = "2";
			efect.sprite = Efects [3];
			if (!disabledcard) {
				image = Images [CardID];
			} else {
				image = ImagesBW [CardID];
			}
			peson = Persons [0];

			break;

			}
	}

	public void SetActiveButton(){
		if(activebutton != null)
		activebutton.sprite = activeTrue;
		isactivebutton = true;
	}

	public void SetDesativeButton(){
		activebutton.sprite = activeFalse;
		isactivebutton = false;
	}

	public void ActiveCard(){
		if(activebutton != null)
			activebutton.sprite = activeTrue;
		isactivebutton = true;

		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Add (this.CardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		//finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);

//		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");
//
//		int[] finalArray = new  int[original.Length + 1 ];
//
//		for(int i = 0; i < original.Length; i ++ ) {
//			finalArray[i] = original[i];
//		}
//
//		finalArray[finalArray.Length - 1] = CardID;
//		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	public void DeactiveCard(){


		if (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length > 15) {// VERIFICA SE TEM PELO MENOS 15 CARTAS SELECIONADAS
			activebutton.sprite = activeFalse;
			isactivebutton = false;
			int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

			List<int> iList = new List<int> ();

			for (int i = 0; i < original.Length; i++) {
				iList.Add (original [i]);
			}

			iList.Remove (CardID);

			int[] finalArray = new int[iList.Count];

			int x = 0;
			foreach (int i in iList) {
				finalArray [x] = i;
				x++;
			}

			PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
		}
	}

	public int[] CleanArray(int[] arr){




		List<int> list2 = new List<int>();
		foreach (int x in arr) {
			if (!list2.Contains (x)) {
				list2.Add (x);
			}
		}

		return list2.ToArray ();

	}

	public List<int> CleanArraytoList(int[] arr){




		List<int> list2 = new List<int>();
		foreach (int x in arr) {
			if (!list2.Contains (x)) {
				list2.Add (x);
			}
		}

		return list2;

	}


}
