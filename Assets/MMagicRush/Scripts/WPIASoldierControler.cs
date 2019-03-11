using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WPIASoldierControler : MonoBehaviour {

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

	public int heroID;


	public float topPreference;
	public float midPreference;
	public float botPreference;

	public float idleChance;
	public float gemColectorChance;
	public float cheapUnitySummonChance;
	public float pushHatChance;
	public float cheapMagicCastChance;
	public float itemColectorChance;
	public float heroHarassChance;
	public float retreatChance;
	public float protectHatChance;
	public float heroBattleChance;
	public float randomMovementChance;

	public int twist;
	public bool twisting;
	public bool twisted;

	public WPScript waypoint;
	public GameController gc;

	//	public STATE state = STATE.DEFAULT;

	public int team;

	[HideInInspector]
	public Vector3 touchStartPosition;

	[HideInInspector]
	public Vector3 touchEndPosition;

	//PREFABS

	public SpriteRenderer CharBound;

	//[HideInInspector]
	public GameObject targetEnemy;

	public GameObject healtbarSoldier;

	public HealtBarSolid healtbarSoldierSolid;

	public GameObject specialBar;
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

	public Collider2D ColliderComponent;

	public GameObject SplashEffect;

	//FLAGS

	private bool gameEnded = false;

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	public bool seeking;

	public bool lockedTarget;

	public float cdSeek;

	public float respawningTimer;

	public bool alive;

	public bool explosiveDamage;

	public int shotsCounter;

	//STATUS

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

	public int tempdamage;


	public float damageSpeed;

	[HideInInspector]
	public float range;

	public float danoCD = 0;

	//[HideInInspector]
	public float speed;

	//[HideInInspector]
	public float maxSpeed;

	[HideInInspector]
	public int energyMax;

	private bool tired;

	[HideInInspector]
	public int energy;

	public bool resting;

	private float energyCounter;

	public string effects;
	public float effectsDuration;

	public float captureMarkerTimer;

	private Vector3 previous;
	public float velocity;


	//LANE WAYPOINTS

	public GameObject WaypointMark;

	public GameObject RiverPassLeft;
	public GameObject RiverPassRight;
	public GameObject NearestPass;


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


	// Use this for initialization
	void Start () {

		if (PlayerPrefs.HasKey ("Dificulty")) {
			var x = PlayerPrefs.GetInt ("Dificulty");
			if (x == 1) {
				
			}
			if (x == 2) {
				
			}
			if (x == 3) {
				
			}

		} else {
			
		}
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

		if (heroUnity) {// CONFIGURAÇÃO DE HEROIS
			SetupHero();
		} else {//CONFIGURAÇÃO DE TROPAS
			SetupTroop(heroID);
		}

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
		respawningTimer = 0.5f;
		StartCoroutine (Respawning ());

		this.effects = "default";
		this.speed = speed /1.5f;
		this.damageSpeed = damageSpeed + 1.5f;
		this.maxSpeed = this.speed;
		this.level = 1;
		if (healtbarSoldierSolid != null) {
			this.healtbarSoldierSolid.transform.gameObject.SetActive (true);
			//this.specialBar.SetActive (true);
		}
		//		this.state = STATE.DEFAULT;
		if(heroUnity)
			StartCoroutine (HealingAndXp ());
		
		waypoint = GameObject.Find ("Terreno").GetComponent<WPScript> ();
		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		//audioManager.PlayAudio ("spawn");
		ColliderComponent = GetComponent<BoxCollider2D> ();

		//if(tutorial == false)
		//Twist (0); ///INICIA ESCOLHA DE DECISOES
	}

	void Update () {
		if (healtbarSoldierSolid != null) {
			healtbarSoldierSolid.atualValue = vida;
			healtbarSoldierSolid.maxValue = vidaMax;
		}

		if(tutorial == false)
		if (StaticController.instance.GameController.GameOver == true) {
			this.speed = 0;
			this.targetEnemy = null;
		}


		if (GameObject.Find ("GameController").GetComponent<GameController> ().GameOver) {
			this.alive = false;
		}
		// VELOCIDADE
//		if (previous.x < transform.position.x) {
//			this.anim.GetComponent<SpriteRenderer> ().flipX = false;
//		} else if (previous.x > transform.position.x) {
//			this.anim.GetComponent<SpriteRenderer> ().flipX = true;
//		}

		velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
		previous = transform.position;

//		Debug.Log ("Velocity: "+ velocity);

		if (this.velocity <= 0) {
			anim.SetBool ("Walk", false);
			anim.SetBool ("Idle", true);
		} else if (this.velocity >= 1) {
			anim.SetBool ("Walk", true);
			anim.SetBool ("Idle", false);
		}

		// COLISÕES COM TROPAS E ADVERSÁRIOS
//			foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("CharacterBound")) {
//				if (CharBound.bounds.Intersects (obstacle.GetComponent<SpriteRenderer> ().bounds) && obstacle != this.CharBound.transform.gameObject) {
//					if (this.CharBound.transform.position.x > obstacle.transform.position.x) {
//						transform.Translate (Vector3.right * Time.deltaTime * 0.5f);
//					} else {
//						transform.Translate (Vector3.left * Time.deltaTime * 0.5f);
//					}
//					if (this.CharBound.transform.position.y > obstacle.transform.position.y) {
//						transform.Translate (Vector3.up * Time.deltaTime * 0.5f);
//					} else {
//						transform.Translate (Vector3.down * Time.deltaTime * 0.5f);
//					}
//
//				}
//
//			}

		//ALTERAÇÔES DE CHANCE DE ESCOLHAS NO TWIST

		if (this.vida < (this.vidaMax/2)) {
			retreatChance = 90;
		} else {
			retreatChance = 0;
		}

//		if (GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds >= 100) {
//			cheapMagicCastChance = 90;
//			cheapUnitySummonChance = 90;
//		} else {
//			cheapMagicCastChance = 0;
//			cheapUnitySummonChance = 0;
//		}

//		if (GameObject.Find ("treasureChest").GetComponent<TreasureScript> ().heroProgress > 0) {
//			heroHarassChance = 75;
//		} else 

		if (GameObject.Find ("HeroBaseEnemy").GetComponent<ChargesScript> ().inCombat == true) {
			retreatChance = 90;
		} else if (GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().alive == true && GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().vida <= 20) {
			heroHarassChance = 75;
		} else if (GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().alive == true && GameObject.Find ("Hero").transform.position.y > 0) {
			heroHarassChance = 60;
		} else if (this.vida >= 100) {
			heroHarassChance = 20;
		} else {
			heroHarassChance = 0;
		}

		//heroHarassChance = 100;
//		if (gc.EnemyDiamonds < 100) {
//			gemColectorChance = 15;
//		}

		if (GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().alive == false) {
			pushHatChance = 50;
		} else if (this.vida >= 100) {
			pushHatChance = 25;
		} else if (this.vida >= 50) { 
			pushHatChance = 10;
		}else{
				pushHatChance = 0;
		}

//		if (twisting == false && tutorial == false) {
//			twisting = true;
//			Twist (0);
//		}
		gemColectorChance = 0;

		//EVENTO DE MORTE
		if (this.vida <= 0 && heroUnity && this.GetComponent<SpriteRenderer>().enabled == true) {
			
			this.speed = 0;
			Instantiate (deathAngel, this.transform.position, Quaternion.identity).transform.parent = this.transform;
			if (tutorial == false) {
				GameObject.Find ("RespawnTimerEnemy").GetComponent<RespawnTimer> ().ActiveRespawnTimer (respawningTimer);
				StartCoroutine (Respawning ());
			} else {
				transform.position = new Vector2 (200, 200);
				gameObject.SetActive (false);
			}
			audioManager.PlayAudio ("death");
		} 
//		else if(this.vida <= 0) {
//			this.speed = 0;
//			Instantiate (deathAngel, this.transform.position, Quaternion.identity).transform.parent = this.transform;
//			audioManager.PlayAudio ("death");
//			transform.position = new Vector2 (200, 200);
//			gameObject.SetActive (false);
//			//Destroy (this.gameObject);
//		}
		if (this.vida > this.vidaMax) {
			this.vida = this.vidaMax;
		}

		//	
		//FLAGS PARA HEALING
		//
		if (this.team == 1 && this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
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

		if (this.team == 2 && this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
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
//				LvlUpAnim.SetInteger ("LevelUpInt", -1);
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
//				LvlUpAnim.SetInteger ("LevelUpInt", 1);
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
//				LvlUpAnim.SetInteger ("LevelUpInt", 2);
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

		//Debug.Log ("xp: " + xp / xpMax);

		//
		//COOLDOWN DE EFEITOS
		//
		if(effects != "default"){
			effectsDuration += Time.deltaTime;
			if (effectsDuration > 3) {
				Removeeffect ();
				effectsDuration = 0;
				effects = "default";
			}
		}

		if(Vector3.Distance (transform.position, RiverPassLeft.transform.position)>Vector3.Distance (transform.position, RiverPassRight.transform.position)){
			NearestPass = RiverPassRight;
		}else{
			NearestPass = RiverPassLeft;
		}

		//
		//ESTADOS DO PERSONAGEM
		//

		//SEGUINDO OS MARCADORES DE MOVIMENTO

//		if (GetNewWaypoint () != null) {
//			seeking = false;
//			WaypointMark = GetNewWaypoint ();
//			targetEnemy = null;
//			if (Vector3.Distance (transform.position, WaypointMark.transform.position) > 0.2f) {
//				anim.SetTrigger ("Walk");
//				transform.position = Vector3.MoveTowards (transform.position, WaypointMark.transform.position, Time.deltaTime * speed);
//			} else {
//				Destroy (WaypointMark.gameObject);
//				twisting = false;
//				WaypointMark = GetNewWaypoint ();
//			}
		if (GetNewWaypoint () != null && multiplayer == false) {
			lockedTarget = false;
			seeking = false;
			WaypointMark = GetNewWaypoint ();
			//targetEnemy = null;
			if (Vector3.Distance (transform.position, WaypointMark.transform.position) > 0.2f) {
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
						twisting = false;
						WaypointMark = GetNewWaypoint ();

					} else {
						captureMarkerTimer += Time.deltaTime;
					}
				} else {
					Destroy (WaypointMark.gameObject);
					twisting = false;
					WaypointMark = GetNewWaypoint ();

				}
			}

		} else {
			seeking = true;
		} 

		//			if (this.state == STATE.SEEKING) { // BUSCANDO ALVO
		//				targetEnemy = SeekEnemyTarget ();
		//				if (targetEnemy != null) {
		//					this.state = STATE.DEFAULT;
		//				}
		//			}

				if (seeking && cdSeek >= 0.01f) {
					if (lockedTarget == false) {
						this.targetEnemy = SeekEnemyTarget ();
					}
					//this.targetEnemy = SeekEnemyTarget ();
					if (this.targetEnemy != null) {
						seeking = false;
					}
					cdSeek = 0;
				} else {
					cdSeek += Time.deltaTime;
				}



		// PERSEGUINDO E ATACANDO ALVO ENCONTRADO

		if (seeking == false && this.targetEnemy != null ) { 

			foreach (GameObject o in GameObject.FindGameObjectsWithTag("enemywaypoint")) {
				//Destroy (o.gameObject);
			}

			if (this.team == 2 && this.vida <= 2) {
//				if (TryTwist() == false) {
//					Twist (5);
//				}
			}

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
				//anim.SetTrigger ("Walk");
				//SpendingEnergy ();
				transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);

			} else if (targetEnemy != null && this.anim.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO
					if (targetEnemy.transform.position.x < transform.position.x) {
						anim.GetComponent<SpriteRenderer> ().flipX = true;
					} else if (targetEnemy.transform.position.x > transform.position.x) {
						anim.GetComponent<SpriteRenderer> ().flipX = false;
					}

					if (targetEnemy.transform.Find ("MovementMarker(Clone)"))
						Destroy (targetEnemy.transform.Find ("MovementMarker(Clone)").gameObject);
				
					if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
						//danoCD = 0;
						anim.SetTrigger ("Attack");
						StartCoroutine (DealDamage ());
						
					} else {
//					if (tutorial) {
//						danoCD += Time.deltaTime * 20;
//					} else {
//						danoCD += Time.deltaTime * 10;
//					}
					}

			} 
		} else if(this.targetEnemy == null)  {
			//Twist (0);
			seeking = true;
		}

		danoCD += Time.deltaTime;
		//EVENTOS DE IA

