using UnityEngine;
using System.Collections;
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

	public bool heroUnity;

	public int troopId;

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

	public WPScript waypoint;

	//	public STATE state = STATE.DEFAULT;

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

	public Sprite[] tropasSprites;

	public GameObject FlashingEffects;

	public GameObject platform;

	public GameObject heroBase;

	public Animator anim;

	public AudioManager audioManager;

	public GameObject deathAngel;

	public Animator XpBar;

	public Animator LvlUpAnim;

	//FLAGS

	private bool gameEnded = false;

	public bool inCombat;

	public bool healing;

	public bool GainXP;

	public bool seeking;

	public float cdSeek;

	public float respawningTimer;

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

	[HideInInspector]
	public int damage;

	[HideInInspector]
	public float damageSpeed;

	[HideInInspector]
	public float range;

	private float danoCD = 0;

	[HideInInspector]
	public float speed;

	[HideInInspector]
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


	//LANE WAYPOINTS

	public GameObject WaypointMark;


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



		if (heroUnity) {// CONFIGURAÇÃO DE HEROIS
			SetupHero();
		} else {//CONFIGURAÇÃO DE TROPAS
			SetupTroop(troopId);
		}

		//CONFIGURAÇÃO EM COMUM 
		UpdateLife ();
		this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();

		UpdateEnergy ();
		this.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		this.energybarSoldier.GetComponent<HealtBar> ().energy = true;

		if(heroUnity)
		if (team == 1) {
			this.xp = GameController.playerXp;
		} else {
			this.xp = GameController.enemyXp;
		}

		if (GetComponent<Animator> () != null) {
			anim = GetComponent<Animator> ();
		}
		respawningTimer = 0.5f;
		StartCoroutine (Respawning ());

		this.effects = "default";
		this.speed = speed / 15;
		this.maxSpeed = this.speed;
		this.level = 1;
		this.healtbarSoldier.SetActive (true);
		//		this.state = STATE.DEFAULT;
		if(heroUnity)
			StartCoroutine (HealingAndXp ());
		
		waypoint = GameObject.Find ("Terreno").GetComponent<WPScript> ();
		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		audioManager.PlayAudio ("spawn");

		Twist (0); ///INICIA ESCOLHA DE DECISOES
	}

	void Update () {

		//ALTERAÇÔES DE CHANCE DE ESCOLHAS NO TWIST

		if (this.vida <= 1) {
			retreatChance = 90;
		} else {
			retreatChance = 0;
		}

		if (GameObject.Find ("GameController").GetComponent<GameController> ().EnemyDiamonds >= 10) {
			cheapMagicCastChance = 90;
			cheapUnitySummonChance = 90;
		} else {
			cheapMagicCastChance = 0;
			cheapUnitySummonChance = 0;
		}

		if (GameObject.Find ("treasureChest").GetComponent<TreasureScript> ().heroProgress > 0) {
			heroHarassChance = 75;
		} else if (GameObject.Find ("HeroBaseEnemy").GetComponent<ChargesScript> ().inCombat == true) {
			heroHarassChance = 90;
		} else if (GameObject.Find ("Hero").GetComponent<SpriteRenderer> ().enabled == true && GameObject.Find ("Hero").GetComponent<WPSoldierControler> ().vida == 1) {
			heroHarassChance = 75;
		} else if (GameObject.Find ("Hero").GetComponent<SpriteRenderer> ().enabled == true && GameObject.Find ("Hero").transform.position.y > 0) {
			heroHarassChance = 60;
		} else {
			heroHarassChance = 0;
		}

		if (GameObject.Find ("Hero").GetComponent<SpriteRenderer> ().enabled == false) {
			pushHatChance = 95;
		} else {
			pushHatChance = 0;
		}

		if (twisting == false) {
			twisting = true;
			Twist (0);
		}
	

		//EVENTO DE MORTE
		if (this.vida <= 0 && heroUnity) {
			this.speed = 0;
			Instantiate (deathAngel, this.transform.position, Quaternion.identity).transform.parent = this.transform;
			StartCoroutine (Respawning ());
			audioManager.PlayAudio ("death");
		} else if(this.vida <= 0) {
			audioManager.PlayAudio ("death");
			Destroy (this.gameObject);
		}
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

		//Debug.Log ("xp: " + xp / xpMax);

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

		if (GetNewWaypoint () != null) {
			seeking = false;
			WaypointMark = GetNewWaypoint ();
			targetEnemy = null;
			if (Vector3.Distance (transform.position, WaypointMark.transform.position) > 0.2f) {
				anim.SetTrigger ("Walk");
				transform.position = Vector3.MoveTowards (transform.position, WaypointMark.transform.position, Time.deltaTime * speed);
			} else {
				Destroy (WaypointMark.gameObject);
				twisting = false;
				WaypointMark = GetNewWaypoint ();
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

				if (seeking && cdSeek >= 1) {
					this.targetEnemy = SeekEnemyTarget ();
					if (this.targetEnemy != false) {
						seeking = false;
					}
					cdSeek = 0;
				} else {
					cdSeek += Time.deltaTime;
				}



		// PERSEGUINDO E ATACANDO ALVO ENCONTRADO

		if(seeking == false && this.targetEnemy != null ) { 

			if (this.team == 2 && this.vida <= 2) {
//				if (TryTwist() == false) {
//					Twist (5);
//				}
			}

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
				anim.SetTrigger ("Walk");
				//SpendingEnergy ();
				transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);

			} else if(targetEnemy != null && this.GetComponent<SpriteRenderer>().enabled == true) { //ATACA ALVO
				
				if (danoCD > damageSpeed ) { //TEMPO ENTRE ATAQUES
					anim.SetTrigger ("Attack");
					if (targetEnemy.GetComponent<WPSoldierControler> () != null) {//ALVO HEROI
						targetEnemy.GetComponent<WPSoldierControler> ().vida -= damage;
						targetEnemy.GetComponent<WPSoldierControler> ().UpdateLife();
						targetEnemy.GetComponent<WPSoldierControler> ().ReceiveDamage ();
						if(this.range>1)
							TrowArrow ();
						if (targetEnemy.GetComponent<WPSoldierControler> ().vida <= -1) // ALVO MORREU
							this.targetEnemy = null;
					}else if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO TROPA
						targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
						targetEnemy.GetComponent<SoldierControler> ().UpdateLife();
						targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage ();
						if(this.range>1)
							TrowArrow ();
						if (targetEnemy.GetComponent<SoldierControler> ().vida <= -1) // ALVO MORREU
							this.targetEnemy = null;
					} else {//ALVO BASE
						//							if(targetEnemy.tag == "waypoint"){
						//								if (Progress == 2) {
						//									if (heroUnity) {
						//										heroBase.GetComponent<ChargesScript> ().charges++;
						//										GameObject.Find("GameController").GetComponent<GameController>().NextRound ();
						//										//StartCoroutine (Respawning ());
						//									}
						//								} else {
						//									Progress++;
						//									targetEnemy = null;
						//								}
						//							}
						if (heroUnity) {
							//								heroBase.GetComponent<ChargesScript> ().charges += 1;
							//								StartCoroutine (Respawning ());
							//								GameObject.Find("GameController").GetComponent<GameController>().NextRound ();

						}
					}
					danoCD = 0;
					if (this.range > 1) {
						audioManager.PlayAudio ("shot");
					} else {
						audioManager.PlayAudio ("atack");
					}
				} else {
					danoCD += Time.deltaTime * 1000;
				}
			} 
		}

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
		int id;
		if (PlayerPrefs.GetInt ("SelectedCharacter") != null && this.team == 1) {
			id =	PlayerPrefs.GetInt ("SelectedCharacter");
		} else {
			id = 1;
		}
		Debug.Log ("id: " + id);
		switch (id) {
		case(0): 
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 2;//3
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 8;
			this.energyMax = 3;
			this.energy = 3;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim.SetInteger ("Char", 0);
			Debug.Log ("Monica");
			break;
		case(1):
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 2;//3
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 8;
			this.energyMax = 3;
			this.energy = 3;
			//this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.anim.SetInteger ("Char", 1);
			Debug.Log ("Cebolinha");
			break;
		case(2):
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 8;
			this.energyMax = 4;
			this.energy = 4;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			Debug.Log ("Magali");
			break;
		case(3):
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 8;
			this.energyMax = 4;
			this.energy = 4;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			break;
		default:
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 0.5f;
			this.damage = 1;
			this.damageSpeed = 2;
			this.range = 2;
			this.speed = 8;
			this.energyMax = 4;
			this.energy = 4;
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
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

	public void SetupTroop(int id){

		switch (id) {
		case(1): // BIDU
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2;
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
			this.range = 2;
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
			this.range = 2;
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
			this.range = 2;
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
			this.range = 2;
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
			platform.GetComponent<SpriteRenderer> ().color = Color.red;

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
			//this.speed = maxSpeed;
			tired = false;
		}

		if (this.energy <= 0) {
			resting = true;

			if (tired == false) {
				tired = true;
				//this.speed = speed / 2;
			}
		}

	}

	public void ReceiveEffect(string effect){
		effects = effect;
		if (effect == "sight") {
			this.range += 2;
			this.reach += 2;
		}
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
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.platform.GetComponent<SpriteRenderer> ().enabled = false;
		this.healtbarSoldier.SetActive (false);
		this.energybarSoldier.SetActive (false);
		this.skill1.gameObject.SetActive (false);
		this.skill2.gameObject.SetActive (false);
		this.vida = this.vidaMax;
		UpdateLife ();
		if(heroUnity)
			this.energy = this.energyMax;
		this.seeking = false;
		yield return new WaitForSeconds (respawningTimer);

		if(heroUnity)
			transform.position = heroBase.transform.position;
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		this.platform.GetComponent<SpriteRenderer> ().enabled = true;
		this.healtbarSoldier.SetActive (true);
		if (heroUnity)
			this.energybarSoldier.SetActive (true);
		this.skill1.gameObject.SetActive (true);
		this.skill2.gameObject.SetActive (true);
		this.damage = 1;
		this.seeking = true;
		this.targetEnemy = SeekEnemyTarget();
		respawningTimer = 7;
		twisting = false;
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
								}
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

		}
		return Emin;
	}

	public void ReceiveDamage(){

		if (this.team == 1 && heroUnity == true) {
			FlashingEffects.GetComponent<Animator> ().SetTrigger ("Flash");
		}

	}


	public void TrowArrow(){

		GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (targetEnemy != null) {
			arrow.GetComponent<ArrowScript> ().target = targetEnemy;
		} else {
			Destroy (arrow);
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
		yield return new WaitForSeconds (3);
		Twist (0);
	}

	public void Twist(int x){ // aplica efeito do twist
		
		int chooser = 0;
		if (x == 0) {
			chooser = TwistIntChooser ();
			//chooser = Random.Range(1,11);
		} else {
			chooser = x;
		}
		Debug.Log (chooser);
			switch(chooser) {
		case 1://IDLE
			StartCoroutine (WaitForTwist ());
			break;
		case 2://MAGIC
			if (GameObject.Find ("HeroBaseEnemy").GetComponent<EnemyHand> ().ActivateCardEffect ("Magic")) {
				cheapMagicCastChance -= 10;
			}
			twisting = false;

			break;
		case 3://UNIT
			if (GameObject.Find ("HeroBaseEnemy").GetComponent<EnemyHand> ().ActivateCardEffect ("Unit")) {
				cheapUnitySummonChance -= 10;
			}
			twisting = false;

			break;
		case 4://GEM COLECTOR
			if (GameObject.FindGameObjectsWithTag ("enemygem").Length > 0) {
				waypoint.EnemyMovementPlacement (GameObject.FindGameObjectsWithTag ("enemygem") [0].transform.position);

			} else {
				StartCoroutine (WaitForTwist ());
			} 
			break;
		case 5://HERO HARASS
			if (Vector2.Distance(GameObject.Find ("Hero").transform.position,transform.position) > 0.3f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("Hero").transform.position);
			}  else {
				StartCoroutine (WaitForTwist ());
			} 
			break;
		case 6://ITEM COLECTOR
			if (GameObject.FindGameObjectsWithTag ("enemygem").Length > 0) {
				waypoint.EnemyMovementPlacement (GameObject.FindGameObjectsWithTag ("enemygem") [0].transform.position);
			} else {
				StartCoroutine (WaitForTwist ());
			}  
			break;
		case 7://PROTECT HAT
			if (Vector2.Distance(GameObject.Find ("HeroBaseEnemy").transform.position,transform.position) > 0.3f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("HeroBaseEnemy").transform.position);
			} else {
				StartCoroutine (WaitForTwist ());
			} 
			break;
		case 8://PUSH HAT 
			if (Vector2.Distance(GameObject.Find ("HeroBase").transform.position,transform.position) > 0.3f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("HeroBase").transform.position);
			} else {
				StartCoroutine (WaitForTwist ());
			}  
			break;
		case 9://RETREAT
			if (Vector2.Distance(GameObject.Find ("HeroBaseEnemy").transform.position,transform.position) > 0.3f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("HeroBaseEnemy").transform.position);
			} else {
				StartCoroutine (WaitForTwist ());
			}  
			break;
		case 10://HERO BATTLE
			if (Vector2.Distance (GameObject.Find ("Hero").transform.position, transform.position) > 0.3f) {
				waypoint.EnemyMovementPlacement (GameObject.Find ("Hero").transform.position);
			} else {
				StartCoroutine (WaitForTwist ());
			} 
			break;
		case 11://RANDON MOVEMENT
			gemColectorChance += 10;
			StartCoroutine (WaitForTwist ());
			break;
		default:
			Twist (1);
			break;
		}

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

}

