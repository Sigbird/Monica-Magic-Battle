using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
	public float x;
	// Use this for initialization
	void Start () {
		if (x > 0) {
			Destroy (this.gameObject, x);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyObj(){
		Destroy (this.gameObject);
	}
}
