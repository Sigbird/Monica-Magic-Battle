using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMarkerScript : MonoBehaviour {

	public int progress;
	public bool capture;
	public bool targetMarker;
	public bool herobase;

	public bool reached;
	// Use this for initialization
	void Start () {
		reached = false;
	}


	void OnMouseDown(){
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("Hero") != null)
		if (Vector2.Distance (GameObject.Find ("Hero").transform.position, this.transform.position) <= 0.5) {
			reached = true;
		}
	}

//	public void SwitchWaypoints(){
//		if (this.gameObject.name == "Waypoint2")
//			this.gameObject.name = "Waypoint1";
//
//		if (this.gameObject.name == "Waypoint3") 
//			this.gameObject.name = "Waypoint2";
//	}
}
