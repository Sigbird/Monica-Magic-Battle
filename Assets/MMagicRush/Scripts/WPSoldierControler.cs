using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YupiPlay.MMB.Lockstep;
using System;

public class WPSoldierControler : MonoBehaviour {

	//public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	//	public enum STATE
	//	{
	//		DEFAULT,
	//		RETREAT,
	//	}

	//public TipoSoldado Tipo;

	public bool tutorial;
	public bool multiplayer;
	public bool heroUnity;

	public int troopId;
	public int heroID;

	public float topPreference;
	public float midPreference;
	public float botPreference;

	//	public STATE state = STATE.DEFAULT;

	public int team;

	[HideInInspector]
	public Vector3 touchStartPosition;

	[HideInInspector]
	public Vector3 touchEndPosition;

	//PREFABS

	//[HideInInspector]
	public SpriteRenderer CharBound;

	public GameObject targetEnemy;

	public GameObject healtbarSoldier;

	//public GameObject energybarSoldier;

	public Sprite warrior;

	public Sprite[] tropasSprites;

	public Sprite[] heroPortraits;

	public GameObject FlashingEffects;

	public GameObject platform;

	public GameObject heroBase;

	public Animator anim;

	public AudioManager audioManager;

	public GameObject deathAngel;

	public Animator XpBar;

	public Animator LvlUpAnim;

	public GameObject HitAnimationObject;

	public Animation Arrival;

	//FLAGS

	public bool alive;

	private bool gameEnded = false;

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	public bool seeking;

	public bool lockedTarget;

	public float cdSeek;

	public float respawningTimer;

	//STATUS

	public int enemyGems;

	public float xp;

	public float xpLvl1 = 0;
	public float xplvl2 = 25;
	public float xplvl3 = 125;

	private float xpMax;

	public int level;

	private bool levelUp;

	[HideInInspector]
	public int vidaMax;


	public int vida;

	public float reach;

	public int damage;

	public float damageSpeed;

	public float danoCD = 0;

	[HideInInspector]
	public float range;


	public float speed;


	private float maxSpeed;

	[HideInInspector]
	public int energyMax;

	private bool tired;

	[HideInInspector]
	public int energy;

	public bool resting;

	private float energyCounter;

	public string effects;
	public float effectsDuration;

	private Vector3 previous;
	private float velocity;

	//LANE WAYPOINTS

	public GameObject WaypointMark;
	private float captureMarkerTimer;


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

	private bool gameBegin;


	// Use this for initialization
	void Start () {
		gameBegin = false;


		if (heroUnity) {// CONFIGURAÇÃO DE HEROIS
			SetupHero();
		} 

		//CONFIGURAÇÃO EM COMUM 
		UpdateLife ();
		this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();

//		UpdateEnergy ();
//		this.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
//		this.energybarSoldier.GetComponent<HealtBar> ().energy = true;

		if(heroUnity)
		if (team == 1) {
			this.xp = GameController.playerXp;
		} else {
			this.xp = GameController.enemyXp;
		}

//		if (GetComponent<Animator> () != null) {
//			anim = GetComponent<Animator> ();
//		}
		respawningTimer = 0;
		StartCoroutine (Respawning ());

		this.effects = "default";
		this.speed = speed / 10;
		this.maxSpeed = this.speed;
		this.level = 1;
		this.healtbarSoldier.SetActive (true);
		//		this.state = STATE.DEFAULT;
		if(heroUnity)
			StartCoroutine (HealingAndXp ());

		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		audioManager.PlayAudio ("spawn");


	}

	void Update () {
		if(tutorial == false)
		if (StaticController.instance.GameController.GameOver == true) {
			this.speed = 0;
			this.targetEnemy = null;
		}

		// VELOCIDADE
		if (previous.x < transform.position.x) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (previous.x > transform.position.x) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}

		velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
		previous = transform.position;

		//Debug.Log ("Velocity: "+ velocity);

		if (this.velocity <= 0) {
			anim.SetBool ("Walk", false);
			anim.SetBool ("Idle", true);
		} else if (this.velocity >= 1) {
			anim.SetBool ("Walk", true);
			anim.SetBool ("Idle", false);
		}





		// COLISÕES COM TROPAS E ADVERSÁRIOS

		foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("CharacterBound")) {
			if (CharBound.bounds.Intersects (obstacle.GetComponent<SpriteRenderer> ().bounds) && obstacle != this.CharBound.transform.gameObject) {
				if (this.CharBound.transform.position.x > obstacle.transform.position.x) {
					transform.Translate (Vector3.right * Time.deltaTime * 1f);
				} else {
					transform.Translate (Vector3.left * Time.deltaTime * 1f);
				}
				if (this.CharBound.transform.position.y > obstacle.transform.position.y) {
					transform.Translate (Vector3.up * Time.deltaTime * 1f);
				} else {
					transform.Translate (Vector3.down * Time.deltaTime * 1f);
				}

			}
			Debug.Log (obstacle.name);
		}



		//
		//ORDEM DE LAYER
		//
		//this.GetComponent<SpriteRenderer> ().sortingOrder = -(int)(this.transform.position.y - 0.5f);


		//EVENTO DE MORTE
		if (this.vida <= 0 && heroUnity && this.GetComponent<SpriteRenderer>().enabled == true && gameBegin == true) {
			this.speed = 0;
			Instantiate (deathAngel, this.transform.position, Quaternion.identity).transform.parent = this.transform;
			GameObject.Find ("RespawnTimerHero").GetComponent<RespawnTimer> ().ActiveRespawnTimer (respawningTimer);
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("herowaypoint")) {
				Destroy (o.gameObject);
			}
			StartCoroutine (Respawning ());
			Camera.main.gameObject.GetComponent<CameraShake> ().ShakeCamera ();
			audioManager.PlayAudio ("death");
		} else if(this.vida <= 0 && gameBegin == true) {
			audioManager.PlayAudio ("death");
			Destroy (this.gameObject);
		}
		if (this.vida > this.vidaMax) {
			this.vida = this.vidaMax;
		}

		//	
		//FLAGS PARA HEALING E XP
		//
		if (this.team == 1 && this.alive ==  true) {
//			if (this.transform.position.y < -1) {
//				this.healing = true;
//			} else {
//				this.healing = false;
//			}

			if (this.transform.position.y > 1) {
				this.GainXP = true;
			} else {
				this.GainXP = false;
			}
		} 

		if (this.team == 2 && this.alive ==  true) {
//			if (this.transform.position.y > 1) {
//				this.healing = true;
//			} else {
//				this.healing = false;
//			}

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
				this.vida++;
				UpdateLife ();
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
				this.vida++;
				UpdateLife ();
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
		if (heroUnity) {
			if (this.xp < xplvl2 && this.xp < xplvl3) {
				this.level = 1;
				xpMax = 25;
				LvlUpAnim.SetInteger ("LevelUpInt", -1);
				if (skill1.skillLevel <= this.level) {
					skill1.Enable ();
				}
				if (skill2.skillLevel <= this.level) {
					skill2.Enable ();
				}
			}
			if (this.xp > xplvl2 && this.xp < xplvl3) {
				this.level = 2;
				xpMax = 125;
				LvlUpAnim.SetInteger ("LevelUpInt", 1);
				if (skill1.skillLevel <= this.level) {
					skill1.Enable ();
				}
				if (skill2.skillLevel <= this.level) {
					skill2.Enable ();
				}
			}
			if (this.xp >= xplvl3) {
				this.level = 3;
				xpMax = 125;
				LvlUpAnim.SetInteger ("LevelUpInt", 2);
				if (skill1.skillLevel <= this.level) {
					skill1.Enable ();
				}
				if (skill2.skillLevel <= this.level) {
					skill2.Enable ();
				}
			}
		}
		if (XpBar != null)
			XpBar.SetFloat ("Blend", xp / xpMax);


		//
		//COOLDOWN DE EFEITOS
		//
		if(effects != "default"){
			effectsDuration += Time.deltaTime;
			if (effectsDuration > 3) {
				Removeeffect ();
				effects = "default";
			}
		}


		//
		//ESTADOS DO PERSONAGEM
		//

		//SEGUINDO OS MARCADORES DE MOVIMENTO

		if (GetNewWaypoint () != null && multiplayer == false) {
			lockedTarget = false;
			seeking = false;
			WaypointMark = GetNewWaypoint ();
			//targetEnemy = null;
			if(Vector3.Distance (transform.position, WaypointMark.transform.position) > range && WaypointMark.GetComponent<MovementMarkerScript> ().targetMarker == true ) {
				transform.position = Vector3.MoveTowards (transform.position, WaypointMark.transform.position, Time.deltaTime * speed);
			} else if (Vector3.Distance (transform.position, WaypointMark.transform.position) > 0.2f && WaypointMark.GetComponent<MovementMarkerScript> ().targetMarker == false) {
				//anim.SetTrigger ("Walk");
				if (WaypointMark.transform.position.x < transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
					anim.gameObject.transform.localPosition = new Vector3 (0.03f, 0.2f, 0f);
				} else if (WaypointMark.transform.position.x > transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
					anim.gameObject.transform.localPosition = new Vector3 (-0.05f, 0.2f, 0f);
				}
				transform.position = Vector3.MoveTowards (transform.position, WaypointMark.transform.position, Time.deltaTime * speed);
			} else {
				if (WaypointMark.GetComponent<MovementMarkerScript> ().capture == true && WaypointMark.GetComponent<MovementMarkerScript> ().enabled == true) {
					if (captureMarkerTimer >= 0.7f) {
						captureMarkerTimer = 0;
						Destroy (WaypointMark.gameObject);
						StartCoroutine (RedrawLine ());
					} else {
						captureMarkerTimer += Time.deltaTime;
					}
				} else if(WaypointMark.GetComponent<MovementMarkerScript> ().targetMarker == false) {
					Destroy (WaypointMark.gameObject);
					StartCoroutine (RedrawLine ());
				}
			}

		} else {
			seeking = true;
		}
			
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
			


		// PERSEGUINDO E ATACANDO ALVO ENCONTRADO

			if(seeking == false && this.targetEnemy != null ) { 

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
				anim.SetTrigger ("Walk");
				//SpendingEnergy ();
				//transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
				if (targetEnemy.transform.position.x < transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				} else if (targetEnemy.transform.position.x > transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				}


			} else if (targetEnemy != null && this.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO

				if (targetEnemy.transform.position.x < transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				} else if (targetEnemy.transform.position.x > transform.position.x) {
					anim.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				}
					

				if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
					anim.SetTrigger ("Attack");
					StartCoroutine (DealDamage ());
				} else {
					//danoCD += Time.deltaTime * 2;
				}
			}
		} else { // ALVO SUMIU OU MORREU
			if (GameObject.Find ("MovementMarker(Clone)")) {
				if (GameObject.Find ("MovementMarker(Clone)").GetComponent<MovementMarkerScript> ().herobase == false)
					Destroy (GameObject.Find ("MovementMarker(Clone)").gameObject);
			}
			seeking = true;
		} 
		danoCD += Time.deltaTime * 2;


	}

	public void SetupHero(){
		//CONFIGURAÇÃO DE TIPO DE HEROI
		int id = 1;
		if (PlayerPrefs.GetInt ("SelectedCharacter") != null && this.team == 1) {
			heroID = PlayerPrefs.GetInt ("SelectedCharacter");
		} else {
			heroID = 1;
		}

		if(tutorial==false)
		GameObject.Find ("HeroPortrait").GetComponent<Image> ().sprite = heroPortraits [heroID];
		
//		Debug.Log ("id: " + id);
		switch (heroID) {
		case(0): 
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 2;//3
			this.damage = 1;
			this.damageSpeed = 4;
			this.range = 2;
			this.speed = 13;
			this.energyMax = 3;
			this.energy = 3;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			//	this.anim.SetInteger ("Char", 0);
			//this.anim.GetComponent<SpriteRenderer> ().enabled = false;
			//this.gameObject.GetComponent<Animator> ().enabled = false;
			this.anim = transform.Find ("MonicaAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Monica");
			break;
		case(1):
			this.vidaMax = 4;
			this.vida = 4;
			this.reach = 2;//3
			this.damage = 1;
			this.damageSpeed = 4;
			this.range = 2;
			this.speed = 17;
			this.energyMax = 3;
			this.energy = 3;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			//this.anim.SetInteger ("Char", 1);
			this.anim = transform.Find ("CebolinhaAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Cebolinha");
			break;
		case(2):
			this.vidaMax = 7;
			this.vida = 7;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 4;
			this.range = 2;
			this.speed = 12;
			this.energyMax = 4;
			this.energy = 4;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("MagaliAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Magali");
			break;
		case(3):
			this.vidaMax = 4;
			this.vida = 4;
			this.reach = 0.5f;
			this.damage = 2;
			this.damageSpeed = 4;
			this.range = 2;
			this.speed = 13;
			this.energyMax = 4;
			this.energy = 4;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("CascãoAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Cascao");
			break;
		case(4):
			this.vidaMax = 4;
			this.vida = 4;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 13;
			this.energyMax = 4;
			this.energy = 4;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("ChicoAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Chico");
			break;
		default:
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 13;
			this.energyMax = 4;
			this.energy = 4;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("MonicaAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Monica");
			break;

		}

		// CONFIGURAÇÃO DE EQUIPE
		if (this.team == 1) {
			this.tag = "enemysoldier1";
		} else {
			this.tag = "enemysoldier2";
			this.GetComponent<SpriteRenderer>().flipX = true;
			platform.GetComponent<SpriteRenderer> ().color = Color.red;

		}

	}
		

	public void UpdateLife(){
		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarSoldier.GetComponent<HealtBar> ().MaxLife = this.vidaMax;
		this.healtbarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
	}
		

	public void ReceiveEffect(string effect){
		effects = effect;

		if (effect == "sight") {
			this.range += 2;
			this.reach += 2;
		}
		if (effect == "slow") {
			this.GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 1f);
			this.speed = speed / 2;
		}
		if (effect == "extraSlow") {
			this.speed = speed / 4;
		}
		if (effect == "speed") {
			this.speed = speed + 0.1f;
		}
		if (effect == "damage") {
			this.vida --;
			UpdateLife ();
		}
		if (effect == "extraDamage") {
			this.vida -= 2;
			UpdateLife ();
		}
		if (effect == "healing") {
			this.vida++;
			UpdateLife ();
		}
		if (effect == "extraGealing") {
			this.vida+=5;
			UpdateLife ();
		}
		if (effect == "shield") {
			this.vida++;
			UpdateLife ();
		}
		if (effect == "warShout") {
			this.damage++;
			this.damageSpeed++;
			this.speed = speed * 2;
		}
		if (effect == "increaseDamage") {
			this.damage++;
		}
		if (effect == "sleep") {
			this.damage--;
			this.damageSpeed--;
			this.speed = 0;
		}
	}

	public void Removeeffect(){
		if (effects == "sight") {
			this.range -= 2;
			this.reach -= 2;
		}
		if (effects == "slow") {
			GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Unfrozen");
			this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f);
			this.speed = maxSpeed;
		}
		if (effects == "speed") {
			this.speed = maxSpeed;
		}
		if (effects == "shield") {
			this.vida --;
		}
		if (effects == "warShout") {
			this.damage--;
			this.damageSpeed--;
			this.speed = maxSpeed;
		}
		if (effects == "sleep") {
			this.damage++;
			this.damageSpeed++;
			this.speed = maxSpeed;
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
				if (team == 1) {
					GameController.playerXp = this.xp;
				} else {
					GameController.enemyXp = this.xp;
				}
				StartCoroutine (HealingAndXp ());

			} 
		} else {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (HealingAndXp ());
		}

	}

	IEnumerator Respawning(){
		GameObject.Find ("RespawnTimerHero").GetComponent<RespawnTimer> ().ActiveRespawnTimer (respawningTimer);
		yield return new WaitForSeconds (0.01f);
		alive = false;
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.anim.GetComponent<SpriteRenderer>().enabled = false;
		this.platform.GetComponent<SpriteRenderer> ().enabled = false;
		this.healtbarSoldier.SetActive (false);
		//this.energybarSoldier.SetActive (false);
		this.skill1.gameObject.SetActive (false);
		this.skill2.gameObject.SetActive (false);
		this.vida = this.vidaMax;
		UpdateLife ();
//		if(heroUnity)
//			this.energy = this.energyMax;
		this.seeking = false;
		yield return new WaitForSeconds (respawningTimer);

		if(heroUnity)
			transform.position = heroBase.transform.position;
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		this.anim.GetComponent<SpriteRenderer>().enabled = true;
		this.platform.GetComponent<SpriteRenderer> ().enabled = true;
		this.healtbarSoldier.SetActive (true);
//		if (heroUnity)
//			this.energybarSoldier.SetActive (true);
		this.skill1.gameObject.SetActive (true);
		this.skill2.gameObject.SetActive (true);
//		this.damage = 1;
		this.speed = maxSpeed;
		this.seeking = true;
		//this.targetEnemy = SeekEnemyTarget();
		respawningTimer = 12;
		Arrival.Play ();
		alive = true;
		gameBegin = true;
	}

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;
		if (this.tag == "enemysoldier1") { //Procura de Jogador 1
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
								}else if (obj.GetComponent<EnemyRemoteController> () != null) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					}
				}
			} 
			if (GameObject.FindGameObjectsWithTag ("enemytower2") != null) {//PROCURA BASE
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemytower2")) {
						float dist = Vector3.Distance (transform.position, obj.transform.position);
						if (dist <= reach) {
							if (dist < minDis) {
								if (obj.GetComponent<ChargesScript> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
				}
			} 
		

		} else if (this.tag == "enemysoldier2") { //Procura de Jogador 2
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

			if (GameObject.FindGameObjectsWithTag ("enemytower1") != null) {//PROCURA BASE
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemytower1")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist <= reach) {
						if (dist < minDis) {
							if (obj.GetComponent<ChargesScript> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
								Emin = obj;
								minDis = dist;
							}
						}
					}
				}
			}
		
		}
		return Emin;
	}

	IEnumerator DealDamage(){
		danoCD = 0;
		yield return new WaitForSeconds (0.5f);
		if (targetEnemy != null) {
			if(multiplayer == true){ //ALVO HEROI MULTIPLAYER
				if (targetEnemy.GetComponent<EnemyRemoteController> ().alive == false) {
					this.targetEnemy = null;
					lockedTarget = false;
				} else {
					if (NetGameController.Instance.HasGameStarted()) {
						CommandController.AttackEnemyHero ();
						NetClock.Instance.RegisterInputTime();
					} 
					if (this.range > 1)
						TrowArrow ();
				}
			}else if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {//ALVO HEROI IA
				//targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
				//targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
//				if (this.range > 1)
//					TrowArrow ();
				if (targetEnemy.GetComponent<WPIASoldierControler> ().alive == false) { // ALVO MORREU
					this.targetEnemy = null;
					lockedTarget = false;
				} else {
					targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
					if (this.range > 1)
						TrowArrow ();
				}
			} else if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO TROPA
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
//				targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
//				if (this.range > 1)
//					TrowArrow ();
				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
					this.targetEnemy = null;
					lockedTarget = false;
				} else {
					targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
					if (this.range > 1)
						TrowArrow ();
				}
			} else if (targetEnemy.GetComponent<ChargesScript> () != null) {//ALVO TORRE
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				targetEnemy.GetComponent<ChargesScript> ().progress += 0.3f;
				if (this.range > 1)
					TrowArrow ();
//				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
//					this.targetEnemy = null;
//					lockedTarget = false;
//				}

			}

			if (this.range > 1) {
				audioManager.PlayAudio ("shot");
			} else {
				audioManager.PlayAudio ("atack");
			}
		}
	}

	public void ReceiveDamage(int x){

		this.vida -= x;
		UpdateLife ();

		Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);

		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");

		}

	}


	public void TrowArrow(){
		StartCoroutine (DelayedHitEffect ());
		GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (targetEnemy != null) {
			arrow.GetComponent<ArrowScript> ().target = targetEnemy;
		} else {
			Destroy (arrow);
		}

	}

	IEnumerator DelayedHitEffect(){
		yield return new WaitForSeconds (0.3f);
		Instantiate (HitAnimationObject, targetEnemy.transform.position, Quaternion.identity);

	}

	public GameObject GetNewWaypoint(){
		GameObject[] wps = GameObject.FindGameObjectsWithTag ("herowaypoint");
		if (wps != null) {
			foreach (GameObject wp in wps) {
				if (wp.GetComponent<MovementMarkerScript> ().progress == 1) {
					return wp;
				} else if (wp.GetComponent<MovementMarkerScript> ().progress == 2) {
					return wp;
				} else if (wp.GetComponent<MovementMarkerScript> ().progress == 3) {
					return wp;
				} else {
					return null;
				}
			}
		} else {
			return null;
		}
		return null;
	}

	IEnumerator RedrawLine(){
		
//		foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
//			o.name = "Waypoint1";
//			o.GetComponent<MovementMarkerScript> ().progress = 1;
//			GameObject.Find ("Line1").GetComponent<LineScript> ().endObject = o;
//		}
		if(GameObject.Find("Waypoint2")){
			GameObject.Find ("Waypoint2").GetComponent<MovementMarkerScript> ().progress = 1;
			GameObject.Find ("Waypoint2").GetComponent<MovementMarkerScript> ().name = "Waypoint1";
			GameObject.Find ("Terreno").GetComponent<WPScript> ().progress = 2;
		}

		yield return new WaitForSeconds (0.01f);
		GameObject.Find ("Line1").GetComponent<LineScript> ().firstLineDraw = false;
		yield return new WaitForSeconds (0.01f);
		WaypointMark = GetNewWaypoint ();
	}

}

