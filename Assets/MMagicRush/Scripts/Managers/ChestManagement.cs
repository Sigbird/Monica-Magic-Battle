using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChestManagement : MonoBehaviour {

	public GameObject[] Chests;
	public ulong[] datetimers;
	public int Chestsqtd;
	public GameObject ChestInfo;
	public Text ChestInfoText;
	public GameObject OpenChestProcedure;
	public SceneHelper controller;

	void Awake(){

		PlayerPrefs.SetInt ("ChestsNumber", 3);


		if(PlayerPrefs.HasKey("ChestsNumber")){
			Chestsqtd = PlayerPrefs.GetInt ("ChestsNumber");
		}
	}

	// Use this for initialization
	void Start () {

		for (int i = 0; i < Chestsqtd; i++) {
			//PlayerPrefs.DeleteKey ("Chest" + i);
			if (!PlayerPrefs.HasKey ("Chest" + i)) {
				datetimers [i] = (ulong)DateTime.Now.AddMinutes (30).Ticks;
				PlayerPrefs.SetString ("Chest" + i, datetimers [i].ToString ());
			} else {
				datetimers [i] = ulong.Parse (PlayerPrefs.GetString ("Chest" + i));
			}

			Chests [i].transform.GetChild (0).gameObject.SetActive (false);
			Chests [i].transform.GetChild (1).gameObject.SetActive (true);		
//			if (Chests [i].GetComponent<ChestDateTime> ().hasDateTime == false) {
//				Chests [i].GetComponent<ChestDateTime> ().Abertura.AddMinutes (50);
//				Chests [i].GetComponent<ChestDateTime> ().hasDateTime = true;
//			}
		}
	}

	public void ResetChests(){
		for (int i = 0; i < Chestsqtd; i++) {
			PlayerPrefs.DeleteKey ("Chest" + i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Chestsqtd; i++) {

			ulong diff = (datetimers [i] - (ulong)DateTime.Now.Ticks);
			ulong m = diff / TimeSpan.TicksPerMinute;
			if ((float)m < 100) {
				Chests [i].GetComponent<ChestDateTime> ().Duration.text = m.ToString () + "m";
				Chests [i].GetComponent<ChestDateTime> ().Ready = false;
				Chests [i].GetComponent<ChestDateTime> ().Cadeado.SetActive (true);

			} else {
				Chests [i].GetComponent<ChestDateTime> ().Ready = true;
				Chests [i].GetComponent<ChestDateTime> ().Duration.text = "Pronto";
				Chests [i].GetComponent<ChestDateTime> ().Cadeado.SetActive (false);
			}

		}
	}

	public void OpenChestInfo(int x){
		if (Chests [x].GetComponent<ChestDateTime> ().Ready == false) {
			ChestInfo.SetActive (true);
			ChestInfoText.text = Chests [x].GetComponent<ChestDateTime> ().Duration.text;
		} else {
			//Abrir Bau Anim
			OpenChestProcedure.SetActive(true);
		}
		
	}

	public void OpenChestByGold(){
		if (999<int.Parse(controller.premiumcoinsText.text)) {
			PlayerPrefs.SetInt ("PlayerCoins", (int.Parse (controller.premiumcoinsText.text) - 999));
			OpenChestProcedure.SetActive(true);
			ChestInfo.SetActive (false);
		} else {
			//Abrir Bau Anim
			//OpenChestProcedure.SetActive(true);
		}

	}

	public void OpenChestByVideo(){
			ChestInfo.SetActive (false);
			OpenChestProcedure.SetActive(true);

	}


	public void UpdateChests(){
		if(PlayerPrefs.HasKey("ChestsNumber")){
			Chestsqtd = PlayerPrefs.GetInt ("ChestsNumber");
		}

		for (int i = 0; i < 4; i++) {
			Chests [i].transform.GetChild (0).gameObject.SetActive (true);
			Chests [i].transform.GetChild (1).gameObject.SetActive (false);		
		}

		for (int i = 0; i < Chestsqtd; i++) {
			Chests [i].transform.GetChild (0).gameObject.SetActive (false);
			Chests [i].transform.GetChild (1).gameObject.SetActive (true);		
		}
	}
}