//		if (Vector2.Distance (GameObject.Find ("HeroBaseEnemy").transform.position, transform.position) <= 0.3f) {
//			if (GameObject.Find ("HeroBaseEnemy").GetComponent<ChargesScript> ().inCombat == false) {
//				if (TryTwist () == false) {
//					Twist (1);
//				}
//			}
//		}
//
//		if (Vector2.Distance (GameObject.Find ("HeroBase").transform.position, transform.position) <= 0.3f) {
//			if (TryTwist() == false) {
//				Twist (1);
//			}
//		}



	}

	public void SetupHero(){
		//CONFIGURAÇÃO DE TIPO DE HEROI
		heroID = 1;

		if (PlayerPrefs.HasKey ("SelectedCharacter") && this.team == 1 && tutorial == false) {
			heroID =	PlayerPrefs.GetInt ("SelectedCharacter");
		} else if (PlayerPrefs.HasKey ("Enemy") && this.team != 1 && tutorial == false) {
			heroID = PlayerPrefs.GetInt ("Enemy");
		} else if (tutorial == true){
			heroID = 0;
		}

		if(tutorial == false)
		GameObject.Find ("EnemyPortrait").GetComponent<Image> ().sprite = heroPortraits [heroID];


//		Debug.Log ("id: " + id);
		switch (heroID) {
		case(0): 
			this.vidaMax = 200; //ALto
			this.vida = 200;
			this.reach = 2;//3
			this.damage = 50; //Alto
			this.damageSpeed = 0.5f;//Baixo
			this.range = 1.5f; //Medio
			this.speed = 0.8f; //Baixo
			this.energyMax = 3;
			this.energy = 3;
			this.explosiveDamage = true;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim.SetInteger ("Char", 0);
			this.anim = transform.Find ("MonicaAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			if (team == 1)
				GameObject.Find ("HeroPortrait").GetComponent<Image> ().sprite = heroPortraits [0];
			Debug.Log ("Monica");
			break;
		case(1):
			this.vidaMax = 75; //Medio
			this.vida = 75;
			this.reach = 2;//
			this.damage = 14; //Medio
			this.damageSpeed = 0.5f; //Alto
			this.range = 0.5f;//Baixissimo
			this.speed = 1.7f; //Alto
			this.energyMax = 3;
			this.energy = 3;
			this.explosiveDamage = false;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim.SetInteger ("Char", 1);
			this.anim = transform.Find ("CebolinhaAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			if (team == 1)
				GameObject.Find ("HeroPortrait").GetComponent<Image> ().sprite = heroPortraits [1];
			Debug.Log ("Cebolinha");
			break;
		case(2):
			this.vidaMax = 75; //Medio 75
			this.vida = 75;
			this.reach = 2f;
			this.damage = 14;  //Baixo
			this.damageSpeed = 1; //Medio
			this.range = 0.5f; //Baixissimo
			this.speed = 1.3f; //medio
			this.energyMax = 4;
			this.energy = 4;
			this.explosiveDamage = false;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("MagaliAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			Debug.Log ("Magali");
			break;
		case(3):
			this.vidaMax = 75; //Medio
			this.vida = 75;
			this.reach = 2f;
			this.damage = 35;  //Baixo
			this.damageSpeed = 1f; //Medio
			this.range = 0.5f; //Baixissimo
			this.speed = 1.7f; //Alto
			this.energyMax = 4;
			this.energy = 4;
			this.explosiveDamage = false;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim = transform.Find ("CascãoAnimation").GetComponent<Animator> (); // SET THE ANIMATOR
			this.anim.GetComponent<SpriteRenderer>().enabled = true;
			break;
		default:
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 2f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 1.5f;
			this.speed = 10;
			this.energyMax = 4;
			this.energy = 4;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			Debug.Log ("Monica");
			break;

		}

		//Configuração de Cor do Inimigo

		if (this.team != 1) {
			string ter = PlayerPrefs.GetString ("TerrainType");
			if (ter == "Forest") {
				Debug.Log ("Coloriu");
				this.anim.GetComponent<SpriteRenderer> ().color = Color.green;
			} else if (ter == "Winter") {
				this.anim.GetComponent<SpriteRenderer> ().color = Color.cyan;
			} else if (ter == "Dungeon") {
				this.anim.GetComponent<SpriteRenderer> ().color = Color.gray;
			}
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

	public void SetupTroop(int id){

		switch (id) {
		case(1): // BIDU
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1.5f;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites[0];
			break;
		case(2): // ASTRONAUTA
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 4;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 4;
			this.speed = 5;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [1];
			this.GetComponent<SpriteRenderer> ().flipX = true;
			break;
		case(3): //ANJINHO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 3;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 3;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [2];
			break;
		case(4): //JOTALHÃO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1.5f;
			this.speed = 2;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [3];
			break;
		case(5): //PITECO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1.5f;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [4];
			break;
		case(6): //PENADINHO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 5;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 5;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [5];
			this.GetComponent<SpriteRenderer> ().flipX = true;
			break;
		case(7): //MAURICIO
			this.vidaMax = 10;
			this.vida = 10;
			this.reach = 0.5f;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 1.5f;
			this.speed = 3;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [6];
			break;
		case(8): //SANSAO
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 2;
			this.damage = 3;
			this.damageSpeed = 3;
			this.range = 1.5f;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [7];
			break;
		case(9): //MINGAU
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 6;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 6;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [8];
			break;
		case(10): //ALFREDO
			this.vidaMax = 12;
			this.vida = 12;
			this.reach = 8;
			this.damage = 20;
			this.damageSpeed = 3;
			this.range = 8;
			this.speed = 5;
			this.energyMax = 1;
			this.energy = 200;
			this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [9];
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


	public void ChangeState(){

		//		if (this.state == STATE.RETREAT) {
		//			this.state = STATE.DEFAULT;
		//		}else if (this.state == STATE.DEFAULT) {
		//			this.state = STATE.RETREAT;
		//		}

	}

	public void UpdateLife(){
		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarSoldier.GetComponent<HealtBar> ().MaxLife = this.vidaMax;
		this.healtbarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
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
		if (effect == "sight") {
			this.range += 2;
			this.reach += 2;
		}
		if (effect == "slow") {
			GameObject.Find ("FrozenDamage").GetComponent<Animator> ().SetTrigger ("Frozen");
			this.anim.GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 1f);
			this.speed = speed / 2;
		}
		if (effect == "extraSlow") {
			this.speed = speed / 4;
		}
		if (effect == "speed") {
			this.speed = speed + 0.1f;
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
			this.anim.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f);
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

	IEnumerator Respawning(){
		
		yield return new WaitForSeconds (0.01f);
		this.alive = false;
		this.ColliderComponent.enabled = false;
		this.GetComponent<SpriteRenderer> ().enabled = false;
		this.anim.GetComponent<SpriteRenderer> ().enabled = false;
		this.platform.GetComponent<SpriteRenderer> ().enabled = false;
		if (healtbarSoldierSolid != null) {
			this.healtbarSoldierSolid.transform.gameObject.SetActive (false);
			//this.specialBar.SetActive (false);
		}
		//this.energybarSoldier.SetActive (false);
		this.skill1.gameObject.SetActive (false);
		this.skill2.gameObject.SetActive (false);
		this.vida = this.vidaMax;
//		UpdateLife ();
//		if(heroUnity)
//			this.energy = this.energyMax;
		this.seeking = false;
		this.transform.position = new Vector3(0,7.5f,0);

		yield return new WaitForSeconds (respawningTimer);
		audioManager.PlayAudio ("spawn");
		this.alive = true;
		if(heroUnity)
			//transform.position = heroBase.transform.position;
		this.ColliderComponent.enabled = true;
		this.anim.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponent<SpriteRenderer> ().enabled = true;
		this.platform.GetComponent<SpriteRenderer> ().enabled = true;
		if (healtbarSoldierSolid != null) {
			this.healtbarSoldierSolid.transform.gameObject.SetActive (true);
			//this.specialBar.SetActive (true);
		}
//		UpdateLife ();
//		if (heroUnity)
//			this.energybarSoldier.SetActive (true);
		this.targetEnemy = null;
		this.skill1.gameObject.SetActive (true);
		this.skill2.gameObject.SetActive (true);
		this.speed = maxSpeed;
		this.seeking = true;
		this.targetEnemy = SeekEnemyTarget();
		respawningTimer = 12;
		twisting = false;
		Twist (0);
		Arrival.Play ();
		yield return new WaitForSeconds (0.2f);
		this.transform.position = new Vector3(0,3.5f,0);
	}

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;


		//Procura de Jogador 1

		if (this.tag == "enemysoldier1") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {//PROCURA TROPA
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
				if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {//PROCURA TROPA
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

		if (this.heroID == 0 && shotsCounter == 2) {
			tempdamage = this.damage * 2;
			shotsCounter = 0;
		} 

		if (this.heroID == 1) {
			GetComponent<HeroSpecialHability> ().LostInvisibility ();
		} 
		
		yield return new WaitForSeconds (0.5f);
		if (targetEnemy != null && danoCD > damageSpeed) {
			if (targetEnemy.GetComponent<WPSoldierControler> () != null && danoCD > damageSpeed) {//ALVO HEROI
				danoCD = 0;
				//targetEnemy.GetComponent<WPSoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<WPSoldierControler> ().UpdateLife ();
				if (targetEnemy.GetComponent<WPSoldierControler> ().alive == false) { // ALVO MORREU
					this.targetEnemy = null;
					lockedTarget = false;
				}else{

					if (this.range > 1) {
						TrowArrow ();
					} else {
						targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (tempdamage);
					}
				}
			} else if (targetEnemy.GetComponent<SoldierControler> () != null && danoCD > damageSpeed) {//ALVO TROPA
				danoCD = 0;
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
					this.targetEnemy = null;
					lockedTarget = false;
				}else{
					
					if (this.range > 1) {
						TrowArrow ();
					} else {
						targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (tempdamage);
					}
				}
			} else if (targetEnemy.GetComponent<ChargesScript> () != null && danoCD > damageSpeed) {//ALVO BASE
				danoCD = 0;
					//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
					//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				if (this.range > 1) {
					TrowArrow ();
				} else {
					targetEnemy.GetComponent<ChargesScript> ().progress += 0.25f;
					targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (tempdamage);
				}
			}else if (targetEnemy.GetComponent<ChargesScriptTowers> () != null && danoCD > damageSpeed) {//ALVO TORRE
				danoCD = 0;
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				if (this.range > 1) {
					TrowArrow ();
				} else {
					targetEnemy.GetComponent<ChargesScriptTowers> ().progress += 0.5f;
					targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (tempdamage);
				}
			}

			shotsCounter += 1;

			if (this.range > 1) {
				audioManager.PlayAudio ("shot");
			} else {
				audioManager.PlayAudio ("atack");
			}
		}
	}

	public void ReceiveDamage(int x){
		if (this.heroID == 1) {
			GetComponent<HeroSpecialHability> ().LostInvisibility ();
		} 
		this.vida -= x;
		UpdateLife ();
		
		//Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);

		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");
		}

	}

	public void ReceiveDamage(int x, bool explosion){
		if (this.heroID == 1) {
			GetComponent<HeroSpecialHability> ().LostInvisibility ();
		} 
		this.vida -= x;
		//UpdateLife ();
//		if (explosion) {
//			Debug.Log ("BOOOOM");
//			Instantiate (SplashEffect, this.transform.position, Quaternion.identity);
//		} else {
//			Instantiate (HitAnimationObject, this.transform.position, Quaternion.identity);
//		}
		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");
		}

	}


	public void TrowArrow(){
		StartCoroutine (DelayedHitEffect ());
		if (targetEnemy != null) {
			arrowModel.GetComponent<ArrowScript> ().type = 0;
			arrowModel.GetComponent<ArrowScript> ().target = targetEnemy;
			GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		} else {

		}

	}

	IEnumerator DelayedHitEffect(){
		yield return new WaitForSeconds (range/7f);
		if (targetEnemy != null) {
			if (explosiveDamage) {
				Instantiate (SplashEffect, targetEnemy.transform.position, Quaternion.identity);
			} else {
				Instantiate (HitAnimationObject, targetEnemy.transform.position, Quaternion.identity);
			}		

			if (targetEnemy.GetComponent<SoldierControler> () != null) {
				targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (tempdamage);
			}

			if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {
				targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (tempdamage);
			}

			if (targetEnemy.GetComponent<WPSoldierControler> () != null) {
				targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage (tempdamage);
			}

			if (targetEnemy.GetComponent<ChargesScript> () != null) {
				targetEnemy.GetComponent<ChargesScript> ().ReceiveDamage (tempdamage);
			}

			if (targetEnemy.GetComponent<ChargesScriptTowers> () != null) {
				targetEnemy.GetComponent<ChargesScriptTowers> ().ReceiveDamage (tempdamage);
			}



		}
	}

	public GameObject GetNewWaypoint(){
		GameObject[] wps = GameObject.FindGameObjectsWithTag ("enemywaypoint");
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

	IEnumerator WaitForTwist(){ // espera 3 segundos e chama um novo twist
//		yield return new WaitForSeconds (3);
//		Twist (0);
		yield return new WaitUntil (() => twisted == true);
		twisted = false;
		Twist (0);

	}

	public void Twist(int x){ // aplica efeito do twist

		StartCoroutine (WaitForTwist ());

		int chooser = 0;
		if (x == 0) {
			chooser = TwistIntChooser ();
			//chooser = Random.Range(1,11);
		} else {
			chooser = x;
		}
		Debug.Log ("Escolha: "+chooser);
	
			switch(chooser) {
		case 1://IDLE
			StartCoroutine (WaitForTwist ());
			break;
		case 2://MAGIC
			if (GameObject.Find ("HeroBaseEnemy").GetComponent<EnemyHand> ().ActivateCardEffect ("Magic")) {
				cheapMagicCastChance -= 10;
			}
			//twisting = false;
			twisted = true;

			break;
		case 3://UNIT
			if (GameObject.Find ("HeroBaseEnemy").GetComponent<EnemyHand> ().ActivateCardEffect ("Unit")) {
				cheapUnitySummonChance -= 10;
			}
			//twisting = false;
			twisted = true;

			break;
		case 4://GEM COLECTOR
			if (GetEnabledGem() != null) {
				waypoint.EnemyMovementPlacement (GetEnabledGem().transform.position);
				StartCoroutine(WaitMethod(7));
			} else {
				//StartCoroutine (WaitForTwist ());
				twisted = true;
			} 
			break;
		case 5://HERO HARASS
			if (GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().alive == true) {
				if (Vector2.Distance (GameObject.Find ("Hero").transform.position, transform.position) > 1f) {
					waypoint.EnemyMovementPlacement (new Vector2(GameObject.Find ("Hero").transform.position.x,GameObject.Find ("Hero").transform.position.y));
					//waypoint.EnemyMovementPlacement (GameObject.Find ("Hero").transform.position);
					targetEnemy = GameObject.Find ("Hero");
					StartCoroutine (WaitMethod (3));
				} else {
					targetEnemy = GameObject.Find ("Hero");
					StartCoroutine( WaitMethod (2));
				} 
			} else {
				twisted = true;
			}

			break;
		case 6://ITEM COLECTOR
			if (GetEnabledGem() != null) {
				waypoint.EnemyMovementPlacement (GetEnabledGem().transform.position);
				StartCoroutine(WaitMethod(7));
			} else {
				//StartCoroutine (WaitForTwist ());
				twisted = true;
			}  
			break;
		case 7://PROTECT HAT
			if (Vector2.Distance (GameObject.Find ("HeroBaseEnemy").transform.position, transform.position) > 1f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("HeroBaseEnemy").transform.position);
				targetEnemy = GameObject.Find ("Hero");
				StartCoroutine (WaitMethod (3));
			} else {
				twisted = true;
			}  
			break;
		case 8://PUSH HAT 
			if (Random.value > 0.5f) {
				Debug.Log ("Ataca Base");
				if (Vector2.Distance (GameObject.Find ("HeroBase").transform.position, transform.position) > 0.5f) {
					waypoint.EnemyMovementPlacement (new Vector2(GameObject.Find ("HeroBase").transform.position.x,GameObject.Find ("HeroBase").transform.position.y ));
					targetEnemy = GameObject.Find ("HeroBase");
					StartCoroutine (WaitMethod (7));
				} else {
					//StartCoroutine (WaitForTwist ());
					targetEnemy = GameObject.Find ("HeroBase");
					StartCoroutine (WaitMethod (3));
				}  
			} else {
				Debug.Log ("Ataca Torre");
				if (Random.value > 0.5f) {
					Debug.Log ("Direita");
					if (GameObject.Find ("HeroTower1") != null) {
						if (Vector2.Distance (GameObject.Find ("HeroTower1").transform.position, transform.position) > 0.5f) {
							waypoint.EnemyMovementPlacement (new Vector2(GameObject.Find ("HeroTower1").transform.position.x,GameObject.Find ("HeroTower1").transform.position.y));
							targetEnemy = GameObject.Find ("HeroTower1");
							StartCoroutine (WaitMethod (7));
						} else {
							//StartCoroutine (WaitForTwist ());
							targetEnemy = GameObject.Find ("HeroTower1");
							StartCoroutine (WaitMethod (3));
						}  
					} else {
						StartCoroutine (WaitMethod (0));
					}
				} else {
					Debug.Log ("Esquerda");
					if (GameObject.Find ("HeroTower2") != null) {
						if (Vector2.Distance (GameObject.Find ("HeroTower2").transform.position, transform.position) > 0.5f) {
							waypoint.EnemyMovementPlacement (new Vector2(GameObject.Find ("HeroTower2").transform.position.x,GameObject.Find ("HeroTower2").transform.position.y ));
							targetEnemy = GameObject.Find ("HeroTower2");
							StartCoroutine (WaitMethod (7));
						} else {
							//StartCoroutine (WaitForTwist ());
							targetEnemy = GameObject.Find ("HeroTower2");
							StartCoroutine (WaitMethod (3));
						} 
					}else {
						StartCoroutine (WaitMethod (0));
					}
				}
			}
			break;
		case 9://RETREAT
			if (Vector2.Distance (GameObject.Find ("HeroBaseEnemy").transform.position, transform.position) > 1f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("HeroBaseEnemy").transform.position);
				StartCoroutine (WaitMethod (3));
				pushHatChance += 10;
			} else {
				twisted = true;
			}  

			break;
		case 10://HERO BATTLE
			if (GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().alive == true) {
				if (Vector2.Distance (GameObject.Find ("Hero").transform.position, transform.position) > 1f) {
					waypoint.EnemyMovementPlacement (GameObject.Find ("Hero").transform.position);
					targetEnemy = GameObject.Find ("Hero");
					StartCoroutine( WaitMethod (2));
				} else {
					targetEnemy = GameObject.Find ("Hero");
					StartCoroutine( WaitMethod (2));
				} 
			} else {
				twisted = true;
				//StartCoroutine (WaitForTwist ());
			}
			break;
		case 11://RANDON MOVEMENT
			//gemColectorChance += 10;
			heroHarassChance += 10;
			pushHatChance += 10;
			twisted = true;
			//StartCoroutine (WaitForTwist ());
			break;
		default:
			twisted = true;
			//Twist (1);
			break;
		}

	}

//	IEnumerator WaitMethodCrossingRiver(int x){
//		yield return new WaitForSeconds (x);
//		twisted = true;
//		//StartCoroutine (WaitForTwist ());
//	}

	IEnumerator WaitMethod(int x){
		yield return new WaitForSeconds (x);
		twisted = true;
		//StartCoroutine (WaitForTwist ());
	}

	public int TwistIntChooser(){ // escolhe o twist a ser executado;
		float[] probs = new float[12];

		probs [0] = 0;
		probs [1] = idleChance;
		probs [2] = cheapMagicCastChance;
		probs [3] = cheapUnitySummonChance;
		probs [4] = gemColectorChance;
		probs [5] = heroHarassChance;
		probs [6] = itemColectorChance;
		probs [7] = protectHatChance;
		probs [8] = pushHatChance;
		probs [9] = retreatChance;
		probs [10] = heroBattleChance;
		probs [11] = randomMovementChance;

		return (int)Choose(probs);

	}

	float Choose (float[] probs) {

		float total = 0;

		foreach (float elem in probs) {
			total += elem;
		}

		float randomPoint = Random.value * total;

		for (int i= 0; i < probs.Length; i++) {
			if (randomPoint < probs[i]) {
				return i;
			}
			else {
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
		
	public GameObject GetEnabledGem(){
		List<GameObject> enablegems = new List<GameObject> ();
		if (GameObject.FindGameObjectsWithTag ("enemygem").Length > 0) {
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemygem")) {
				if (obj.GetComponent<GemScript> ().enabled == true) {
					enablegems.Add (obj);
				}
			}
			if (enablegems.Count > 0) {
				//enablegems.Sort ();
				return enablegems [Random.Range(0,enablegems.Count)];
			} else {
				return null;
			}
		} else {
			return null;
		}
		return null;
	}

}

