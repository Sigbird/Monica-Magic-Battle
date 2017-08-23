using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {
	public Sprite[] TerrainImages;
	public GameObject[] Trees;
	private SpriteRenderer Sr;
	private int rand;
	// Use this for initialization
	void Start () {
		Sr = this.GetComponent<SpriteRenderer> ();
		rand = Random.Range (0, TerrainImages.Length+1);

		Sr.sprite = TerrainImages [rand];
		Trees [rand].SetActive (true);
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
