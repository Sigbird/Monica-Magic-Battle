﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierControler : MonoBehaviour {

	//public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	public enum STATE
	{
		MOVE,
		IDLE,
		DEFAULT,
		ATACKING,
		RETREAT,
		SEEKING,
	}

	//public TipoSoldado Tipo;

	public STATE state = STATE.DEFAULT;

	public int team;

	[HideInInspector]
	public Vector3 touchStartPosition;

	[HideInInspector]
	public Vector3 touchEndPosition;

	//PREFABS

	//[HideInInspector]
	public GameObject targetEnemy;

	public GameObject healtbarSoldier;

	public GameObject energybarSoldier;

	public Sprite warrior;

	public Sprite archer;

	public Sprite mage;

	public Sprite general;

	public GameObject light;

	public GameObject platform;

	public GameObject heroBase;

	public Animator anim;

	//FLAGS

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	//STATUS

	public int xp;

	public int level;

	private bool levelUp;

	public int vidaMax;

	public int vida;

	public int damage;

	public float damageSpeed;

	public float range;

	private float danoCD = 0;

	public float speed;

	private float maxSpeed;

	public int energyMax;

	private bool tired;

	public int energy;

	public bool resting;

	private float energyCounter;

	//SKILLS

	public SkillsScript skill1;
	public SkillsScript skill2;

	//PROJECTILES

	public GameObject arrowSlot;

	public GameObject arrowModel;

	//PANELS

	public GameObject victoryScreen;

	public GameObject defeatScreen;

	//Debugging

	public InputField[] heroImputs;

	public InputField[] enemyImputs;

	private float count; 
	[HideInInspector]
	public float desloc;
	public float deslocTimer;

	// Use this for initialization
	void Start () {

		UpdateLife ();
		this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();

		UpdateEnergy ();
		this.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		this.energybarSoldier.GetComponent<HealtBar> ().energy = true;

		anim = GetComponent<Animator> ();
	
		this.speed = speed / 15;
		this.maxSpeed = this.speed;
		this.level = 1;
		this.healtbarSoldier.SetActive (true);
		this.state = STATE.SEEKING;
		StartCoroutine (HealingAndXp ());

		//CONFIGURAÇÃO DE TIPO
		if (this.team == 1) {
			int id =	PlayerPrefs.GetInt ("SelectedCharacter");
			switch (id) {
			case(1):
				this.vidaMax = 3;
				this.vida = 3;
				this.damage = 1;
				this.damageSpeed = 2;
				this.range = 1;
				this.speed = 8;
				this.energyMax = 4;
				this.energy = 4;
				this.GetComponent<SpriteRenderer> ().sprite = warrior;
				Debug.Log ("Monica");
				break;
			case(2):
				this.vidaMax = 3;
				this.vida = 3;
				this.damage = 1;
				this.damageSpeed = 2;
				this.range = 1;
				this.speed = 8;
				this.energyMax = 4;
				this.energy = 4;
				this.GetComponent<SpriteRenderer> ().sprite = warrior;
				Debug.Log ("Cebolinha");
				break;
			case(3):
				this.vidaMax = 3;
				this.vida = 3;
				this.damage = 1;
				this.damageSpeed = 2;
				this.range = 1;
				this.speed = 8;
				this.energyMax = 4;
				this.energy = 4;
				this.GetComponent<SpriteRenderer> ().sprite = warrior;
				Debug.Log ("Magali");
				break;
			case(4):
				this.vidaMax = 3;
				this.vida = 3;
				this.damage = 1;
				this.damageSpeed = 2;
				this.range = 1;
				this.speed = 8;
				this.energyMax = 4;
				this.energy = 4;
				this.GetComponent<SpriteRenderer> ().sprite = warrior;
				break;
			default:
				this.GetComponent<SpriteRenderer> ().sprite = warrior;
				break;

			}

		} else {
			//CARREGAR PERSONAGEM DO ADVERSÁRIO
			this.GetComponent<SpriteRenderer>().flipX = true;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
		}



		// CONFIGURAÇÃO DE EQUIPE
		if (this.team == 1) {
			this.tag = "enemysoldier1";
		} else {
			this.tag = "enemysoldier2";
			platform.GetComponent<SpriteRenderer> ().color = Color.red;

		}
			
		//DESLOCAMENTO INICIAL
		if (Random.value < 0.5f) {
			desloc = 1f;
		} else {
			desloc = -1f;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

//		//Debugging
//		if(this.team == 1){
//			if(heroImputs[0].text !=null)
//				this.vida = System.Int32.Parse(heroImputs [0].text);
//			if(heroImputs[1].text !=null)
//				this.vidaMax = System.Int32.Parse(heroImputs [1].text);
//			if(heroImputs[2].text !=null)
//				this.energy = System.Int32.Parse(heroImputs [2].text);
//			if(heroImputs[3].text !=null)
//				this.energyMax = System.Int32.Parse(heroImputs [3].text);
//			if(heroImputs[4].text !=null)
//				this.damage = System.Int32.Parse(heroImputs [4].text);
//		//	if(heroImputs[5].text !=null)
//			//this.speed = float.Parse(heroImputs [5].text);
//		}
//
//		if(this.team == 2){
//			if(enemyImputs[0].text !=null)
//				this.vida = System.Int32.Parse(enemyImputs [0].text);
//			if(enemyImputs[1].text !=null)
//				this.vidaMax = System.Int32.Parse(enemyImputs [1].text);
//			if(enemyImputs[2].text !=null)
//				this.energy = System.Int32.Parse(enemyImputs [2].text);
//			if(enemyImputs[3].text !=null)
//				this.energyMax = System.Int32.Parse(enemyImputs [3].text);
//			if(enemyImputs[4].text !=null)
//				this.damage = System.Int32.Parse(enemyImputs [4].text);
//		//	if(enemyImputs[5].text !=null)
//		//	this.speed = float.Parse(enemyImputs [5].text);
//		}


		//
		//ORDEM DE LAYER
		//
		this.GetComponent<SpriteRenderer> ().sortingOrder = -(int)(this.transform.position.y - 0.5f);


		//DESLOCAMENTO INICIAL
//		if (deslocTimer < Random.Range(30,50)) {
//			transform.Translate (Vector2.left * Time.deltaTime * desloc);
//			deslocTimer++;
//		}

		//Evento de Respawning
		if (this.vida <= 0) {
			StartCoroutine (Respawning ());
		}

		//	
		//FLAGS PARA HEALING
		//
		if (this.team == 1) {
			if (this.transform.position.y < -1) {
				this.healing = true;
			} else {
				this.healing = false;
			}

			if (this.transform.position.y > 1) {
				this.GainXP = true;
			} else {
				this.GainXP = false;
			}
		} 

		if (this.team == 2) {
			if (this.transform.position.y > 1) {
				this.healing = true;
			} else {
				this.healing = false;
			}

			if (this.transform.position.y < -1) {
				this.GainXP = true;
			} else {
				this.GainXP = false;
			}
		}

		//
		//FLAGS PARA HABILIDADES
		//
		if(skill1.skillActivated){
			switch (skill1.skillID) {
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				break;
			}
			skill1.skillActivated = false;
		}
		if (skill2.skillActivated) {
			switch (skill2.skillID) {
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				break;
			}
			skill2.skillActivated = false;
		}

		//
		//FLAGS PARA LEVEL
		//
		if(this.xp < 25 && this.xp < 125){
			this.level = 1;
			if (skill1.skillLevel <= this.level) {
				skill1.Enable ();
			}
			if (skill2.skillLevel <= this.level) {
				skill2.Enable ();
			}
		}
		if(this.xp > 25 && this.xp < 125){
			this.level = 2;
			if (skill1.skillLevel <= this.level) {
				skill1.Enable ();
			}
			if (skill2.skillLevel <= this.level) {
				skill2.Enable ();
			}
		}
		if (this.xp >= 125) {
			this.level = 3;
			if (skill1.skillLevel <= this.level) {
				skill1.Enable ();
			}
			if (skill2.skillLevel <= this.level) {
				skill2.Enable ();
			}
		}

			
				
			//
			//ESTADOS DO PERSONAGEM
			//
			if (this.state == STATE.MOVE) {
				if (Vector3.Distance (transform.position, touchEndPosition) > 1) {
					transform.position = Vector3.MoveTowards (transform.position, touchEndPosition, Time.deltaTime * speed);
				} else {
					this.state = STATE.DEFAULT;
				}

			}

			if (this.state == STATE.IDLE) {
				transform.position = Vector3.MoveTowards (transform.position, transform.position, Time.deltaTime * speed);
			}

			if (this.state == STATE.RETREAT) {
				//this.speed = 1.5f;
				if (Vector3.Distance (transform.position, heroBase.transform.position) > range - 0.5f) {
					GetComponent<SpriteRenderer> ().flipX = true;
					anim.SetTrigger ("Walk");
					SpendingEnergy ();
					transform.position = Vector3.MoveTowards (transform.position, heroBase.transform.position, Time.deltaTime * speed);

				} else {
				if (this.vida <= this.vidaMax - 2) {
					this.vida += 2;
					UpdateLife ();
				}
					this.state = STATE.SEEKING;
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else {
				//this.speed = 1;
			}
					
			if (this.state == STATE.SEEKING) {
				targetEnemy = SeekEnemyTarget ();
				if (targetEnemy != null) {
					this.state = STATE.DEFAULT;
				}
			}

			if (this.state == STATE.DEFAULT) {
				
					//DESLOCAMENTO ATE INIMIGO
					if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
						anim.SetTrigger("Walk");
						SpendingEnergy();
						transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
						
					} else { //ATACA ALVO
						anim.SetTrigger("Attack");
						if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
							if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO HEROI
								targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
								UpdateLife ();
							} else {//ALVO BASE
								targetEnemy.GetComponent<ChargesScript> ().charges++;
								StartCoroutine (Respawning ());
							}
							danoCD = 0;
						} else {
							danoCD += Time.deltaTime;
						}
					} 

					//ANIMACAODE TIRO DE PROJETEIS
//					if (inCombat == true && arrowSlot != null && this.Tipo == TipoSoldado.Lanceiro && targetEnemy != null) {
//						if (Vector3.Distance (arrowSlot.transform.position, targetEnemy.transform.position) > 0.1f && targetEnemy != null) {
//							arrowSlot.transform.position = Vector3.MoveTowards (arrowSlot.transform.position, targetEnemy.transform.position, Time.deltaTime * 5);
//							Vector3 moveDirection = arrowSlot.transform.position - targetEnemy.transform.position; 
//							if (moveDirection != Vector3.zero) {
//								float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
//								arrowSlot.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
//							}
//						} else {
//							//arrowSlot.GetComponent<ArrowScript> ().DestroyArrow ();
//							//arrowSlot = null;
//						}
//					}

					//FIM DO COMBATE
//					if (targetEnemy.GetComponent<SoldierControler> ().vida <= 0 || targetEnemy.GetComponent<SoldierControler>() == null) {
//						inCombat = false;
//					}
//	
			}
			
	}


	public void ChangeState(){
	
		if (this.state == STATE.RETREAT) {
			this.state = STATE.SEEKING;
		}

		if (this.state == STATE.DEFAULT) {
			this.targetEnemy = null;
			this.state = STATE.RETREAT;
		}

	}

	public void UpdateLife(){
		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarSoldier.GetComponent<HealtBar> ().MaxLife = this.vidaMax;
		this.healtbarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
	}

	public void UpdateEnergy(){
		this.energybarSoldier.GetComponent<HealtBar> ().Life = this.energy;
		this.energybarSoldier.GetComponent<HealtBar> ().MaxLife = this.energyMax;
		this.energybarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
	}

	public void SpendingEnergy(){
		energyCounter += Time.deltaTime;
		if (energyCounter > 2 && resting == false) {
			this.energy--;
			UpdateEnergy ();
			energyCounter = 0;
		}

		if (energyCounter > 2 && resting == true) {
			this.energy++;
			UpdateEnergy ();
			energyCounter = 0;
		}

		if (this.energy >= this.energyMax) {
			resting = false;
			this.speed = maxSpeed;
			tired = false;
		}

		if (this.energy <= 0) {
			resting = true;

			if (tired == false) {
				tired = true;
				this.speed = speed / 2;
			}
		}

	}

	IEnumerator HealingAndXp(){
		if (healing || GainXP) {
			if (healing) {
				yield return new WaitForSeconds (3f);
				if (vida < vidaMax) {
					this.vida += 1;
					UpdateLife ();
				}
				StartCoroutine (HealingAndXp ());
			}
			if (GainXP) {
				yield return new WaitForSeconds (1f);
				this.xp += 5;
				StartCoroutine (HealingAndXp ());

			} 
		} else {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (HealingAndXp ());
		}

	}

	IEnumerator Respawning(){
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.platform.GetComponent<SpriteRenderer> ().enabled = false;
		this.healtbarSoldier.SetActive (false);
		this.energybarSoldier.SetActive (false);
		transform.position = heroBase.transform.position;
		this.xp = 0;
		this.vida = this.vidaMax;
		UpdateLife ();
		this.energy = this.energyMax;
		this.state = STATE.IDLE;
		this.skill1.Disable ();
		this.skill2.Disable ();
		yield return new WaitForSeconds (4f);
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		this.platform.GetComponent<SpriteRenderer> ().enabled = true;
		this.healtbarSoldier.SetActive (true);
		this.energybarSoldier.SetActive (true);
		this.state = STATE.RETREAT;
	}

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;
		if (this.tag == "enemysoldier1") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist < minDis) {
						Emin = obj;
						minDis = dist;
					}
				}
			} else {
				SeekEnemyTarget ();
			}
		} else if (this.tag == "enemysoldier2") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist < minDis) {
						Emin = obj;
						minDis = dist;
					}
				}
			}
		}
		return Emin;
	}
		

//	public void TrowArrow(){
//		GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
//		if (Vector3.Distance (arrow.transform.position, targetEnemy.transform.position) > 0.1f) {
//			arrow.transform.position = Vector3.MoveTowards (arrow.transform.position, targetEnemy.transform.position, Time.deltaTime * 5);
//		} else {
//			Destroy (arrow);
//		}
//	}


}
