using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDefense : MonoBehaviour {

	public int team;

	public int reach;
	public float shootCD;
	public float damageSpeed;
	public float danoCD;
	public int damage;

	public AudioManager audioManager;
	public bool seeking;
	public float cdSeek;
	public bool lockedTarget;
	public GameObject targetEnemy;
	public GameObject arrowModel;
	// Use this for initialization
	void Start () {
		this.seeking = true;
		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (seeking && cdSeek >= 0.01f) {
			if (lockedTarget == false) {
				this.targetEnemy = SeekEnemyTarget ();
			}
			if (this.targetEnemy != null) {
				seeking = false;
			}
			cdSeek = 0;
		} else {
			cdSeek += Time.deltaTime;
		}

		if(seeking == false && this.targetEnemy != null ) { 

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > reach) { //FORA DO ALCANCE
				seeking = true;
			} else if(targetEnemy != null) { //ATACA ALVO

				if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
					//anim.SetTrigger ("Attack");
					if (targetEnemy.GetComponent<WPIASoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO HEROI
						targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
						targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
						targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage ();
						if (this.reach > 1)
							TrowArrow ();
						if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
							this.targetEnemy = null;
							lockedTarget = false;
						}
					} else if (targetEnemy.GetComponent<SoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO TROPA
						targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
						targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
						targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage ();
						if (this.reach > 1)
							TrowArrow ();
						if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
							this.targetEnemy = null;
							lockedTarget = false;
						}
					} else if (targetEnemy.GetComponent<WPSoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO HEROI
						targetEnemy.GetComponent<WPSoldierControler> ().vida -= damage;
						targetEnemy.GetComponent<WPSoldierControler> ().UpdateLife ();
						targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage ();
						if (this.reach > 1)
							TrowArrow ();
						if (targetEnemy.GetComponent<WPSoldierControler> ().vida <= -1) // ALVO MORREU
							this.targetEnemy = null;
					}
					danoCD = 0;
					if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {
						if (this.reach > 1) {
							audioManager.PlayAudio ("shot");
						} else {
							audioManager.PlayAudio ("atack");
						}
					}
				} else {
					danoCD += Time.deltaTime * 2;
				}
			} 
		}


	}

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;
		if (this.team == 1) { //Procura de Jogador 1
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null ) {//PROCURA TROPA
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {

					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist <= reach) {
						if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
							if (obj.GetComponent<SoldierControler> () != null) {
								Emin = obj;
								minDis = dist;
							}
						}
					}
				}
			} 
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {//PROCURA HEROI
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					if (obj.GetComponent<SpriteRenderer> ().enabled == true) {
						float dist = Vector3.Distance (transform.position, obj.transform.position);
						if (dist <= reach) {
							if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
								if (obj.GetComponent<WPIASoldierControler> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					}
				}
			} 


		} else if (this.team == 2) { //Procura de Jogador 2
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {//PROCURA TROPA
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist <= reach) {
						if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
							if (obj.GetComponent<SoldierControler> () != null) {
								Emin = obj;
								minDis = dist;
							}
						}
					}
				}
			}
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {//PROCURA HEROI
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					if (obj.GetComponent<SpriteRenderer> ().enabled == true) {
						float dist = Vector3.Distance (transform.position, obj.transform.position);
						if (dist <= reach) {
							if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
								if (obj.GetComponent<WPSoldierControler> ().heroUnity != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					}
				}
			}

		}
		return Emin;
	}

	public void TrowArrow(){
		arrowModel.GetComponent<ArrowScript> ().target = targetEnemy;
		//GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (targetEnemy != null) {
			Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		} 

	}

}
