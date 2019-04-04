using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionScreen : MonoBehaviour {

	public Button monica;
	public Button monica2;
	public Button magali;
	public Button magali2;
	public Button chico;
	public Button chico2;
	public Button cebolinha;
	public Button cebolinha2;
	public Button cascao;
	public Button cascao2;
	public Sprite SelectedHalo;

	public void Start(){
		if (PlayerPrefs.GetInt ("SelectedCharacter") != null) {
			int x = PlayerPrefs.GetInt ("SelectedCharacter");
			switch (x) {
			case 0:
				monica2.image.sprite = SelectedHalo;
				break;
			case 1:
				cebolinha2.image.sprite = SelectedHalo;
				break;
			case 2:
				magali2.image.sprite = SelectedHalo;
				break;
			case 3:
				cascao2.image.sprite = SelectedHalo;
				break;
			case 4:
				chico2.image.sprite = SelectedHalo;
				break;
			default:
				monica2.image.sprite = SelectedHalo;
				break;
			}

		} else {
			monica2.image.sprite = SelectedHalo;
			PlayerPrefs.SetInt ("SelectedCharacter", 0);
		}

		
	}

	// Use this for initialization
	void OnEnable () {
//		if (PlayerPrefs.GetInt ("monica") == 1) {
//			monica.enabled = true;
//			monica2.enabled = true;
//		} else {
//			monica.enabled = false;
//			monica2.enabled = false;
//		}
//
//		if (PlayerPrefs.GetInt ("cebolinha") == 1) {
//			cebolinha.enabled = true;
//			cebolinha2.enabled = true;
//		} else {
//			cebolinha.enabled = true;
//			cebolinha2.enabled = true;
//		}

		if (PlayerPrefs.GetInt ("magali") == 1) {
			magali.gameObject.SetActive (true);
			magali2.gameObject.SetActive (true);
		} else {
			magali.gameObject.SetActive (false);
			magali2.gameObject.SetActive (false);
		}


		if (PlayerPrefs.GetInt ("cascao") == 1) {
			cascao.gameObject.SetActive (true);
			cascao2.gameObject.SetActive (true);
		} else {
			cascao.gameObject.SetActive (false);
			cascao2.gameObject.SetActive (false);
		}

//		if (PlayerPrefs.GetInt ("chico") == 1) {
//			monica.enabled = true;
//			monica2.enabled = true;
//		} else {
//			monica.enabled = true;
//			monica2.enabled = true;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
