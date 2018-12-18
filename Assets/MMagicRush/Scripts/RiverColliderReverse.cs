using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverColliderReverse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		col.transform.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 100);
		//col.transform.position = Vector3.MoveTowards (col.transform.position, new Vector2(col.transform.position.x-1,col.transform.position.y), Time.deltaTime * 20);

	}
}
