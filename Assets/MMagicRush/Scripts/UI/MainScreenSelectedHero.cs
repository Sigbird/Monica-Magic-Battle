using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenSelectedHero : MonoBehaviour {

	public Sprite[] HeroCards;
	public Image SelectedHero;

	void OnEnable(){

		if (PlayerPrefs.GetInt ("SelectedCharacter") != null) {
			int x = PlayerPrefs.GetInt ("SelectedCharacter");
			SelectedHero.sprite = HeroCards [x];
		} else {
			SelectedHero.sprite = HeroCards [0];
		}

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
