using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRespawn : MonoBehaviour {

	public GameObject GemPrefab;
	public int gemvalue;
	public int enemgemvalue;
	public List<int> usedValues = new List<int>();
	public List<int> enemusedValues = new List<int>();
	public int gemcounter;
	public int enemgemcounter;
	public int remainingPos = 3;
	public int posTaken1;
	public int posTaken2;
	public int remainingPosEnem = 3;
	private int posTaken1Enem;
	private int posTaken2Enem;

	// Use this for initialization
	void Start () {
		gemvalue = 0;
		enemgemvalue = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (gemvalue >= 50) {
			//usedValues.Clear ();
			gemvalue = 0;
		}

		if (enemgemvalue >= 50) {
			//enemusedValues.Clear ();
			enemgemvalue = 0;
		}


		if (gemcounter < 3) {
			StartCoroutine (InstatiateGem ());
			gemcounter++;
		}

		if (enemgemcounter < 3) {
			StartCoroutine (InstatiateEnemyGem ());
			enemgemcounter++;
		}

	}

	public int GemCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("gem");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}

	public int EnemyGemCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("enemygem");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}

	public Vector2 GetRandomPosition(){

//		int val = Random.Range(0, 3);
//		while(usedValues.Contains(val))
//		{
//			val = Random.Range(0, 3);
//		}
//		usedValues.Add (val);

		switch (RandomInt()) {
		case 1:
			return new Vector2 (-2.5f, -0.40f);
			break;
		case 2: 
			return new Vector2 (0f, -2.5f);
			break;
		case 3: 
			return new Vector2 (2.5f, -2f);
			break;
		default:
			return new Vector2 (-2.5f, -0.40f);
			break;
		}
	}

	public Vector2 GetEnemyRandomPosition(){

//		int val = Random.Range(0, 3);
//		while(enemusedValues.Contains(val))
//		{
//			val = Random.Range(0, 3);
//		}
//		enemusedValues.Add (val);

		switch (RandomIntEnem()) {
		case 1:
			return new Vector2 (2.5f, 1f);
			break;
		case 2: 
			return new Vector2 (0f, 3.5f);
			break;
		case 3: 
			return new Vector2 (-2.5f, 2.3f);
			break;
		default:
			return new Vector2 (2.5f, 1f);
			break;
		}
	}

	IEnumerator InstatiateGem(){
		yield return new WaitForSeconds (Random.Range (2, 10));
		gemvalue += 10;
		GemPrefab.GetComponent<GemScript> ().gemvalue = gemvalue;
		GemPrefab.GetComponent<GemScript> ().team = 1;
		GemPrefab.tag = "gem";
		Instantiate (GemPrefab, GetRandomPosition(), Quaternion.identity);
	}

	IEnumerator InstatiateEnemyGem(){
		yield return new WaitForSeconds (Random.Range (2, 10));
		enemgemvalue += 10;
		GemPrefab.GetComponent<GemScript> ().gemvalue = enemgemvalue;
		GemPrefab.GetComponent<GemScript> ().team = 2;
		GemPrefab.tag = "enemygem";
		Instantiate (GemPrefab, GetEnemyRandomPosition(), Quaternion.identity);
	}

	public int RandomIntEnem(){
		int x = 0;

		switch (remainingPosEnem) {
		case 1:
			if (posTaken1Enem == 1 && posTaken2Enem == 2) {
				x = 3;
			}
			if (posTaken1Enem == 2 && posTaken2Enem == 1) {
				x = 3;
			}
			if (posTaken1Enem == 1 && posTaken2Enem == 3) {
				x = 2;
			}
			if (posTaken1Enem == 3 && posTaken2Enem == 1) {
				x = 2;
			}
			if (posTaken1Enem == 2 && posTaken2Enem == 3) {
				x = 1;
			}
			if (posTaken1Enem == 3 && posTaken2Enem == 2) {
				x = 1;
			}
			remainingPosEnem--;
			break;
		case 2: 
			if (posTaken1Enem == 1) {
				x = Random.Range (2, 3);
				posTaken2Enem = x;
			}
			if (posTaken1Enem == 3) {
				x = Random.Range (1, 2);
				posTaken2Enem = x;
			}
			if (posTaken1Enem == 2){
				x = Random.Range(1,2);
				if(x == 1){
					x = 1;
					posTaken2Enem = x;
				}else{
					x = 3;
					posTaken2Enem = x;
				}
			}
			remainingPosEnem--;
			break;
		case 3:
			x = Random.Range (1, 3);
			posTaken1Enem = x;
			remainingPosEnem--;
			break;
		default:
			remainingPosEnem = 3;
			posTaken1Enem = 0;
			posTaken2Enem = 0;
			return RandomIntEnem();
			break;
		}
		return x;
	}

	public int RandomInt(){
		int x = 0;

		switch (remainingPos) {
		case 1:
			if (posTaken1 == 1 && posTaken2 == 2) {
				x = 3;
			}
			if (posTaken1 == 2 && posTaken2 == 1) {
				x = 3;
			}
			if (posTaken1 == 1 && posTaken2 == 3) {
				x = 2;
			}
			if (posTaken1 == 3 && posTaken2 == 1) {
				x = 2;
			}
			if (posTaken1 == 2 && posTaken2 == 3) {
				x = 1;
			}
			if (posTaken1 == 3 && posTaken2 == 2) {
				x = 1;
			}
			Debug.Log ("LastPost: " + x);
			remainingPos--;
			break;
		case 2: 
			if (posTaken1 == 1) {
				x = Random.Range (2, 3);
				posTaken2 = x;
			}
			if (posTaken1 == 3) {
				x = Random.Range (1, 2);
				posTaken2 = x;
			}
			if (posTaken1 == 2){
				x = Random.Range(1,2);
				if(x == 1){
					x = 1;
					posTaken2 = x;
				}else{
					x = 3;
					posTaken2 = x;
				}
			}
			remainingPos--;
			break;
		case 3:
			x = Random.Range (1, 3);
			posTaken1 = x;
			remainingPos--;
			break;
		default:
			remainingPos = 3;
			posTaken1 = 0;
			posTaken2 = 0;
			return RandomInt();
			break;
		}
		return x;
	}
}
