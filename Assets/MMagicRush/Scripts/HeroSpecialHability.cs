using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpecialHability : MonoBehaviour {
	public GameObject sansãoProjectile;
	public PassiveUI uiTrigger;
	public int team;
	public int effectDuration;
	public int effect;
	public float effectCD;
	public GameObject[] SpecialHabilityZone;
	public GameObject MeiaCascao;

	// Use this for initialization
	void Start () {

		if (team == 0) {
			effect = PlayerPrefs.GetInt ("SelectedCharacter");
			//effect = this.gameObject.GetComponent<WPSoldierControler> ().heroID;
		} else {
			effect = this.gameObject.GetComponent<WPIASoldierControler> ().heroID;
		}

		uiTrigger.hero = effect;

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
			effectCD = 6;//10
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
		uiTrigger.StartCooldown = true;
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
			yield return new WaitForSeconds (effectDuration);
				break;
		case 1://CEBOLINHA INVISIVEl
			GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f,.5f) ;
			transform.FindChild ("Platform").gameObject.SetActive (false);
			transform.FindChild ("HealtBarSoldier").gameObject.SetActive (false);
			yield return new WaitForSeconds (effectDuration);
			GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f,1f) ;
			transform.FindChild ("Platform").gameObject.SetActive (true);
			transform.FindChild ("HealtBarSoldier").gameObject.SetActive (true);
				break;
		case 2://AREA DE CURA MAGALI
			SpecialHabilityZone [effect].SetActive (true);
			if (this.gameObject.GetComponent<WPSoldierControler> ().vida < this.gameObject.GetComponent<WPSoldierControler> ().vidaMax) {
				this.gameObject.GetComponent<WPSoldierControler> ().vida++;
				this.gameObject.GetComponent<WPSoldierControler> ().UpdateLife ();
			}
				yield return new WaitForSeconds (effectDuration);
				SpecialHabilityZone [effect].SetActive (false);
				break;
			case 3://AREA DE FEDOR CASCAO
			Instantiate(MeiaCascao,this.transform.position,Quaternion.identity);
				//SpecialHabilityZone [effect].SetActive (true);
				yield return new WaitForSeconds (effectDuration);
				//SpecialHabilityZone [effect].SetActive (false);
				break;
			case 4://AREA DE DANO CHICO
				SpecialHabilityZone [effect].SetActive (true);
				break;
			default:
				break;
			}

		SpecialHabilityZone [effect].SetActive (false);
		StartCoroutine (ApplyEffect ());
	}
}
