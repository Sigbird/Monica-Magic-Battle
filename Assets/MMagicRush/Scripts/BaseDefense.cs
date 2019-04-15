using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YupiPlay.MMB.Lockstep;
using System;

public class BaseDefense : MonoBehaviour {

	public int team;

	public float reach;
	public float shootCD;
	public float damageSpeed;
	public float danoCD;
	public int damage;
	public float heroHealingCD;

	public bool haveAmmo;
	public float reloading;

	public AudioManager audioManager;
	public bool seeking;
	public float cdSeek;
	public bool lockedTarget;
	public GameObject targetEnemy;
	public GameObject arrowModel;
	public bool tower;

	public GameObject HitAnimationObject;
	// Use this for initialization
	void Start () {
		if (tower) {
			this.damage = 17;
			this.reach = 3;
		} else {
			this.damage = 22;
			this.reach = 2.5f;
		}
		this.damageSpeed = 3;
		this.haveAmmo = true;
		this.seeking = true;
		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (haveAmmo == false) {
			reloading += Time.deltaTime;
			if (reloading >= 3) {
				haveAmmo = true;
			}
		}
		
		heroHealingCD += Time.deltaTime;

//		if (heroHealingCD >= 10 && HeroNearby()) {
//			heroHealingCD = 0;
//			if (team == 1) {
//				GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().vida += 1;
//			} else {
//				GameObject.Find ("HeroEnemy").GetComponent<WPIASoldierControler> ().vida += 1;
//			}
//		}

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

		if (seeking == false && this.targetEnemy != null) { 

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > reach) { //FORA DO ALCANCE
				seeking = true;
			} else if (targetEnemy != null) { //ATACA ALVO

				if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
					//anim.SetTrigger ("Attack");
					if(targetEnemy.GetComponent<EnemyRemoteController> () != null){
						if (targetEnemy.GetComponent<EnemyRemoteController> ().alive == false) {
							this.targetEnemy = null;
							lockedTarget = false;
						} else {
							if (NetGameController.Instance.HasGameStarted()) {
								CommandController.AttackEnemyHero ();
								NetClock.Instance.RegisterInputTime();
							} 
							if (this.reach > 1)
								TrowArrow ();
						}
					}else if (targetEnemy.GetComponent<WPIASoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO HEROI
						//targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
						//targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
						//targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
						//if (this.reach > 1)

							TrowArrow ();
						if (targetEnemy.GetComponent<WPIASoldierControler> ().alive == false) { // ALVO MORREU
							this.targetEnemy = null;
							lockedTarget = false;
						}
					} else if (targetEnemy.GetComponent<SoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO TROPA
						//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
						//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
						//targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
						//if (this.reach > 1)
							TrowArrow ();
						if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
							this.targetEnemy = null;
							lockedTarget = false;
						}
					} else if (targetEnemy.GetComponent<WPSoldierControler> () != null && targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {//ALVO HEROI
						//targetEnemy.GetComponent<WPSoldierControler> ().vida -= damage;
						//targetEnemy.GetComponent<WPSoldierControler> ().UpdateLife ();

						//if (this.reach > 1)
						Debug.Log("CAUSOU DANO!");
							TrowArrow ();
						if (targetEnemy.GetComponent<WPSoldierControler> ().alive == false) // ALVO MORREU
							this.targetEnemy = null;
					}
					danoCD = 0;
					if (targetEnemy != null && targetEnemy.GetComponent<SpriteRenderer> () != null) {
						if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == true) {
							//if (this.reach > 1) {
								audioManager.PlayAudio ("shot");
//							} else {
//								audioManager.PlayAudio ("atack");
//							}
						}
					}
				} else {
					//danoCD += Time.deltaTime * 2;
				}
			} 
		} else {
			seeking = true;
		}
		danoCD += Time.deltaTime;

	}

	public bool HeroNearby(){
		if (haveAmmo == true) {
			if (team == 1) {
				if (GameObject.Find ("Hero") != null) {
					if (Vector2.Distance (GameObject.Find ("Hero").transform.position, transform.position) <= reach) {
						return true;
					} else {
						return false;
					}
				}
				return false;
			} else {
				if (GameObject.Find ("HeroEnemy") != null) {
					if (Vector2.Distance (GameObject.Find ("HeroEnemy").transform.position, transform.position) <= reach) {
						return true;
					} else {
						return false;
					}
				}
				return false;
			}
		} else {
			return false;
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
								} else if (obj.GetComponent<EnemyRemoteController> () != null) {
									Emin = obj;
									minDis = dist;
								}
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
								if (obj.GetComponent<WPSoldierControler> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								} else if (obj.GetComponent<EnemyRemoteController> () != null) {
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
		StartCoroutine (DelayedHitEffect ());
		arrowModel.GetComponent<ArrowScript> ().target = targetEnemy;
		arrowModel.GetComponent<ArrowScript> ().type = 0;
		//GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (targetEnemy != null) {
			Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		} 

	}

	IEnumerator DelayedHitEffect(){
		yield return new WaitForSeconds (reach/4.5f);
//		if (targetEnemy != null) {
//			Instantiate (HitAnimationObject, targetEnemy.transform.position, Quaternion.identity);
//			targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage);
//		}

		if (targetEnemy != null) {

				Instantiate (HitAnimationObject, targetEnemy.transform.position, Quaternion.identity);	

			if (targetEnemy.GetComponent<SoldierControler> () != null) {
				targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
			}

			if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {
				targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
			}

			if (targetEnemy.GetComponent<WPSoldierControler> () != null) {
				targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage);
			}

			if (targetEnemy.GetComponent<ChargesScript> () != null) {
				targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (damage);
			}

			if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {
				targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (damage);
			}



		}
	}

}
