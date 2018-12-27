using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRespawn : MonoBehaviour {

	public Transform[] spawnPoints;
	public Transform[] spawnPointsEnemy;
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
	public bool NewMechanic;

	// Use this for initialization
	void Start () {
//		gemvalue = 0;
//		enemgemvalue = 0;
//
//		lastGem = 10;
//		lastEnemyGem = 10;
//
//		gemsPlaced = 0;
//		randomCD = Random.Range (3, 8);
//
		foreach (Transform t in ChooseSet(3)) {
			t.GetComponent<GemScript> ().BeginInterface ();
		}

		foreach (Transform t in ChooseSetEnemy(3)) {
			t.GetComponent<GemScript> ().BeginInterface ();
		}
	}
	
	// Update is called once per frame
	void Update () {

//		gemCD += Time.deltaTime;



//		if (gemsPlaced < 3) {
//			InstatiateGem ();
//			gemsPlaced++;
//		}
//
//		if (enemygemsPlaced < 3) {
//			InstatiateEnemyGem ();
//			enemygemsPlaced++;
//		}
			

	}
		
		
	public void ResetHeroGems(){
		foreach (Transform t in spawnPoints) {
			t.GetComponent<GemScript> ().GemReset ();
		}
		Shuffle (spawnPoints);
		foreach (Transform t in ChooseSet(3)) {
			t.GetComponent<GemScript> ().BeginInterface ();
		}
	}

	public void ResetEnemyGems(){
		foreach (Transform t in spawnPointsEnemy) {
			t.GetComponent<GemScript> ().GemReset ();
		}
		Shuffle (spawnPointsEnemy);
		foreach (Transform t in ChooseSetEnemy(3)) {
			t.GetComponent<GemScript> ().BeginInterface ();
		}
	}


	public Transform[] ChooseSet (int numRequired) {
		Transform[] result = new Transform[numRequired];

		int numToChoose = numRequired;

		for (int numLeft = spawnPoints.Length; numLeft > 0; numLeft--) {

			float prob = numToChoose/numLeft;

			if (Random.value <= prob) {
				numToChoose--;
				result[numToChoose] = spawnPoints[numLeft - 1];

				if (numToChoose == 0) {
					break;
				}
			}
		}

		return result;
	}

	public Transform[] ChooseSetEnemy (int numRequired) {
		Transform[] result = new Transform[numRequired];

		int numToChoose = numRequired;

		for (int numLeft = spawnPointsEnemy.Length; numLeft > 0; numLeft--) {

			float prob = numToChoose/numLeft;

			if (Random.value <= prob) {
				numToChoose--;
				result[numToChoose] = spawnPointsEnemy[numLeft - 1];

				if (numToChoose == 0) {
					break;
				}
			}
		}
		return result;
	}

	void Shuffle (Transform[] deck) {
		for (int i = 0; i < deck.Length; i++) {
			Transform temp = deck[i];
			int randomIndex = Random.Range(0, deck.Length);
			deck[i] = deck[randomIndex];
			deck[randomIndex] = temp;
		}
	}

}
