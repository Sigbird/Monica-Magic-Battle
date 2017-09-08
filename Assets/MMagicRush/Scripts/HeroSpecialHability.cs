using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpecialHability : MonoBehaviour {
	public GameObject sansãoProjectile;
	public int team;
	public int effectDuration;
	public int effect;
	public float effectCD;
	public GameObject[] SpecialHabilityZone;
	// Use this for initialization
	void Start () {

		if (team == 0) {
			effect = this.gameObject.GetComponent<WPSoldierControler> ().heroID;
		} else {
			effect = this.gameObject.GetComponent<WPIASoldierControler> ().heroID;
		}


		switch (effect) {
		case 0:
			effectCD = 6;
			effectDuration = 2;
			break;
		case 1:
			effectCD = 6;
			effectDuration = 3;
			break;
		case 2:
			effectCD = 10;
			effectDuration = 4;
			break;
		case 3:
			effectCD = 6;
			effectDuration = 3;
			break;
		case 4:
			effectCD = 6;
			effectDuration = 3;
			break;
		default:
			
			break;
		}
		StartCoroutine (ApplyEffect ());
	}

	void Update () {
		
		
	}


	IEnumerator ApplyEffect(){
		yield return new WaitForSeconds (effectCD);
			GameObject target;
			switch (effect) {
			case 0://ATIRA O COELHO
				if (team == 0) {
					target = GameObject.Find ("HeroEnemy");
					if (target.GetComponent<SpriteRenderer> ().enabled == true) {
						GameObject arrow = Instantiate (sansãoProjectile, this.transform.position, Quaternion.identity);
						arrow.GetComponent<ArrowScript> ().target = target;
						target.GetComponent<WPIASoldierControler> ().ReceiveDamage (1);
					}	
				} else {
					target = GameObject.Find ("Hero");
					if (target.GetComponent<SpriteRenderer> ().enabled == true) {
						GameObject arrow = Instantiate (sansãoProjectile, this.transform.position, Quaternion.identity);
						arrow.GetComponent<ArrowScript> ().target = target;
						target.GetComponent<WPSoldierControler> ().ReceiveDamage (1);
					}
				}
				break;
		case 1://CEBOLINHA INVISIVEl
			GetComponent<SpriteRenderer> ().enabled = false;
			transform.FindChild ("Platform").gameObject.SetActive (false);
			transform.FindChild ("HealtBarSoldier").gameObject.SetActive (false);
				break;
			case 2://AREA DE CURA MAGALI
				SpecialHabilityZone [effect].SetActive (true);
				break;
			case 3://AREA DE FEDOR CASCAO
				SpecialHabilityZone [effect].SetActive (true);
				break;
			case 4://AREA DE DANO CHICO
				SpecialHabilityZone [effect].SetActive (true);
				break;
			default:
				break;
			}
		yield return new WaitForSeconds (effectDuration);
		GetComponent<SpriteRenderer>().enabled = true;
		transform.FindChild ("Platform").gameObject.SetActive (true);
		SpecialHabilityZone [effect].SetActive (false);
		StartCoroutine (ApplyEffect ());
	}
}
