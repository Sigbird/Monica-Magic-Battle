using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTestesVariables : MonoBehaviour {
	public int SelectedOption;
	public GameObject HeroTroops;
	public GameObject EnemyTroops;
	public WPIASoldierControler ControllerInimigo;
	public WPSoldierControler ControllerHeroi;
	public GameObject BaseModel;
	public GameObject TowerModel;
	public InputField[] InputTexts;
	public Text TroopName;
	public int selectedtroop;
	public int selectedHero;
	// Use this for initialization
	void Start () {
		
		//ControllerHeroi = GameObject.Find ("Hero").GetComponent<WPSoldierControler> ();
		//ControllerInimigo = GameObject.Find ("HeroEnemy").GetComponent<WPIASoldierControler> ();


		switch (SelectedOption) {
		case 1://Heroi
			InputTexts [0].text = ControllerHeroi.vida.ToString ();
			InputTexts [1].text = ControllerHeroi.damage.ToString ();
			InputTexts [2].text = ControllerHeroi.damageSpeed.ToString ();
			InputTexts [3].text = ControllerHeroi.range.ToString ();
			InputTexts [4].text = ControllerHeroi.speed.ToString ();
			selectedHero = ControllerHeroi.heroID;
			break;
		case 2://HeroiOponente
			InputTexts [0].text = ControllerInimigo.vida.ToString ();
			InputTexts [1].text = ControllerInimigo.damage.ToString ();
			InputTexts [2].text = ControllerInimigo.damageSpeed.ToString ();
			InputTexts [3].text = ControllerInimigo.range.ToString ();
			InputTexts [4].text = ControllerInimigo.speed.ToString ();
			selectedHero = ControllerInimigo.heroID;
			break;
		case 3://TropaAliada
			break;
		case 4://TropaInimiga
			break;
		case 5://Torres
			InputTexts [0].text = TowerModel.GetComponent<ChargesScriptTowers>().vidaMax.ToString ();
			InputTexts [1].text = TowerModel.GetComponent<BaseDefense>(). damage.ToString ();
			InputTexts [2].text = TowerModel.GetComponent<BaseDefense>().damageSpeed.ToString ();
			InputTexts [3].text = TowerModel.GetComponent<BaseDefense>().reach.ToString ();
			break;
		case 6://Bases
			InputTexts [0].text = BaseModel.GetComponent<ChargesScript>().vidaMax.ToString ();
			InputTexts [1].text = BaseModel.GetComponent<BaseDefense>().damage.ToString ();
			InputTexts [2].text = BaseModel.GetComponent<BaseDefense>().damageSpeed.ToString ();
			InputTexts [3].text = BaseModel.GetComponent<BaseDefense>().reach.ToString ();
			break;
		default:
			break;
		}



	}
	
	// Update is called once per frame
	void Update () {
		if (InputTexts.Length>= 6) {
			if (InputTexts [5].text != "") {
				selectedtroop = int.Parse (InputTexts [5].text);
			}
		}
	}

	public void SelectOption(int x){
		SelectedOption = x;
	}

	public void ApplyChanges(){
		switch (SelectedOption) {
		case 1://Heroi
			ControllerHeroi.vida = int.Parse(InputTexts [0].text);
			ControllerHeroi.damage = int.Parse(InputTexts [1].text);
			ControllerHeroi.damageSpeed = float.Parse(InputTexts [2].text);
			ControllerHeroi.range = float.Parse(InputTexts [3].text);
			ControllerHeroi.speed = float.Parse(InputTexts [4].text);
			break;
		case 2://HeroiOponente
			ControllerInimigo.vida = int.Parse(InputTexts [0].text);
			ControllerInimigo.damage = int.Parse(InputTexts [1].text);
			ControllerInimigo.damageSpeed = float.Parse(InputTexts [2].text);
			ControllerInimigo.range = float.Parse(InputTexts [3].text);
			ControllerInimigo.speed = float.Parse(InputTexts [4].text);
			break;
		case 3://TropaAliada
			HeroTroops.GetComponent<SoldierControler> ().Custom = true;
			HeroTroops.GetComponent<SoldierControler> ().vida = int.Parse(InputTexts [0].text);
			HeroTroops.GetComponent<SoldierControler> ().vidaMax = int.Parse(InputTexts [0].text);
			HeroTroops.GetComponent<SoldierControler> ().damage = int.Parse(InputTexts [1].text);
			HeroTroops.GetComponent<SoldierControler> ().damageSpeed = float.Parse(InputTexts [2].text);;
			HeroTroops.GetComponent<SoldierControler> ().range = float.Parse(InputTexts [3].text);
			HeroTroops.GetComponent<SoldierControler> ().speed = float.Parse(InputTexts [4].text);
			Instantiate (HeroTroops, GameObject.Find("HeroBase").transform.position, Quaternion.identity);
			break;
		case 4://TropaInimiga
			EnemyTroops.GetComponent<SoldierControler> ().Custom = true;
			EnemyTroops.GetComponent<SoldierControler> ().vida = int.Parse(InputTexts [0].text);
			EnemyTroops.GetComponent<SoldierControler> ().vidaMax = int.Parse(InputTexts [0].text);
			EnemyTroops.GetComponent<SoldierControler> ().damage = int.Parse(InputTexts [1].text);
			EnemyTroops.GetComponent<SoldierControler> ().damageSpeed = float.Parse(InputTexts [2].text);;
			EnemyTroops.GetComponent<SoldierControler> ().range = float.Parse(InputTexts [3].text);
			EnemyTroops.GetComponent<SoldierControler> ().speed = float.Parse(InputTexts [4].text);
			Instantiate (EnemyTroops, GameObject.Find("HeroBaseEnemy").transform.position, Quaternion.identity);
			break;
		case 5://Torres
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("enemytower1")) {
				if (item.GetComponent <ChargesScriptTowers>() != null) {
					item.GetComponent<ChargesScriptTowers> ().vida = float.Parse(InputTexts [0].text);
					item.GetComponent<ChargesScriptTowers> ().vidaMax = float.Parse(InputTexts [0].text);
					item.GetComponent<BaseDefense>().damage = int.Parse(InputTexts [1].text);
					item.GetComponent<BaseDefense>().damageSpeed = float.Parse(InputTexts [2].text);
					item.GetComponent<BaseDefense>().reach = float.Parse(InputTexts [3].text);
				}
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("enemytower2")) {
				if (item.GetComponent <ChargesScriptTowers>() != null) {
					item.GetComponent<ChargesScriptTowers> ().vida = float.Parse(InputTexts [0].text);
					item.GetComponent<ChargesScriptTowers> ().vidaMax = float.Parse(InputTexts [0].text);
					item.GetComponent<BaseDefense>().damage = int.Parse(InputTexts [1].text);
					item.GetComponent<BaseDefense>().damageSpeed = float.Parse(InputTexts [2].text);
					item.GetComponent<BaseDefense>().reach = float.Parse(InputTexts [3].text);
				}
			}


			break;
		case 6://Bases
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("enemytower1")) {
				if (item.GetComponent <ChargesScript>() != null) {
					item.GetComponent<ChargesScript> ().vida = float.Parse(InputTexts [0].text);
					item.GetComponent<ChargesScript> ().vidaMax = float.Parse(InputTexts [0].text);
					item.GetComponent<BaseDefense>().damage = int.Parse(InputTexts [1].text);
					item.GetComponent<BaseDefense>().damageSpeed = float.Parse(InputTexts [2].text);
					item.GetComponent<BaseDefense>().reach = float.Parse(InputTexts [3].text);
				}
			}
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("enemytower2")) {
				if (item.GetComponent <ChargesScript>() != null) {
					item.GetComponent<ChargesScript> ().vida = float.Parse(InputTexts [0].text);
					item.GetComponent<ChargesScript> ().vidaMax = float.Parse(InputTexts [0].text);
					item.GetComponent<BaseDefense>().damage = int.Parse(InputTexts [1].text);
					item.GetComponent<BaseDefense>().damageSpeed = float.Parse(InputTexts [2].text);
					item.GetComponent<BaseDefense>().reach = float.Parse(InputTexts [3].text);
				}
			}
			break;
		default:
			break;
		}
	}

	public void SaveChangesHero(){
		switch (selectedHero) {
		case(0): // Monica
			PlayerPrefs.SetFloat ("MonicaAtrib1", float .Parse(InputTexts [1].text) ); //Dano = Alto
			PlayerPrefs.SetFloat ("MonicaAtrib2", float.Parse(InputTexts [2].text)); //VelDano = Baixo
			PlayerPrefs.SetFloat ("MonicaAtrib3", float.Parse(InputTexts [3].text)); //AlcanceDano = Medio
			PlayerPrefs.SetFloat ("MonicaAtrib4", float.Parse(InputTexts [0].text)); //Life = Alto
			PlayerPrefs.SetFloat ("MonicaAtrib5", float.Parse(InputTexts [4].text)); //Movimento = Baixo
			break;
		case(1): // ASTRONAUTA
			PlayerPrefs.SetFloat ("CebolinhaAtrib1", float .Parse(InputTexts [1].text) ); //Dano = Alto
			PlayerPrefs.SetFloat ("CebolinhaAtrib2", float.Parse(InputTexts [2].text)); //VelDano = Baixo
			PlayerPrefs.SetFloat ("CebolinhaAtrib3", float.Parse(InputTexts [3].text)); //AlcanceDano = Medio
			PlayerPrefs.SetFloat ("CebolinhaAtrib4", float.Parse(InputTexts [0].text)); //Life = Alto
			PlayerPrefs.SetFloat ("CebolinhaAtrib5", float.Parse(InputTexts [4].text)); //Movimento = Baixo
			break;
		case(2): //ANJINHO -> Cranicola
			PlayerPrefs.SetFloat ("MagaliAtrib1", float .Parse(InputTexts [1].text) ); //Dano = Alto
			PlayerPrefs.SetFloat ("MagaliAtrib2", float.Parse(InputTexts [2].text)); //VelDano = Baixo
			PlayerPrefs.SetFloat ("MagaliAtrib3", float.Parse(InputTexts [3].text)); //AlcanceDano = Medio
			PlayerPrefs.SetFloat ("MagaliAtrib4", float.Parse(InputTexts [0].text)); //Life = Alto
			PlayerPrefs.SetFloat ("MagaliAtrib5", float.Parse(InputTexts [4].text)); //Movimento = Baixo
			break;
		case(3): //ANJINHO -> Cranicola
			PlayerPrefs.SetFloat ("CascaoAtrib1", float .Parse(InputTexts [1].text) ); //Dano = Alto
			PlayerPrefs.SetFloat ("CascaoAtrib2", float.Parse(InputTexts [2].text)); //VelDano = Baixo
			PlayerPrefs.SetFloat ("CascaoAtrib3", float.Parse(InputTexts [3].text)); //AlcanceDano = Medio
			PlayerPrefs.SetFloat ("CascaoAtrib4", float.Parse(InputTexts [0].text)); //Life = Alto
			PlayerPrefs.SetFloat ("CascaoAtrib5", float.Parse(InputTexts [4].text)); //Movimento = Baixo
			break;
		default:
			break;
		}
	}



	public void SelectTroop(){

		switch (selectedtroop) {
		case(1): // BIDU
			InputTexts[0].text = "";
			InputTexts[0].text = "75";//Medio
			InputTexts[1].text = "22";//Medio
			InputTexts[2].text = "0.5";//Medio
			InputTexts[3].text = "1"; //Baixissimo
			InputTexts[4].text = "1.3"; //Medio
			TroopName.text = "Bidu";
			break;
		case(2): // ASTRONAUTA
			InputTexts[0].text = "35";//Baixo
			InputTexts[1].text = "22"; //Medio
			InputTexts[2].text = "1"; //Baixo
			InputTexts[3].text = "3"; //Alto
			InputTexts[4].text = "1.3"; //Medio
			TroopName.text = "Astronauta";
			break;
		case(3): //ANJINHO -> Cranicola
			InputTexts[0].text = "15"; //Baixissimo
			InputTexts[1].text = "50"; //Alto
			InputTexts[2].text = "0.25f"; //Altissimo
			InputTexts[3].text = "1"; //Baixissimo
			InputTexts[4].text = "1.7"; //Alto
			TroopName.text = "Cranicola";
			break;
		case(4): //JOTALHÃO
			InputTexts[0].text = "200"; //Alto
			InputTexts[1].text = "50"; //Alto
			InputTexts[2].text = "1"; //Baixo
			InputTexts[3].text = "1"; //Baixissimo
			InputTexts[4].text = "0.8"; //Baixo
			TroopName.text  = "Jotalhão";
			break;
		case(5): //PITECO
			InputTexts[0].text = "35"; //Baixo
			InputTexts[1].text = "22"; // Medio
			InputTexts[2].text = "0.5"; //medio
			InputTexts[3].text = "2"; //Medio
			InputTexts[4].text = "1.3"; //Medio
			TroopName.text  = "Piteco";
			break;
		case(6): //PENADINHO
			InputTexts[0].text = "75"; //Medio
			InputTexts[1].text = "22"; //Medio
			InputTexts[2].text = "0.5"; //Medio
			InputTexts[3].text = "2"; //Medio
			InputTexts[4].text = "0.8"; //Baixo
			TroopName.text  = "Penadinho";
			break;
		case(7): //MAURICIO -> off
			break;
		case(8): //SANSAO
			InputTexts[0].text = "35"; //Baixo
			InputTexts[1].text = "14"; //Baixo
			InputTexts[2].text = "0.5"; //Medio
			InputTexts[3].text = "1"; //Baixissimo
			InputTexts[4].text = "1.3"; //Medio
			InputTexts[5].text = "Sansao";
			break;
		case(9): //MINGAU
			InputTexts[0].text = "75"; //Medio
			InputTexts[1].text = "22"; //Medio
			InputTexts[2].text = "0.33";  //Alto
			InputTexts[3].text = "1"; //Baixissimo
			InputTexts[4].text = "1.7"; //Alto
			TroopName.text = "Mingau";
			break;
		case(10): //ALFREDO
			break;
		default:
			break;
		}
	}
}
