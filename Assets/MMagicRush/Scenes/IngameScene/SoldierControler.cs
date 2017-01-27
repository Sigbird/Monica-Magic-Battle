using UnityEngine;
using System.Collections;

public class SoldierControler : MonoBehaviour {

	//public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	private enum STATE
	{
		MOVE,
		IDLE,
		DEFAULT,
		ATACKING,
		RETREAT,
		SEEKING,
	}

	//public TipoSoldado Tipo;

	private STATE state = STATE.DEFAULT;

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

	//FLAGS

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	//STATUS

	public int xp;

	public int level;

	public int vidaMax;

	public int vida;

	public int damage;

	public float damageSpeed;

	public float range;

	private float danoCD = 0;

	public float speed;

	public int energyMax;

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

		skill1.Enable ();

		//this.vida = 10;
		//this.range = 1;
		//this.dano = 1;
		this.speed = speed / 15;
		//this.damageSpeed = damageSpeed;
		this.healtbarSoldier.SetActive (true);
		this.GetComponent<SpriteRenderer> ().sprite = warrior;
		this.state = STATE.SEEKING;
		StartCoroutine (HealingAndXp ());

		//CONFIGURAÇÃO DE TIPO
//		switch (Tipo) {
//		case(TipoSoldado.General):
//			this.vidaMax = 10;
//			this.range = 4;
//			this.dano = 3;
//			this.speed = 0;
//			platform.SetActive (false);
//			this.healtbarGeneral.SetActive (true);
//			this.GetComponent<SpriteRenderer> ().sprite = general;
//			break;
//		case(TipoSoldado.Guerreiro):
//			this.vidaMax = 10;
//			this.range = 1;
//			this.dano = 2;
//			this.speed = 2;
//			this.healtbarSoldier.SetActive (true);
//			this.GetComponent<SpriteRenderer> ().sprite = warrior;
//			this.state = STATE.SEEKING;
//			break;
//		case(TipoSoldado.Lanceiro):
//			this.vidaMax = 4;
//			this.range = 4;
//			this.dano = 4;
//			this.speed = 2.5f;
//			this.healtbarLanceiro.SetActive (true);
//			this.GetComponent<SpriteRenderer> ().sprite = archer;
//			break;
//		case(TipoSoldado.Mago):
//			this.vidaMax = 4;
//			this.range = 3;
//			this.dano = 1;
//			this.speed = 2;
//			this.healtbarMago.SetActive (true);
//			this.GetComponent<SpriteRenderer> ().sprite = mage;
//			break;
//		default:
//			break;
//
//		}



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
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
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
					SpendingEnergy ();
					if (resting == false) {
						transform.position = Vector3.MoveTowards (transform.position, heroBase.transform.position, Time.deltaTime * speed);
					}
				} else {
				this.vida += 2;
					UpdateLife ();
					this.state = STATE.SEEKING;
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
						SpendingEnergy();
						if (resting == false) {
							transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
						}
					} else { //ATACA ALVO
						if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
							if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO HEROI
								targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
						if (this.team == 1) {
							Debug.Log ("atacou");
						}
								UpdateLife ();
							} else {//ALVO BASE
								targetEnemy.GetComponent<ChargesScript> ().charges--;
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
					if (targetEnemy.GetComponent<SoldierControler> ().vida <= 0 || targetEnemy == null) {
						inCombat = false;
					}
	
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
		}

		if (this.energy <= 0) {
			resting = true;
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
				this.xp += 3;
				yield return new WaitForSeconds (1f);
				StartCoroutine (HealingAndXp ());

			} 
		} else {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (HealingAndXp ());
		}

	}

	IEnumerator Respawning(){
		transform.position = heroBase.transform.position;
		this.vida = 10;
		this.xp = 0;
		this.state = STATE.IDLE;
		yield return new WaitForSeconds (4f);
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
