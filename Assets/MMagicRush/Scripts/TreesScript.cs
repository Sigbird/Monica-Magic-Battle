using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesScript : MonoBehaviour {

	public SpriteRenderer treeLeaves;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TroopNearby ()) {
			treeLeaves.color = new Color (1, 1, 1, 0.5f);
		} else {
			treeLeaves.color = new Color (1, 1, 1, 1f);
		}
	}

	public bool TroopNearby(){
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
			if (Vector2.Distance (transform.position, obj.transform.position) < 1) {
				return true;
			}
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
			if (Vector2.Distance (transform.position, obj.transform.position) < 0.5) {
				return true;
			}
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemygem")) {
			if (Vector2.Distance (transform.position, obj.transform.position) < 0.5) {
				return true;
			}
		}
		return false;
	}

}
