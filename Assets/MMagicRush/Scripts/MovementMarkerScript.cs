using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMarkerScript : MonoBehaviour {

	public int progress;
	public bool capture;
	public bool targetMarker;
	public bool herobase;
	public GameObject Hero;
	private float minDistance;


	public bool reached;
	// Use this for initialization
	void Awake(){
		Hero = GameObject.Find ("GameController").GetComponent<GameController> ().HeroGameObject;
		reached = false;
		minDistance = Hero.GetComponent<WPSoldierControler> ().range -2;
	}

	void Start () {
		

	}

	void OnDestroy(){
		
	}

	void OnMouseDown(){
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (Hero != null) {
			if (this.targetMarker == true) {
				if(Vector2.Distance (Hero.transform.position, this.transform.position) < minDistance){
					//Destroy (this.gameObject);
				}
			} else {
				if (Vector2.Distance (Hero.transform.position, this.transform.position) <= 0.5) {
					Destroy (this.gameObject);
				}
			}
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
