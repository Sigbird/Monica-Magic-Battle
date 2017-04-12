using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsRespawn : MonoBehaviour {

	public GameObject GemPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GemCounter () < 1) {
			GemPrefab.GetComponent<GemScript> ().team = 1;
			GemPrefab.tag = "gem";
			Instantiate (GemPrefab, new Vector2 (Random.Range(-2.5f,2.5f), Random.Range(0f,-3.5f)), Quaternion.identity);
		}

		if (EnemyGemCounter () < 1) {
			GemPrefab.GetComponent<GemScript> ().team = 2;
			GemPrefab.tag = "enemygem";
			Instantiate (GemPrefab, new Vector2 (Random.Range(-2.5f,2.5f), Random.Range(0f,3.5f)), Quaternion.identity);
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

}
