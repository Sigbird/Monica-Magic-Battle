using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardInfoScript : MonoBehaviour {
	public bool cardStore;
	public Text cardname;
	public Text descrition;
	public Text Gemcost;
	public Text cost;
	public Text damage;
	public Image efect;
	public Image image;
	public GameObject ImageAnimated;
	public Image character;
	public GameObject lastCard;
	public GameObject cardPurchasedButton;
	public Button purchaseButton;
	public GameObject MinCardsWarning;


	public Sprite[] Efects;
	public Sprite[] Images;
	public Sprite[] Persons;

	public GameObject enableButton;
	public GameObject disableButton;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendCard(GameObject o){
		lastCard = o;
		cardname.text = o.GetComponent<CardScript> ().cardname;
		descrition.text = o.GetComponent<CardScript> ().descrition;
		cost.text = o.GetComponent<CardScript> ().cost;
		Gemcost.text = o.GetComponent<CardScript> ().Gemcost;
		damage.text = o.GetComponent<CardScript> ().damage;
		efect.sprite = o.GetComponent<CardScript> ().efect.sprite;
		image.sprite = o.GetComponent<CardScript> ().image;
		character.sprite = o.GetComponent<CardScript> ().peson;
		Debug.Log (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length);
		if (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length <= 15 && enableButton != null) {
			enableButton.GetComponent<Button> ().interactable = false;
		} else if(enableButton != null) {
			enableButton.GetComponent<Button> ().interactable = true;
		}

		if (cardStore == true) {
			cardPurchasedButton.SetActive (false);
			purchaseButton.interactable = true;
			Debug.Log (o.GetComponent<CardScript> ().CardID);
			foreach (int x in PlayerPrefsX.GetIntArray ("PlayerCardsIDs")) {
				if (x == o.GetComponent<CardScript> ().CardID) {
//					cardPurchasedButton.SetActive (true);
//					purchaseButton.interactable = false;
				}
			}
		}

		if(enableButton != null && disableButton != null)
		if (o.GetComponent<CardScript> ().isactivebutton) {
			enableButton.SetActive (true);
			disableButton.SetActive(false);
		} else {
			enableButton.SetActive (false);
			disableButton.SetActive(true);
		}


	}

	public void DisplayCard(int cardID){ // INGAME CARD INFO
		if (cardPurchasedButton != null) {
			cardPurchasedButton.SetActive (false);
			foreach (int x in PlayerPrefsX.GetIntArray ("PlayerCardsIDs")) {
				if (x == cardID) {
					//cardPurchasedButton.SetActive (true);
				}
			}
		}
		
		switch (cardID) {
		case 1:
			cardname.text = "Nevasca";
			descrition.text = "Aplica dois de dano em todas unidades ininimgas";
			cost.text = "25";
			damage.text = "2";
			efect.sprite = Efects [0];
			image.sprite = Images [cardID];
			ImageAnimated.SetActive (true);
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill2",true);
			character.sprite = Persons[0];
			break;
		case 2:
			cardname.text = "Estalo Magico";
			descrition.text = "Aplica um de dano em uma unidade ininimga";
			cost.text = "10";
			damage.text = "1";
			efect.sprite = Efects [0];
			image.sprite = Images [cardID];
			ImageAnimated.SetActive (true);
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill1",true);
			character.sprite = Persons [0];
//			cardname.text = "Explosão Magica";
//			descrition.text = "Aplica dois de dano em todas unidades ininimgas";
//			cost.text = "10";
//			damage.text = "2";
//			efect.sprite = Efects[0];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
			break;
		case 3:
			cardname.text = "Canja de Galinha";
			descrition.text = "Cura toda sua tropa";
			cost.text = "75";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			ImageAnimated.SetActive (true);
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill3",true);
			character.sprite = Persons[0];
//			cardname.text = "Nevasca";
//			descrition.text = "Aplica dois de dano em todas unidades ininimgas";
//			cost.text = "25";
//			damage.text = "2";
//			efect.sprite = Efects [0];
//			image.sprite = Images [cardID];
//			ImageAnimated.SetActive (true);
//			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill2",true);
//			character.sprite = Persons[0];
			break;
		case 4:
			cardname.text = "Explosão Magica";
			descrition.text = "Aplica dois de dano em todas unidades ininimgas";
			cost.text = "10";
			damage.text = "2";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
//			cardname.text = "Terremoto";
//			descrition.text = "Deixa Tropas Inimigas Lentas";
//			cost.text = "50";
//			damage.text = "2";
//			efect.sprite = Efects[0];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
			break;
		case 5:
			cardname.text = "Terremoto";
			descrition.text = "Deixa Tropas Inimigas Lentas";
			cost.text = "50";
			damage.text = "2";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
//			cardname.text = "Hora da Soneca";
//			descrition.text = "Para Tropas Inimigas";
//			cost.text = "75";
//			damage.text = "2";
//			efect.sprite = Efects[0];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
			break;
		case 6:
			cardname.text = "Hora da Soneca";
			descrition.text = "Para Tropas Inimigas";
			cost.text = "75";
			damage.text = "2";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
//			cardname.text = "Remédio";
//			descrition.text = "Cura seu heroi";
//			cost.text = "5";
//			damage.text = "0";
//			efect.sprite = Efects[0];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
			break;
		case 7:
			cardname.text = "Remédio";
			descrition.text = "Cura seu heroi";
			cost.text = "5";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 8:
			cardname.text = "Escudo";
			descrition.text = "Parotege suas Tropas";
			cost.text = "25";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 9:
			cardname.text = "Grito de Guerra";
			descrition.text = "Fortalece suas tropas";
			cost.text = "100";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 10:
			cardname.text = "Sem Munição";
			descrition.text = "Paralisa torres inimigas";
			cost.text = "50";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;

			//TROPAS

		case 11:
			cardname.text = "Bidu";
			descrition.text = "Chama a unidade Bidu para ajudar";
			cost.text = "5";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[1];
			break;
		case 12:
			cardname.text = "Astronauta";
			descrition.text = "Chama a unidade Astronauta para ajudar";
			cost.text = "3";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 13:
			cardname.text = "Anjinho";
			descrition.text = "Chama a unidade Anjinho para ajudar";
			cost.text = "1";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 14:
			cardname.text = "Jotalhão";
			descrition.text = "Chama a unidade jotalhão para ajudar";
			cost.text = "50";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 15:
			cardname.text = "Piteco";
			descrition.text = "Chama a unidade Piteco para ajudar";
			cost.text = "15";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 16:
			cardname.text = "Penadinho";
			descrition.text = "Chama a unidade Penadinho para ajudar";
			cost.text = "50";
			damage.text = "1";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
		case 17:
			cardname.text = "Louco";
			descrition.text = "Chama a unidade Louco para ajudar";
			cost.text = "100";
			damage.text = "5";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[3];
			break;
		case 18:
			cardname.text = "Sansão";
			descrition.text = "Chama a unidade Sansão para ajudar";
			cost.text = "40";
			damage.text = "3";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[1];
			break;
		case 19:
			cardname.text = "Mingau";
			descrition.text = "Chama a unidade Mingau para ajudar";
			cost.text = "110";
			damage.text = "2";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[2];
			break;
		case 20:
			cardname.text = "Alfredo";
			descrition.text = "Chama a unidade Alfredo para ajudar";
			cost.text = "125";
			damage.text = "2";
			efect.sprite = Efects[1];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;

			//Movimento

		case 21:
			cardname.text = "Mover";
			descrition.text = "Muda a rota do jogador para outra especifica";
			cost.text = "20";
			damage.text = "0";
			efect.sprite = Efects[0];
			image.sprite = Images[cardID];
			character.sprite = Persons[0];
			break;
//		case 22:
//			cardname.text = "Torre de Agua";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "3";
//			damage.text ="1";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 23:
//			cardname.text = "Torre de Desentupidor";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "1";
//			damage.text = "1";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 24:
//			cardname.text = "Torre de Neve";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "50";
//			damage.text = "1";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 25:
//			cardname.text = "Torre da Cura";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "15";
//			damage.text = "1";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 26:
//			cardname.text = "Torre do Tesouro";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text ="50";
//			damage.text = "1";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 27:
//			cardname.text = "Torre do Sono";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text ="100";
//			damage.text = "5";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];;
//			break;
//		case 28:
//			cardname.text = "Torre AntiTorre";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "40";
//			damage.text = "3";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
//		case 29:
//			cardname.text = "Torre Protetora";
//			descrition.text = "Constroi a torre para ajudar no campo";
//			cost.text = "110";
//			damage.text = "5";
//			efect.sprite = Efects[2];
//			image.sprite = Images[cardID];
//			character.sprite = Persons[0];
//			break;
		}


	}

	public void BuyCard(){
		int coins = PlayerPrefs.GetInt ("PlayerCoins");
		if (int.Parse (cost.text) <= coins) {
			int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

			List<int> iList = new List<int> ();

			for (int i = 0; i < original.Length; i++) {
				iList.Add (original [i]);
			}

			iList.Add (lastCard.GetComponent<CardScript> ().CardID);

			int[] finalArray = new int[iList.Count];

			int x = 0;
			foreach (int i in iList) {
				finalArray [x] = i;
				x++;
			}


//		int[] finalArray = new  int[original.Length + 1 ];
//
//		for(int i = 0; i < original.Length; i ++ ) {
//			finalArray[i] = original[i];
//		}
//
//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;


			//ArrayUtility.Add<int>(ref temp,lastCard.GetComponent<CardScript>().CardID);
			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);

			if (PlayerPrefsX.GetIntArray ("SelectedCardsIDs").Length <= 15) {
				ActiveCard ();
			}

			PlayerPrefs.SetInt ("PlayerCoins", coins - int.Parse (cost.text));
			this.gameObject.SetActive (false);
		}
	}

	public void RemoveCard(){

		int[] original = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");

		if (original.Length > 14) {

			List<int> iList = new List<int> ();


			for (int i = 0; i < original.Length; i++) {
				iList.Add (original [i]);
			}

			iList.Remove (lastCard.GetComponent<CardScript> ().CardID);

			int[] finalArray = new int[iList.Count];

			int x = 0;
			foreach (int i in iList) {
				finalArray [x] = i;
				x++;
			}

			PlayerPrefsX.SetIntArray ("PlayerCardsIDs", finalArray);
			DeactiveCard ();
			Destroy (lastCard.gameObject);
			this.gameObject.SetActive (false);
		} else {
			MinCardsWarning.SetActive (true);
		}
	}

	public void ActiveCard(){
		lastCard.GetComponent<CardScript> ().SetActiveButton ();
		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Add (lastCard.GetComponent<CardScript> ().CardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}
			
//		int[] finalArray = new  int[original.Length + 1 ];
//
//		for(int i = 0; i < original.Length; i ++ ) {
//			finalArray[i] = original[i];
//		}

//		finalArray[finalArray.Length - 1] = lastCard.GetComponent<CardScript>().CardID;
		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	public void DeactiveCard(){
		lastCard.GetComponent<CardScript> ().SetDesativeButton ();
		int[] original = PlayerPrefsX.GetIntArray ("SelectedCardsIDs");

		List<int> iList = new List<int>();

		for(int i = 0; i < original.Length; i ++ ) {
			iList.Add (original [i]);
		}

		iList.Remove (lastCard.GetComponent<CardScript> ().CardID);

		int[] finalArray = new int[iList.Count];

		int x = 0;
		foreach (int i in iList) {
			finalArray [x] = i;
			x++;
		}

		PlayerPrefsX.SetIntArray ("SelectedCardsIDs", finalArray);
	}

	public void CloseInfo(){
		Time.timeScale = 1;
		WPScript.UIopen = false;
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill1",false);
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill2",false);
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill3",false);
		ImageAnimated.SetActive (false);
		this.gameObject.SetActive (false);
	}

	public void RecicleCard(){
		Time.timeScale = 1;
		WPScript.UIopen = false;
		GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds += (int)(lastCard.GetComponent<CardSlotScript> ().cardCost / 2);
		GameObject.Find("DeckPile").GetComponent<DeckPileScript>().DrawNewCard(lastCard.GetComponent<CardSlotScript>().CardPosition);
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill1",false);
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill2",false);
		ImageAnimated.GetComponent<Animator> ().SetBool ("Skill3",false);
		ImageAnimated.SetActive (false);
		Destroy (lastCard.gameObject);
		this.gameObject.SetActive (false);
	}

	public void ActivateEffect(){
		if (lastCard.GetComponent<CardSlotScript> ().cardCost < GameObject.Find ("GameController").GetComponent<GameController> ().Diamonds) {
			Time.timeScale = 1;
			WPScript.UIopen = false;
			lastCard.GetComponent<CardSlotScript> ().ActivateCardEffect ();
			lastCard.GetComponent<CardSlotScript> ().UpdateCard ();
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill1", false);
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill2", false);
			ImageAnimated.GetComponent<Animator> ().SetBool ("Skill3", false);
			ImageAnimated.SetActive (false);
			this.gameObject.SetActive (false);
		}
	}

}
