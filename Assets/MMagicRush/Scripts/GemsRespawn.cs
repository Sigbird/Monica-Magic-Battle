using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRespawn : MonoBehaviour {

	public GameObject GemPrefab;
	public int gemvalue;
	public int enemgemvalue;
	public List<int> usedValues = new List<int>();
	public List<int> enemusedValues = new List<int>();
	public int remainingPos = 3;
	public int posTaken1;
	public int posTaken2;
	public int remainingPosEnem = 3;
	private int posTaken1Enem;
	private int posTaken2Enem;

	public float gemCD;
	private float randomCD;
	public int gemsPlaced;
	public int enemygemsPlaced;
	public int gemPosition;
	public int lastGem;
	public int lastEnemyGem;
	private int gemID;
	private int enemygemID;

	// Use this for initialization
	void Start () {
		gemvalue = 0;
		enemgemvalue = 0;

		lastGem = 10;
		lastEnemyGem = 10;

		gemsPlaced = 0;
		randomCD = Random.Range (3, 8);
		gemPosition = RandomInt ();
	}
	
	// Update is called once per frame
	void Update () {

		gemCD += Time.deltaTime;



		if (gemCD > randomCD && gemsPlaced < 3) {
			InstatiateGem ();
			gemsPlaced++;
		}

		if (gemCD > randomCD && enemygemsPlaced < 3) {
			InstatiateEnemyGem ();
			enemygemsPlaced++;
		}

		if (gemCD > randomCD) {
			gemPosition = RandomInt ();
			gemCD = 0;
			randomCD = Random.Range (3, 8);
		}


		if (gemvalue >= 50) {
			//usedValues.Clear ();
			gemvalue = 0;
		}

		if (enemgemvalue >= 50) {
			//enemusedValues.Clear ();
			enemgemvalue = 0;
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

	public Vector2 GetRandomPosition(int x){


		switch (x) {
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

	public Vector2 GetEnemyRandomPosition(int x){


		switch (x) {
		case 1:
			return new Vector2 (-2.5f, 2.3f);
			break;
		case 2: 
			return new Vector2 (0f, 3.5f);
			break;
		case 3: 
			return new Vector2 (2.5f, 1f);
			break;
		default:
			return new Vector2 (2.5f, 1f);
			break;
		}
	}

	public void InstatiateGem(){
		if (gemID >= 3)
			gemID = 0;
		
		if (gemID != lastGem) {
			gemvalue += 10;
			gemID += 1;
			GemPrefab.GetComponent<GemScript> ().gemvalue = gemvalue;
			GemPrefab.GetComponent<GemScript> ().team = 1;
			GemPrefab.GetComponent<GemScript> ().id = gemID;
			GemPrefab.tag = "gem";
			Instantiate (GemPrefab, GetRandomPosition (gemPosition), Quaternion.identity);
		} else {
			lastGem = 10;
		}

	}

	public void InstatiateEnemyGem(){
		if (enemygemID >= 3)
			enemygemID = 0;
		
		if (enemygemID != lastEnemyGem) {
			enemgemvalue += 10;
			enemygemID += 1;
			GemPrefab.GetComponent<GemScript> ().gemvalue = enemgemvalue;
			GemPrefab.GetComponent<GemScript> ().team = 2;
			GemPrefab.GetComponent<GemScript> ().id = enemygemID;
			GemPrefab.tag = "enemygem";
			Instantiate (GemPrefab, GetEnemyRandomPosition (gemPosition), Quaternion.identity);
		} else {
			lastEnemyGem = 10;
		}
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
//			Debug.Log ("LastPost: " + x);
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
