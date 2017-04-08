using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScript : MonoBehaviour {

	public GameObject WaypointMarker;
	public int progress;

	// Use this for initialization
	void Start () {
		progress = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (progress >= 4) {
			progress = 1;
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
}
