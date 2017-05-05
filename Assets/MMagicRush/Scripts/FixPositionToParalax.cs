using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionToParalax : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		Vector2 reference = Camera.main.transform.position;

			this.transform.position = new Vector2 (reference.x + 0.75f, reference.x - 3.9f);


//		if (this.transform.position.x >= 0 && this.transform.position.y >= 0) {
//			this.transform.position = new Vector2 (reference.x + transform.position.x, reference.x + transform.position.x);
//		}
//
//		if (this.transform.position.x < 0 && this.transform.position.y < 0) {
//			this.transform.position = new Vector2 (reference.x - transform.position.x, reference.x - transform.position.x);
//		}
//
//		if (this.transform.position.x >= 0 && this.transform.position.y < 0) {
//			this.transform.position = new Vector2 (reference.x + transform.position.x, reference.x - transform.position.x);
//		}
//
//		if (this.transform.position.x < 0 && this.transform.position.y >= 0) {
//			this.transform.position = new Vector2 (reference.x - transform.position.x, reference.x + transform.position.x);
//		}
	}
}
