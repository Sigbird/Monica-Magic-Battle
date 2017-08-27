using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTestController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Debug.Log(GridInputSnap.GetGridInput());
        }
	}
}
