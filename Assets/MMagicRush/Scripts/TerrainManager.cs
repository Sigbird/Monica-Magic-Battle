using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {
	public Sprite[] TerrainImages;
	public GameObject[] Trees;
	private SpriteRenderer Sr;
	private int rand;
	public string terrain;
	// Use this for initialization
	void Start () {

		Sr = this.GetComponent<SpriteRenderer> ();

		terrain = PlayerPrefs.GetString ("TerrainType");

		if (terrain == "Forest") {
			Sr.sprite = TerrainImages[0];
			Trees [0].SetActive (true);
		}
		if (terrain == "Dungeon") {
			Sr.sprite = TerrainImages[1];
			Trees [1].SetActive (true);
		}
		if (terrain == "Winter") {
			Sr.sprite = TerrainImages[2];
			Trees [2].SetActive (true);
		}



		Sr = this.GetComponent<SpriteRenderer> ();
//		rand = Random.Range (0, TerrainImages.Length);
//
//		Sr.sprite = TerrainImages [rand];
//		Trees [rand].SetActive (true);
//		switch (rand) {
//		case 0:
//			break;
//		case 1:
//			break;
//		case 2:
//			break;
//		default:
//			break;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
