using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiaCascao : MonoBehaviour {
	public int team;
	public GameObject target;
	public float damageCD;
	// Use this for initialization
	void Start () {
		if (team == 0) {
			target = GameObject.Find ("HeroEnemy");
		} else {
			target = GameObject.Find ("Hero");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		damageCD += Time.deltaTime;

		if (team == 0) {
			if (target.GetComponent<WPIASoldierControler> ().anim.GetComponent<SpriteRenderer> ().enabled == true) {
				if(Vector3.Distance(this.transform.position,target.transform.position)<2 && damageCD > 2){
					target.GetComponent<WPIASoldierControler> ().ReceiveDamage (1);
					damageCD = 0;
				}
			}	
		} else {
			if (target.GetComponent<WPSoldierControler> ().anim.GetComponent<SpriteRenderer> ().enabled == true) {
				if(Vector3.Distance(this.transform.position,target.transform.position)<2 && damageCD > 2){
					target.GetComponent<WPSoldierControler> ().ReceiveDamage (1);
					damageCD = 0;
				}
			}
		}
	}
}
