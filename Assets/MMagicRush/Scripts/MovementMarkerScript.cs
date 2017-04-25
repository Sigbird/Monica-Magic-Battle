using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMarkerScript : MonoBehaviour {

	public int progress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SwitchWaypoints(){
		if (this.gameObject.name == "Waypoint2")
			this.gameObject.name = "Waypoint1";

		if (this.gameObject.name == "Waypoint3") 
			this.gameObject.name = "Waypoint2";
	}
}
