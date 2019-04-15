using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YupiPlay.MMB.Multiplayer;

public class SoldierControler : Bolt.EntityEventListener<ITroopState> {

	//public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	public enum STATE
	{
		DEFAULT,
		RETREAT,
	}

	//public TipoSoldado Tipo;

	public bool heroUnity;

	public int troopId;

	public bool multiplayer;

	public float topPreference;
	public float midPreference;
	public float botPreference;

	public STATE state = STATE.DEFAULT;

	public int team;

	[HideInInspector]
	public Vector3 touchStartPosition;

	[HideInInspector]
	public Vector3 touchEndPosition;

	//PREFABS

	public GameObject HeroCharacter;

	public GameObject EnemyCharacter;

	public GameObject SoldierCharacter;


	public SpriteRenderer CharBound;

	public GameObject targetEnemy;

	public GameObject healtbarSoldier;

	public HealtBarSolid healtbarSoldierSolid;

	public GameObject energybarSoldier;

	public Sprite warrior;

	public Sprite[] tropasSprites;

	public GameObject FlashingEffects;

	public GameObject platform;

	public GameObject heroBase;

	public GameObject[] animations;
	public bool haveAnimation;             //Animations

	public AudioManager audioManager;

	public GameObject deathAngel;

	public Animator XpBar;

	public Animator LvlUpAnim;

	public GameObject HitAnimationObject;

	public GameObject troop;

	public GameObject SplashEffect;
	//FLAGS

	public bool alive;

	private bool gameEnded = false;

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	public bool seeking;

	public float cdSeek;

	public float respawningTimer;

	public float specialHabilityCD;

	public float specialHabilityCDTimer;

	public bool skillshoter;
	//STATUS

	public bool Custom;

	public float xp;

	public float xpLvl1 = 0;
	public float xplvl2 = 25;
	public float xplvl3 = 125;

	private float xpMax;

	public int level;

	private bool levelUp;


	public float vidaMax;


	public float vida;

	public float reach;


	public float damage;

	public float tempdamage;


	public float damageSpeed;


	public float range;

	public float danoCD = 0;

	//[HideInInspector]
	public float speed;

	[HideInInspector]
	private float maxSpeed;

	[HideInInspector]
	public int energyMax;

	private bool tired;

	[HideInInspector]
	public int energy;

	public bool resting;

	public bool explosiveDamage;

	private float energyCounter;

	private float summonCounter;
	public bool summon = false;

	public string effects;
	public float effectsDuration;


	//LANE WAYPOINTS

	public GameObject[] LaneTop;
	public GameObject[] LaneMid;
	public GameObject[] LaneBot;

	public GameObject[] LeftExit = new GameObject[3];
	public GameObject[] RightExit = new GameObject[3];
	public int step;
	public Transform t;


	public int lane;
	public string LaneName;
	public GameObject[] ActualLane;

	public int Progress;


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
		if(step == 0)
		step = 1;

