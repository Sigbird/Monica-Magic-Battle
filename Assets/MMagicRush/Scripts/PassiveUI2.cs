using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUI2 : MonoBehaviour {

	public float counter;
	public HealtBarSolid Bar;
	public bool StartCooldown;

	// Use this for initialization
	void Start () {
		Bar = GetComponent<HealtBarSolid> ();
		counter = 0;
		Bar.maxValue = 7;
	}

	// Update is called once per frame
	void Update () {
		if (StartCooldown) {
			counter += Time.deltaTime;
		} else {
			counter = 0;
		}

		PassiveCooldown ();
		Bar.atualValue = counter;
	}

	public void PassiveCooldown(){


		if (counter >7) { // reset


			StartCooldown = false;
		}


	}
}
