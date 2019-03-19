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

	void Awake(){

		Chestsqtd = 3;


//		if(PlayerPrefs.HasKey("ChestsNumber")){
//			Chestsqtd = PlayerPrefs.GetInt ("ChestsNumber");
//		}
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
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Chestsqtd; i++) {

			ulong diff = (datetimers [i] - (ulong)DateTime.Now.Ticks);
			ulong m = diff / TimeSpan.TicksPerMinute;
			if ((float)m > 0) {
				Chests [i].GetComponent<ChestDateTime> ().Duration.text = m.ToString () + "m";
			} else {
				
				Chests [i].GetComponent<ChestDateTime> ().Duration.text = "0";
			}

		}
	}

	public void OpenChestInfo(int x){
		ChestInfo.SetActive (true);
		ChestInfoText.text = Chests [x].GetComponent<ChestDateTime> ().Duration.text;
		
	}

	public void UpdateChests(){
		if(PlayerPrefs.HasKey("ChestsNumber")){
			Chestsqtd = PlayerPrefs.GetInt ("ChestsNumber");
		}

		for (int i = 0; i < Chestsqtd; i++) {
			Chests [i].transform.GetChild (0).gameObject.SetActive (false);
			Chests [i].transform.GetChild (1).gameObject.SetActive (true);		
		}
	}
}
