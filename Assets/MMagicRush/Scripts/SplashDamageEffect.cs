using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageEffect : MonoBehaviour {

	public int player;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 1);
		ExplosionDamage ();
//		if (player == 1) {
//			
//		} else {
//		
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ExplosionDamage()
	{
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 1);
		int i = 0;
		while (i < hitColliders.Length)
		{
			if (player == 1) {
				if (hitColliders [i].gameObject.GetComponent<WPIASoldierControler> () != null) {
					hitColliders [i].gameObject.GetComponent<WPIASoldierControler> ().ReceiveDamage (22);
				}
				if (hitColliders [i].gameObject.GetComponent<SoldierControler> () != null) {
					if (hitColliders [i].gameObject.GetComponent<SoldierControler> ().team != 1) {
						hitColliders [i].gameObject.GetComponent<SoldierControler> ().ReceiveDamage (22);
					}
				}
			} else {
				if (hitColliders [i].gameObject.GetComponent<WPSoldierControler> () != null) {
					hitColliders [i].gameObject.GetComponent<WPSoldierControler> ().ReceiveDamage (22);
				}
				if (hitColliders [i].gameObject.GetComponent<SoldierControler> () != null) {
					if (hitColliders [i].gameObject.GetComponent<SoldierControler> ().team == 1) {
						hitColliders [i].gameObject.GetComponent<SoldierControler> ().ReceiveDamage (22);
					}
				}
			}
				
			i++;
		}
	}

}
