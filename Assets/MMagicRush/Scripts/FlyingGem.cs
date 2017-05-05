using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGem : MonoBehaviour {
	public Transform target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Diamonds").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (this.transform.position, target.position, Time.deltaTime * 7);

		if (Vector2.Distance (this.transform.position, target.position) < 0.5f) {
			Destroy (this.gameObject);
		}

	}
}
