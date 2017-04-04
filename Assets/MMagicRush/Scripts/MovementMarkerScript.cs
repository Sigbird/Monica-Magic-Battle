using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMarkerScript : MonoBehaviour {
	
	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Hero").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (this.transform.position, player.position) < 0.5f) {
			Destroy (this.gameObject);
		}
	}
}
