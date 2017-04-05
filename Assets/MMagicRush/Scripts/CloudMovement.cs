using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	private float counter;
	public float interval;
	public bool right;

	// Use this for initialization
	void Start () {
		interval = Random.Range (20, 30);
	}
	
	// Update is called once per frame
	void Update () {
		
//		if (counter >= interval) {
//			if (right == true) {
//				right = false;
//			} else {
//				right = true;
//			}
//			counter = 0;
//		}
//
//		if (right) {
//			transform.Translate (Vector2.right * Time.deltaTime * 0.5f);
//		} else {
//			transform.Translate (Vector2.left * Time.deltaTime * 0.5f);
//		}

		transform.Translate (Vector2.right * Time.deltaTime * Random.Range (0.2f, 0.6f));

		if (counter >= interval) {
			transform.position = new Vector2 (-5, Random.Range (2, -1.5f));
			interval = Random.Range (20, 30);
			counter = 0;
		}

		counter += Time.deltaTime;


	}
}
