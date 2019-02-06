using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour {


	public float timer;
	public int randomMoment;
	public Transform Troop;
	public bool Spawning;
	public GameObject[] EnemiesOnScene;
	private int RandomMin;
	private int RandomMax;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("Dificulty")) {
			var x = PlayerPrefs.GetInt ("Dificulty");
			if (x == 1) {
				RandomMin = 30;
				RandomMax = 40;
			}
			if (x == 2) {
				RandomMin = 22;
				RandomMax = 32;
			}
			if (x == 3) {
				RandomMin = 15;
				RandomMax = 25;
			}

		} else {
			RandomMin = 22;
			RandomMax = 32;
		}

		randomMoment = Random.Range (RandomMin, RandomMax);
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;

		if (timer >= randomMoment && Spawning == true) {
			timer = 0;
			randomMoment = Random.Range (RandomMin, RandomMax);
			InstatiateEnemyTroop(Random.Range(1,6));
		}

		EnemiesOnScene = GameObject.FindGameObjectsWithTag("enemysoldier2");
		if (EnemiesOnScene.Length > 4) {
			Spawning = false;
		} else {
			Spawning = true;
		}
	}

	public void InstatiateEnemyTroop(int troopid){
		Transform t =  Troop;
		t.gameObject.GetComponent<SoldierControler> ().troopId = troopid;
		t.gameObject.GetComponent<SoldierControler> ().summon = false;
		if (Random.value > 0.5f) {
			Instantiate (t, new Vector2 (this.transform.position.x - 0.5f, this.transform.position.y), Quaternion.identity);
		} else {
			Instantiate (t, new Vector2 (this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
		}
	}
}