		 LeftExit = new GameObject[3];
		 RightExit = new GameObject[3];
		if (troopId != 2) {
			if (team == 1) {
				LeftExit [0] = GameObject.Find ("Left1").gameObject;
				LeftExit [1] = GameObject.Find ("Left2").gameObject;
				LeftExit [2] = GameObject.Find ("Left3").gameObject;
				RightExit [0] = GameObject.Find ("Right1").gameObject;
				RightExit [1] = GameObject.Find ("Right2").gameObject;
				RightExit [2] = GameObject.Find ("Right3").gameObject;
			} else {
				LeftExit [0] = GameObject.Find ("Left1Enemy").gameObject;
				LeftExit [1] = GameObject.Find ("Left2Enemy").gameObject;
				LeftExit [2] = GameObject.Find ("Left3Enemy").gameObject;
				RightExit [0] = GameObject.Find ("Right1Enemy").gameObject;
				RightExit [1] = GameObject.Find ("Right2Enemy").gameObject;
				RightExit [2] = GameObject.Find ("Right3Enemy").gameObject;	
			}

		} else {
			if (team == 1) {
				LeftExit [0] = GameObject.Find ("Left2").gameObject;
				LeftExit [1] = GameObject.Find ("Left2").gameObject;
				LeftExit [2] = GameObject.Find ("Left3").gameObject;
				RightExit [0] = GameObject.Find ("Right2").gameObject;
				RightExit [1] = GameObject.Find ("Right2").gameObject;
				RightExit [2] = GameObject.Find ("Right3").gameObject;
			} else {
				LeftExit [0] = GameObject.Find ("Left2Enemy").gameObject;
				LeftExit [1] = GameObject.Find ("Left2Enemy").gameObject;
				LeftExit [2] = GameObject.Find ("Left3Enemy").gameObject;
				RightExit [0] = GameObject.Find ("Right2Enemy").gameObject;
				RightExit [1] = GameObject.Find ("Right2Enemy").gameObject;
				RightExit [2] = GameObject.Find ("Right3Enemy").gameObject;	

			}
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
		ChooseLane ();

//		if (heroUnity) {// CONFIGURAÇÃO DE HEROIS
//			SetupHero();
//		} else {//CONFIGURAÇÃO DE TROPAS
			SetupTroop(troopId);
//		}
			
		//CONFIGURAÇÃO EM COMUM 
		//UpdateLife ();
		//this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();

		//UpdateEnergy ();
		//this.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		//this.energybarSoldier.GetComponent<HealtBar> ().energy = true;

		if(heroUnity)
		if (team == 1) {
			this.xp = GameController.playerXp;
		} else {
			this.xp = GameController.enemyXp;
		}

//		if (GetComponent<Animator> () != null) {
//			anim = GetComponent<Animator> ();
//		}
//		respawningTimer = 0.5f;
//		StartCoroutine (Respawning ());

		this.effects = "default";
		this.speed = speed /2;
		if (this.speed < 0) {
			this.speed = 0.1f;
		}
		//this.damageSpeed = damageSpeed + 1.5f;
		this.maxSpeed = this.speed;
		this.range = range - 0.3f;
		//this.reach = 1.5f;
		this.level = 1;
		if (healtbarSoldierSolid != null) {
			this.healtbarSoldierSolid.transform.gameObject.SetActive (true);
		}
		this.state = STATE.DEFAULT;

		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		audioManager.PlayAudio ("spawn");

		//DESLOCAMENTO INICIAL
		HeroCharacter = GameObject.Find("HeroSpritezone");
		EnemyCharacter = GameObject.Find ("EnemySpritezone");
	

	}
	
	void Update() {
		if (multiplayer == false) {
			__Update();
			return;
		}

		if (!entity.isOwner) {
			__Update();
		}
	}

	public override void SimulateOwner() {
		__Update();
	}

	void __Update () {

		if (GameObject.Find ("GameController").GetComponent<GameController> ().GameOver) {
			Destroy (this.gameObject);
		}

		if (healtbarSoldierSolid != null) {
			healtbarSoldierSolid.maxValue = vidaMax;
			healtbarSoldierSolid.atualValue = vida;
		}


		//
		//ORDEM DE LAYER
		//
		//this.GetComponent<SpriteRenderer> ().sortingOrder = -(int)(this.transform.position.y - 0.5f);


		//DESLOCAMENTO LATERAL COM INTERSESSÃO
//		if (gameObject.GetComponent<SpriteRenderer> ().bounds.Intersects (HeroCharacter.GetComponent<SpriteRenderer> ().bounds)) {
//			if (this.transform.position.x > HeroCharacter.transform.position.x) {
//				transform.Translate (Vector3.right * Time.deltaTime * 0.5f);
//			} else {
//				transform.Translate (Vector3.right * Time.deltaTime *  0.5f);
//			}
//			if (this.transform.position.y > HeroCharacter.transform.position.y) {
//				transform.Translate (Vector3.up * Time.deltaTime *  0.5f);
//			} else {
//				transform.Translate (Vector3.down * Time.deltaTime *  0.5f);
//			}
//
//
//		}
//		if (gameObject.GetComponent<SpriteRenderer> ().bounds.Intersects (EnemyCharacter.GetComponent<SpriteRenderer> ().bounds)) {
//			if (this.transform.position.x > EnemyCharacter.transform.position.x) {
//				transform.Translate (Vector3.right * Time.deltaTime * 0.5f);
//			} else {
//				transform.Translate (Vector3.right * Time.deltaTime * 0.5f);
//			}
//			if (this.transform.position.y > EnemyCharacter.transform.position.y) {
//				transform.Translate (Vector3.up * Time.deltaTime * 0.5f);
//			} else {
//				transform.Translate (Vector3.down * Time.deltaTime * 0.5f);
//			};
//		}
		foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("CharacterBound")) {
			if (CharBound.bounds.Intersects (obstacle.GetComponent<SpriteRenderer> ().bounds) && obstacle != this.CharBound.transform.gameObject) {
				if (this.CharBound.transform.position.x > obstacle.transform.position.x) {
					transform.Translate (Vector3.right * Time.deltaTime * 0.5f);
				} else {
					transform.Translate (Vector3.left * Time.deltaTime * 0.5f);
				}
				if (this.CharBound.transform.position.y > obstacle.transform.position.y) {
					transform.Translate (Vector3.up * Time.deltaTime * 0.5f);
				} else {
					transform.Translate (Vector3.down * Time.deltaTime * 0.5f);
				}
					
			}
		
		}
			
