using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour {


	public float timer;
	public int randomMoment;
	public Transform Troop;
	public bool Spawning;
	public GameObject[] EnemiesOnScene;
	// Use this for initialization
	void Start () {
		randomMoment = Random.Range (5, 15);
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;

		if (timer >= randomMoment && Spawning == true) {
			timer = 0;
			randomMoment = Random.Range (10, 30);
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
