using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScript : MonoBehaviour {

	public GameObject WaypointMarker;
	public GameObject EnemyWaypointMarker;
	public int progress;
	public int enemyprogress;

	// Use this for initialization
	void Start () {
		progress = 1;
		enemyprogress = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (progress >= 4) {
			progress = 1;
		}

		if (enemyprogress >= 4) {
			enemyprogress = 1;
		}


		if (EnemyMovementCounter () < 3) {
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			Instantiate (EnemyWaypointMarker, new Vector2 (Random.Range(-2.5f,2.5f), Random.Range(0f,4.5f)), Quaternion.identity);
			enemyprogress++;
		}

	}

	void OnMouseDown(){
		if (MovementCounter () < 3) {
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity);
			progress++;
		}
			
	}

	public int MovementCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("herowaypoint");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}

	public int EnemyMovementCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("enemywaypoint");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}
}