		//
		//EVENTO DE MORTE
		//

		if(this.vida <= 0) {
			StartCoroutine (DeathEvent ());
			//audioManager.PlayAudio ("death");
		}

		if (this.vida > this.vidaMax) {
			this.vida = this.vidaMax;
		}


		//	
		//FLAGS PARA HEALING
		//
		if (this.team == 1 && this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
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

		if (this.team == 2 && this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
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
		specialHabilityCDTimer += Time.deltaTime;
		if (targetEnemy != null) {
			if(Vector2.Distance(transform.position, targetEnemy.transform.position)>1f){
				if (this.troopId == 1 && specialHabilityCDTimer > 3) {
				specialHabilityCDTimer = 0;
				transform.GetComponent<Rigidbody2D> ().AddForce ((targetEnemy.transform.position - transform.position).normalized * 60 );
			}
			}
		}

//		if(this.GetComponent<SpriteRenderer>().enabled == true){
//			summonCounter += Time.deltaTime;
//		}
//
//		if (summonCounter >= 9 && this.summon == true) {
//			summonCounter = 0;
//			GameObject t =  troop;
//			t.GetComponent<SoldierControler> ().troopId = 3;
//			t.GetComponent<SoldierControler> ().step = this.step;
//			t.GetComponent<SoldierControler> ().summon = false;
//			Instantiate (t, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y), Quaternion.identity);
//			Instantiate (t, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
//		}
			

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
		if ( LeftExit.Length > 0 && RightExit.Length > 0  && targetEnemy == null) {

			if (Vector2.Distance (this.transform.position, LeftExit [0].transform.position) <= 0.2f || Vector2.Distance (this.transform.position, RightExit [0].transform.position) <= 0.2f) {
				step = 2;
			}
			if (Vector2.Distance (this.transform.position, LeftExit [1].transform.position) <= 0.2f || Vector2.Distance (this.transform.position, RightExit [1].transform.position) <= 0.2f) {
				step = 3;
			}

			if (Vector2.Distance (this.transform.position, LeftExit [0].transform.position) > Vector2.Distance (this.transform.position, RightExit [0].transform.position) && step == 1) {
				t = RightExit [0].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [0].transform.position, Time.deltaTime * speed);
			} else if (Vector2.Distance (this.transform.position, LeftExit [0].transform.position) < Vector2.Distance (this.transform.position, RightExit [0].transform.position) && step == 1) {
				t = LeftExit [0].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [0].transform.position, Time.deltaTime * speed);
			}

			if (Vector2.Distance (this.transform.position, LeftExit [1].transform.position) > Vector2.Distance (this.transform.position, RightExit [1].transform.position) && step == 2) {
				t = RightExit [1].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [1].transform.position, Time.deltaTime * speed);
			} else if (Vector2.Distance (this.transform.position, LeftExit [1].transform.position) < Vector2.Distance (this.transform.position, RightExit [1].transform.position) && step == 2) {
				t = LeftExit [1].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [1].transform.position, Time.deltaTime * speed);
			}

			if (Vector2.Distance (this.transform.position, LeftExit [2].transform.position) > Vector2.Distance (this.transform.position, RightExit [2].transform.position) && step == 3) {
				t = RightExit [2].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [2].transform.position, Time.deltaTime * speed);
			} else if (Vector2.Distance (this.transform.position, LeftExit [2].transform.position) < Vector2.Distance (this.transform.position, RightExit [2].transform.position) && step == 3)  {
				t = LeftExit [2].transform;
				//transform.position = Vector2.MoveTowards (transform.position, RightExit [2].transform.position, Time.deltaTime * speed);
			}
			if (t.position.x > transform.position.x) {
				transform.eulerAngles = new Vector3 (0, 180, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, 0, 0);
			}
			transform.position = Vector2.MoveTowards (transform.position, t.position, Time.deltaTime * speed);
			if (haveAnimation)
				this.transform.Find("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
		}		



		// PROCURANDO ALVO NOVO

		if (seeking && cdSeek >= 1) {
			this.targetEnemy = SeekEnemyTarget ();
			if (this.targetEnemy != null) {
				seeking = false;
			}
			cdSeek = 0;
		} else {
			cdSeek += Time.deltaTime;
		}



		// PERSEGUINDO E ATACANDO ALVO ENCONTRADO

		if (this.team == 1) { // VERIFICA EQUIPE 
		
			if (targetEnemy != null && seeking == false) { // VERIFICA SE EXISTE ALVO E SE PAROU A BUSCA


				if (Vector3.Distance (transform.position, targetEnemy.transform.position) > reach) {
					seeking = true;
					targetEnemy = null;
				} else {
					//DESLOCAMENTO ATE INIMIGO
					if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
						this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

						//SpendingEnergy ();
						//					Debug.Log ("Perseguindo");
						transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
						//seeking = true;
					} else if (targetEnemy != null && this.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO
						if (targetEnemy.transform.position.x < transform.position.x) {
							transform.eulerAngles = new Vector3 (0, 180, 0);
						} else {
							transform.eulerAngles = new Vector3 (0, 0, 0);
						}

						if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES

							if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {//VERIFICA SE ALVO HEROI
								if (targetEnemy.GetComponent<WPIASoldierControler> ().alive == true) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else if (targetEnemy.GetComponent<ChargesScript> () != null) {//VERIFICA SE ALVO É BASE
								if (targetEnemy.GetComponent<ChargesScript> ()!= null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {//VERIFICA SE ALVO É TORRE
								if (targetEnemy.GetComponent<ChargesScriptTowers> ()!= null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else {//VERIFICA SE ALVO É TROPA
								if (targetEnemy.GetComponent<SoldierControler> ()!= null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							}

						} 

					}
				}
					
			} else if(targetEnemy == null){ // VERIFICA SE EXISTE ALVO E SE PAROU A BUSCA
				seeking = true;
				this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
			}

		} else if(this.team == 2) { // VERIFICA EQUIPE

			if (targetEnemy != null && seeking == false) { // VERIFICA SE EXISTE ALVO E SE PAROU A BUSCA


				if (Vector3.Distance (transform.position, targetEnemy.transform.position) > reach) {
					seeking = true;
					targetEnemy = null;
				} else {
					//DESLOCAMENTO ATE INIMIGO
					if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
						this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

						//SpendingEnergy ();
						//					Debug.Log ("Perseguindo");
						transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
						//seeking = true;
					} else if (targetEnemy != null && this.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO
						if (targetEnemy.transform.position.x < transform.position.x) {
							transform.eulerAngles = new Vector3 (0, 180, 0);
						} else {
							transform.eulerAngles = new Vector3 (0, 0, 0);
						}

						if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES

							if (targetEnemy.GetComponent<WPSoldierControler> () != null) {//VERIFICA SE ALVO HEROI
								if (targetEnemy.GetComponent<WPSoldierControler> ().alive == true) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else if (targetEnemy.GetComponent<ChargesScript> () != null) {//VERIFICA SE ALVO É BASE
								if (targetEnemy.GetComponent<ChargesScript> () != null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {//VERIFICA SE ALVO É TORRE
								if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							} else {//VERIFICA SE ALVO É TROPA
								if (targetEnemy.GetComponent<SoldierControler> () != null) {//VERIFICA ALVO AINDA VIVE
									StartCoroutine (DealDamage ());
								} else {
									targetEnemy = null;
									seeking = true;
									this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");

								}
							}

						} 

					} else if(targetEnemy == null) {
						seeking = true;
						this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
					}
				}

			} else if(targetEnemy == null){ // VERIFICA SE EXISTE ALVO E SE PAROU A BUSCA
				seeking = true;
				this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
			}
		}


//		if (seeking == false && this.targetEnemy != null) { 
//
//
//			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > reach) {
//				seeking = true;
//				targetEnemy = null;
//			} else {
//				//DESLOCAMENTO ATE INIMIGO
//				if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
//					this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
//
//					//SpendingEnergy ();
////					Debug.Log ("Perseguindo");
//					transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
//					//seeking = true;
//				} else if (targetEnemy != null && this.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO
//						if (targetEnemy.transform.position.x < transform.position.x) {
//							transform.eulerAngles = new Vector3 (0, 180, 0);
//						} else {
//							transform.eulerAngles = new Vector3 (0, 0, 0);
//						}
//
//						if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
//							danoCD = 0;
//							if (haveAnimation)
//								this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
//							//anim.SetTrigger ("Attack");
//
//							StartCoroutine (DealDamage ());
//						} 
//		
//				} 
//			}
//		} else if(this.targetEnemy == null)  {
//			seeking = true;
//			this.transform.Find ("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Walk");
//
//		}

		danoCD += Time.deltaTime;

	}

	public void SetupTroop(int id){


		switch (id) {
		case(1): // BIDU
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card111");//75;//Medio
			this.vida = PlayerPrefs.GetFloat ("Card111");//75;//Medio
			this.damage = PlayerPrefs.GetFloat ("Card112");//22;//Medio
			this.damageSpeed = PlayerPrefs.GetFloat ("Card113");//0.5f;//Medio
			this.range = PlayerPrefs.GetFloat ("Card114");//1; //Baixissimo
			this.speed = PlayerPrefs.GetFloat ("Card115");//1.3f; //Medio
			}
			this.reach = 2;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.specialHabilityCD = 2;
			this.explosiveDamage = false;
			this.skillshoter = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [0];
			this.haveAnimation = true;
			Instantiate (animations [1], transform);
			break;
		case(2): // ASTRONAUTA
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card121");//35;//Baixo
			this.vida = PlayerPrefs.GetFloat ("Card121");//35;//Baixo
			this.damage = PlayerPrefs.GetFloat ("Card122");//22; //Medio
			this.damageSpeed = PlayerPrefs.GetFloat ("Card123");//1; //Baixo
			this.range = PlayerPrefs.GetFloat ("Card124");//3; //Alto
			this.speed = PlayerPrefs.GetFloat ("Card125");//1.3f; //Medio
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = false;
			this.skillshoter = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [1];
			//this.GetComponent<SpriteRenderer> ().flipX = true;
			Instantiate (animations [2], transform);
			this.haveAnimation = true;
			break;
		case(3): //ANJINHO -> Cranicola
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card131");//15; //Baixissimo
			this.vida = PlayerPrefs.GetFloat ("Card131");//15; //Baixissimo
			this.damage = PlayerPrefs.GetFloat ("Card132");//50; //Alto
			this.damageSpeed = PlayerPrefs.GetFloat ("Card133");//0.25f; //Altissimo
			this.range = PlayerPrefs.GetFloat ("Card134");//1; //Baixissimo
			this.speed = PlayerPrefs.GetFloat ("Card135");//1.7f; //Alto
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = true;
			this.skillshoter = true;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [2];
			Instantiate (animations [3], transform);
			this.haveAnimation = true;
			break;
		case(4): //JOTALHÃO
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card141");//200; //Alto
			this.vida = PlayerPrefs.GetFloat ("Card141");//200; //Alto
			this.damage = PlayerPrefs.GetFloat ("Card142");//50; //Alto
			this.damageSpeed = PlayerPrefs.GetFloat ("Card143");//1; //Baixo
			this.range = PlayerPrefs.GetFloat ("Card144");//1; //Baixissimo
			this.speed = PlayerPrefs.GetFloat ("Card145");//0.8f; //Baixo
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = false;
			this.skillshoter = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [3];
			Instantiate (animations [4], transform);
			this.haveAnimation = true;
			break;
		case(5): //PITECO
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card151");//35; //Baixo
			this.vida = PlayerPrefs.GetFloat ("Card151");//35; //Baixo
			this.damage = PlayerPrefs.GetFloat ("Card152");//22; // Medio
			this.damageSpeed = PlayerPrefs.GetFloat ("Card153");//0.5f; //medio
			this.range = PlayerPrefs.GetFloat ("Card154");//2; //Medio
			this.speed = PlayerPrefs.GetFloat ("Card155");//1.3f; //Medio
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = true;
			this.skillshoter = true;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [4];
			Instantiate (animations [5], transform);
			this.haveAnimation = true;
			break;
		case(6): //PENADINHO
			if(Custom == false){
			this.vidaMax =PlayerPrefs.GetFloat ("Card161");//75; //Medio
			this.vida = PlayerPrefs.GetFloat ("Card161");//75; //Medio
			this.damage = PlayerPrefs.GetFloat ("Card162");//22; //Medio
			this.damageSpeed = PlayerPrefs.GetFloat ("Card163");//0.5f; //Medio
			this.range = PlayerPrefs.GetFloat ("Card164");//2; //Medio
			this.speed = PlayerPrefs.GetFloat ("Card165");// 0.8f; //Baixo
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = true;
			this.explosiveDamage = true;
			this.skillshoter = true;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [5];
			//this.GetComponent<SpriteRenderer> ().flipX = true;
			Instantiate (animations [6], transform);
			this.haveAnimation = true;
			break;
		case(7): //MAURICIO -> off
			
			this.vidaMax = 10;
			this.vida = 10;
			this.reach = 0.5f;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 2;
			this.speed = 3;

			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [6];
			break;
		case(8): //SANSAO
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card181");//35; //Baixo
			this.vida = PlayerPrefs.GetFloat ("Card181");//35; //Baixo
			this.damage =PlayerPrefs.GetFloat ("Card182");// 14; //Baixo
			this.damageSpeed =PlayerPrefs.GetFloat ("Card183");// 0.5f; //Medio
			this.range =PlayerPrefs.GetFloat ("Card184"); ///1; //Baixissimo
			this.speed =PlayerPrefs.GetFloat ("Card185"); //1.3f; //Medio
			}
			this.reach =  1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = false;
			this.skillshoter = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [7];
			Instantiate (animations [8], transform);
			this.haveAnimation = true;
			break;
		case(9): //MINGAU
			if(Custom == false){
			this.vidaMax = PlayerPrefs.GetFloat ("Card191");//75; //Medio
			this.vida = PlayerPrefs.GetFloat ("Card191");//75; //Medio
			this.damage = PlayerPrefs.GetFloat ("Card192"); //22; //Medio
			this.damageSpeed = PlayerPrefs.GetFloat ("Card193");//0.33f;  //Alto
			this.range = PlayerPrefs.GetFloat ("Card194");//1; //Baixissimo
			this.speed = PlayerPrefs.GetFloat ("Card195");//1.7f; //Alto
			}
			this.reach = 1.5f;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.explosiveDamage = false;
			this.skillshoter = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [8];
			Instantiate (animations [9], transform);
			this.haveAnimation = true;
			break;
		case(10): //ALFREDO
			
			this.vidaMax = 9;
			this.vida = 9;
			this.reach = 1.5f;
			this.damage = 5;
			this.damageSpeed = 5;
			this.range = 2;
			this.speed = 3;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [9];
			break;

			/// MINIONS

		case(11): //MUMINHO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach =  1.5f;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2;
			this.speed = 3;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [10];
			break;
		default:
			Debug.LogWarning ("TroopOutofRange");
			break;

		}

		// CONFIGURAÇÃO DE EQUIPE
		if (this.team == 1) {
			this.tag = "enemysoldier1";
		} else {
			this.tag = "enemysoldier2";
			this.GetComponent<SpriteRenderer>().flipX = true;
			//platform.GetComponent<SpriteRenderer> ().color = Color.red;

		}
	}


//	public void ChangeState(){
//	
//		if (this.state == STATE.RETREAT) {
//			this.state = STATE.DEFAULT;
//		}else if (this.state == STATE.DEFAULT) {
//			this.state = STATE.RETREAT;
//		}
//
//	}

	public void UpdateLife(){
//		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
//		this.healtbarSoldier.GetComponent<HealtBar> ().MaxLife = this.vidaMax;
//		this.healtbarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
	}

//	public void UpdateEnergy(){
//		this.energybarSoldier.GetComponent<HealtBar> ().Life = this.energy;
//		this.energybarSoldier.GetComponent<HealtBar> ().MaxLife = this.energyMax;
//		this.energybarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
//	}

//	public void SpendingEnergy(){
//		energyCounter += Time.deltaTime;
//		if (energyCounter > 2 && resting == false) {
//			this.energy--;
//			UpdateEnergy ();
//			energyCounter = 0;
//		}
//
//		if (energyCounter > 2 && resting == true) {
//			this.energy++;
//			UpdateEnergy ();
//			energyCounter = 0;
//		}
//
//		if (this.energy >= this.energyMax) {
//			resting = false;
//			//this.speed = maxSpeed;
//			tired = false;
//		}
//
//		if (this.energy <= 0) {
//			resting = true;
//
//			if (tired == false) {
//				tired = true;
//				//this.speed = speed / 2;
//			}
//		}
//
//	}

	public void ReceiveEffect(string effect){
		effects = effect;
		if (effect == "slow") {
			this.speed = speed / 2;
		}
		if (effect == "extraSlow") {
			this.speed = speed / 4;
		}
		if (effect == "speed") {
			this.speed = speed * 2;
		}
		if (effect == "damage") {
			this.vida--;
			UpdateLife ();
		}
		if (effect == "extraDamage") {
			this.vida -= 2;
			UpdateLife ();
		}
		if (effect == "healing") {
			this.vida += 22;
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
		if (effect == "sleep") {
			this.damage--;
			this.damageSpeed--;
			this.speed = 0;
		}
	}

	public void Removeeffect(){
		if (effects == "slow") {
			GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Unfrozen");
			this.speed = maxSpeed;
		}
		if (effects == "speed") {
			this.speed = maxSpeed;
		}
		if (effects == "shield") {
			this.vida--;
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

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;


		//Procura de Jogador 1

		if (this.tag == "enemysoldier1") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null && troopId != 4) {//PROCURA TROPA
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (obj.GetComponent<SoldierControler> () != null) {
						if (dist <= reach) {
							if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
								if (this.range > 1) {
									Emin = obj;
									minDis = dist;
								} else if(obj.GetComponent<SoldierControler> ().troopId != 2) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					} else if (obj.GetComponent<WPIASoldierControler> () != null) {
						if (obj.GetComponent<WPIASoldierControler> ().alive == true) {
							if (dist <= reach) {
								if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
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
					if (obj.GetComponent<ChargesScriptTowers> () != null) {
						if (dist <= reach) {
							if (dist < minDis) {
								if (obj.GetComponent<ChargesScriptTowers> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					} else if (obj.GetComponent<ChargesScript> () != null) {
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


			//Procura de Jogador 2

		} else if (this.tag == "enemysoldier2") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null && troopId != 4) {//PROCURA TROPA
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (obj.GetComponent<SoldierControler> () != null) {
						if (dist <= reach) {
							if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
								if (this.range > 1) {
									Emin = obj;
									minDis = dist;
								} else if(obj.GetComponent<SoldierControler> ().troopId != 2) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					} else if (obj.GetComponent<WPSoldierControler> () != null) {
						if (obj.GetComponent<WPSoldierControler> ().alive == true) {
							if (dist <= reach) {
								if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
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
					if (obj.GetComponent<ChargesScriptTowers> () != null) {
						if (dist <= reach) {
							if (dist < minDis) {
								if (obj.GetComponent<ChargesScriptTowers> () != null /*&& obj.GetComponent<SoldierControler> ().LaneName == this.LaneName*/) {
									Emin = obj;
									minDis = dist;
								}
							}
						}
					}else if (obj.GetComponent<ChargesScript> () != null) {
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


		}
		return Emin;
	}

	IEnumerator DealDamage(){
		tempdamage = this.damage;
		danoCD = 0;
		yield return new WaitForSeconds (0.5f);
		if (targetEnemy != null) { 
			if (targetEnemy.GetComponent<WPSoldierControler> () != null) {//ALVO HEROI
				//targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
				if (targetEnemy.GetComponent<WPSoldierControler> ().alive == false) { // ALVO MORREU
					this.targetEnemy = null;
					//lockedTarget = false;
				} else {
					
					if (this.range > 1) {
						TrowArrow ();
					} else {
						if (troopId != 5 && troopId != 6) {
							targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage);
						} else {
							targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage,true);
						}
					}
				}
			} else if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {//ALVO HEROI
				//targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
				if (targetEnemy.GetComponent<WPIASoldierControler> ().alive == false) { // ALVO MORREU
					this.targetEnemy = null;
					//lockedTarget = false;
				} else {
					
					if (this.range > 1) {
						TrowArrow ();
					} else {
						if (troopId != 5 && troopId != 6) {
							targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
						} else {
							targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage,true);
						}
					}
				}
			} else if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO TROPA
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
					this.targetEnemy = null;
					//lockedTarget = false;
				} else {
					
					if (this.range > 1) {
						TrowArrow ();
					} else {
						if (troopId != 5 && troopId != 6) {
							targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
						} else {
							targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage,true);
						}
					}
				}
			} else if (targetEnemy.GetComponent<ChargesScript> () != null) {//ALVO BASE
				danoCD = 0;
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				//targetEnemy.GetComponent<ChargesScript> ().progress += 0.25f;

				if (this.range > 1) {
					TrowArrow ();
				} else {
					if (troopId != 5 && troopId != 6) {
						targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (damage);
					} else {
						targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (damage,true);
					}
				}
			} else if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {//ALVO TORRE
				danoCD = 0;
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				//targetEnemy.GetComponent<ChargesScriptTowers> ().progress += 0.25f;


				if (this.range > 1) {
					TrowArrow ();
				} else {
					if (troopId != 5 && troopId != 6) {
						targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (damage);
					} else {
						targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (damage,true);
					}
				}
				
			} else if (targetEnemy == null) {
				this.targetEnemy = null;
			}

			if (this.range > 1 && this.targetEnemy != null) {
				audioManager.PlayAudio ("shot");
			} else if(this.targetEnemy != null) {
				audioManager.PlayAudio ("atack");
			}

			if (troopId == 3) {
				Instantiate (SplashEffect, this.transform.position, Quaternion.identity);
				Destroy (this.gameObject);
			}
		}
	}

	public void ReceiveDamage(float x){

		this.vida -= x;
		//UpdateLife ();
	
		Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);

		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");
		}

	}

	public void ReceiveDamage(float x, bool explosion){

		this.vida -= x;
		//UpdateLife ();
//		if (explosion) {
//			Instantiate (SplashEffect, this.transform.position, Quaternion.identity);
//		} else {
//			Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);
//		}
		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");
		}

	}

	IEnumerator DeathEvent(){
		if (haveAnimation) {
			this.transform.Find("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Death");
			yield return new WaitForSeconds (0.2f);
		} 
		Destroy (this.gameObject);
	}
		

	public void TrowArrow(){
		StartCoroutine (DelayedHitEffect ());
		GameObject alvo = new GameObject ();
		if (targetEnemy != null) {
			if (troopId != 5 && troopId != 2) { //sprite do Projetil
				arrowModel.GetComponent<ArrowScript> ().type = 0;
			} else {
				arrowModel.GetComponent<ArrowScript> ().type = 1;
			}
		
//			if (skillshoter) {
//				Instantiate (alvo, targetEnemy.transform.position, Quaternion.identity);
//				GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity).GetComponent<ArrowScript>().target = alvo;
//			} else {
//				GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity).GetComponent<ArrowScript>().target = targetEnemy;
//			}

			GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity).GetComponent<ArrowScript>().target = targetEnemy;
//		if (targetEnemy != null) {
//		arrow.GetComponent<ArrowScript> ().target = targetEnemy;
	} else {
			//Destroy (arrow);
	}

	}

	IEnumerator DelayedHitEffect(){
		yield return new WaitForSeconds (range/8f);
		if (targetEnemy != null) {
			if (explosiveDamage) {
				Instantiate (SplashEffect, targetEnemy.transform.position, Quaternion.identity);
			} else {
				Instantiate (HitAnimationObject, targetEnemy.transform.position, Quaternion.identity);
			}

			if (targetEnemy.GetComponent<SoldierControler> () != null) {
				if (troopId != 5 && troopId != 6) {
					targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
				} else {
					targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage,true);
				}
			}

			if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {
				if (troopId != 5 && troopId != 6) {
					targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
				} else {
					targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage,true);
				}
			}

			if (targetEnemy.GetComponent<WPSoldierControler> () != null) {
				if (troopId != 5 && troopId != 6) {
					targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage);
				} else {
					targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (damage,true);
				}
			}

			if (targetEnemy.GetComponent<ChargesScript> () != null) {
				if (troopId != 5 && troopId != 6) {
					targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (damage);
				} else {
					targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (damage,true);
				}
			}

			if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {
				if (troopId != 5 && troopId != 6) {
					targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (damage);
				} else {
					targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (damage,true);
				}
			}
		}
	}

	public void ChangeLane(Vector3 pos){
		this.Progress = 1;
		if (Vector3.Distance (pos, LaneTop [1].transform.position) < Vector3.Distance (pos, LaneMid [1].transform.position) && Vector3.Distance (pos, LaneTop [1].transform.position) < Vector3.Distance (pos, LaneBot [1].transform.position)) {
			ActualLane = LaneTop;
		} else if (Vector3.Distance (pos, LaneMid [1].transform.position) < Vector3.Distance (pos, LaneTop [1].transform.position) && Vector3.Distance (pos, LaneMid [1].transform.position) < Vector3.Distance (pos, LaneBot [1].transform.position)) {
			ActualLane = LaneMid;
		} else if (Vector3.Distance (pos, LaneBot [1].transform.position) < Vector3.Distance (pos, LaneMid [1].transform.position) && Vector3.Distance (pos, LaneBot [1].transform.position) < Vector3.Distance (pos, LaneTop [1].transform.position)) {
			ActualLane = LaneBot;
		} else {
			//
		}

		//Debug.Log("Top: " + Vector3.Distance (pos, LaneTop [1].transform.position) + "Mid: " + Vector3.Distance (pos, LaneMid [1].transform.position) + "Bot: " + Vector3.Distance (pos, LaneBot [1].transform.position)); 
	}




	public void ChooseLane(){
//		int x;
//		float random = Random.value;
//		if (random < (topPreference/100))
//		{
//			x = 1;
//		}
//		else if (random < ((botPreference/100) + (topPreference/100)))
//		{
//			x = 2;
//		}
//		else
//		{
//			x = 1;
//		}

		switch (lane) {
		case 1:
			ActualLane = LaneTop;
			LaneName = "top";
			break;
		case 2:
			ActualLane = LaneBot;
			LaneName = "bot";
			break;
		default:
			ActualLane = LaneTop;
			LaneName = "top";
			break;
		}
	}

}
