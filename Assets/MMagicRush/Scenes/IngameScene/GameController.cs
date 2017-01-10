using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public string tipo{ get; set; }
	public GameObject Soldado;
	public int Diamonds;
	[HideInInspector]
	public int WarriorCost = 10;
	[HideInInspector]
	public int MageCost = 10;
	[HideInInspector]
	public int LanceiroCost = 10;

	[HideInInspector]
	public int Mine1Value = 0;
	private float Mine1Assist;
	[HideInInspector]
	public int Mine2Value = 0;
	private float Mine2Assist;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		//StartCoroutine (EnemyAI ());
	}
	
	// Update is called once per frame
	void Update () {

		GameObject.Find ("Diamonds").GetComponent<Text> ().text = Diamonds.ToString();
		GameObject.Find ("Cost1").GetComponent<Text> ().text = WarriorCost.ToString();
		GameObject.Find ("Cost2").GetComponent<Text> ().text = LanceiroCost.ToString();
		GameObject.Find ("Cost3").GetComponent<Text> ().text = MageCost.ToString();
//		GameObject.Find ("Mine1Text").GetComponent<Text> ().text = Mine1Value.ToString();
	//	GameObject.Find ("Mine2Text").GetComponent<Text> ().text = Mine1Value.ToString();
		if (Mine1Assist <= 10) {
			Mine1Assist = Mine1Assist + Time.deltaTime * 5;
			Mine1Value = (int)Mine1Assist;
			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().enabled = false;
		} else {
			GameObject.Find ("Mine1Cristal").GetComponent<Image> ().enabled = true;
		}

		if (Mine2Assist <= 10) {
			Mine2Assist = Mine2Assist + Time.deltaTime * 10;
			Mine2Value = (int)Mine2Assist;
			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled = false;
		} else {
			GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled = true;
		}
	
	}

	public void MiningGems(int x){
	
		if (x == 1 && GameObject.Find ("Mine1Cristal").GetComponent<Image> ().enabled == true ) {
			Diamonds = Mine1Value + Diamonds;
			Mine1Assist = 0;
		}else if (x == 2 && GameObject.Find ("Mine2Cristal").GetComponent<Image> ().enabled == true) {
			Diamonds = Mine2Value + Diamonds;
			Mine2Assist = 0;
		}

	}

	public void SummonMinion(int team){
		switch(tipo){
		case "Guerreiro":
			if (Diamonds >= WarriorCost) {
				Diamonds = Diamonds - WarriorCost;
				Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Guerreiro;
				WarriorCost = WarriorCost + 10;
				Soldado.GetComponent<SoldierControler> ().team = team;
				if (team == 1) {
					Instantiate (Soldado, GameObject.Find ("SpawPoint1").transform.position, Quaternion.identity);
				} else {
					Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
				}
			}
			break;
		case "Mago":
			if (Diamonds >= MageCost) {
				Diamonds = Diamonds - MageCost;
				Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Mago;
				MageCost = MageCost + 10;
				Soldado.GetComponent<SoldierControler> ().team = team;
				if (team == 1) {
					Instantiate (Soldado, GameObject.Find ("SpawPoint1").transform.position, Quaternion.identity);
				} else {
					Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
				}
			}
			break;
		case "Lanceiro":
			if (Diamonds >= LanceiroCost) {
				Diamonds = Diamonds - LanceiroCost;
				Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Lanceiro;
				LanceiroCost = LanceiroCost + 10;
				Soldado.GetComponent<SoldierControler> ().team = team;
				if (team == 1) {
					Instantiate (Soldado, GameObject.Find ("SpawPoint1").transform.position, Quaternion.identity);
				} else {
					Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
				}
			}
			break;
		case "General":
			Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.General;
			Soldado.GetComponent<SoldierControler> ().team = team;
			if (team == 1) {
				Instantiate (Soldado, GameObject.Find("SpawPoint1").transform.position, Quaternion.identity);
			} else {
				Instantiate (Soldado, GameObject.Find("SpawPoint2").transform.position, Quaternion.identity);
			}
			break;
		default:
			//
			break;
		}

	}

	IEnumerator EnemyAI(){
		switch(Random.Range(0,4)){
		case 1:
			Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Guerreiro;
			Soldado.GetComponent<SoldierControler> ().team = 2;
			Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
		
			break;
		case 2:
			Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Lanceiro;
			Soldado.GetComponent<SoldierControler> ().team = 2;
			Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
			break;
		case 3:
			Soldado.GetComponent<SoldierControler> ().Tipo = SoldierControler.TipoSoldado.Mago;
			Soldado.GetComponent<SoldierControler> ().team = 2;
			Instantiate (Soldado, GameObject.Find ("SpawPoint2").transform.position, Quaternion.identity);
			break;
		default:
			//
			break;
		}

		yield return new WaitForSeconds (Random.Range(3,7));

		StartCoroutine (EnemyAI ());
	}

	public void CallScene(string scene){
		if (scene == "quit") {
			Application.Quit ();
		}else if (scene == "start") {
			Time.timeScale = 1;
		} else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}


}
