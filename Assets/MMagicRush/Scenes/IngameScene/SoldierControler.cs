using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoldierControler : MonoBehaviour {

	//public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	public enum STATE
	{
		DEFAULT,
		RETREAT,
	}

	//public TipoSoldado Tipo;

	public bool heroUnity;

	public int troopId;

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


	public int vidaMax;


	public int vida;

	public float reach;


	public int damage;


	public float damageSpeed;


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

		LeftExit [0] = GameObject.Find ("Left1").gameObject;
		LeftExit [1] = GameObject.Find ("Left2").gameObject;
		LeftExit [2] = GameObject.Find ("Left3").gameObject;
		RightExit [0] = GameObject.Find ("Right1").gameObject;
		RightExit [1] = GameObject.Find ("Right2").gameObject;
		RightExit [2] = GameObject.Find ("Right3").gameObject;
		ChooseLane ();

//		if (heroUnity) {// CONFIGURAÇÃO DE HEROIS
//			SetupHero();
//		} else {//CONFIGURAÇÃO DE TROPAS
			SetupTroop(troopId);
//		}
			
		//CONFIGURAÇÃO EM COMUM 
		UpdateLife ();
		this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();

		//UpdateEnergy ();
		this.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		this.energybarSoldier.GetComponent<HealtBar> ().energy = true;

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
		this.speed = speed / 10;
		this.maxSpeed = this.speed;
		this.level = 1;
		this.healtbarSoldier.SetActive (true);
		this.state = STATE.DEFAULT;

		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		audioManager.PlayAudio ("spawn");

		//DESLOCAMENTO INICIAL
		HeroCharacter = GameObject.Find("HeroSpritezone");
		EnemyCharacter = GameObject.Find ("EnemySpritezone");
	

	}
	
	// Update is called once per frame
	void Update () {

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
		if(this.GetComponent<SpriteRenderer>().enabled == true){
			summonCounter += Time.deltaTime;
		}

		if (summonCounter >= 9 && this.summon == true) {
			summonCounter = 0;
			GameObject t =  troop;
			t.GetComponent<SoldierControler> ().troopId = 3;
			t.GetComponent<SoldierControler> ().step = this.step;
			t.GetComponent<SoldierControler> ().summon = false;
			Instantiate (t, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y), Quaternion.identity);
			Instantiate (t, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
		}
			

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
		if (this.tag == "enemysoldier1" && LeftExit.Length > 0 && RightExit.Length > 0  && targetEnemy == null) {

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
				transform.eulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, 180, 0);
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

		if (seeking == false && this.targetEnemy != null) { 

			//DESLOCAMENTO ATE INIMIGO
			if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
				//anim.SetTrigger ("Walk");
				//SpendingEnergy ();
				//transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
				seeking = true;
			} else if (targetEnemy != null && this.GetComponent<SpriteRenderer> ().enabled == true) { //ATACA ALVO
//				if (targetEnemy.transform.position.x < transform.position.x) {
//					GetComponent<SpriteRenderer> ().flipX = true;
//				} else if (targetEnemy.transform.position.x > transform.position.x) {
//					GetComponent<SpriteRenderer> ().flipX = false;
//				}

				if (danoCD > damageSpeed) { //TEMPO ENTRE ATAQUES
					danoCD = 0;
					if(haveAnimation)
						this.transform.Find("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
					//anim.SetTrigger ("Attack");
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
			seeking = true;
		}

		danoCD += Time.deltaTime * 2;

	}

	public void SetupTroop(int id){

		switch (id) {
		case(1): // BIDU
			this.vidaMax = 5;
			this.vida = 5;
			this.reach = 2;
			this.damage = 2;
			this.damageSpeed = 3;
			this.range = 2;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [0];
			this.haveAnimation = true;
			Instantiate (animations [1], transform);
			break;
		case(2): // ASTRONAUTA
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2;
			this.speed = 5;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [1];
			//this.GetComponent<SpriteRenderer> ().flipX = true;
			Instantiate (animations [2], transform);
			this.haveAnimation = true;
			break;
		case(3): //ANJINHO -> Cranicola
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 2;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [2];
			Instantiate (animations [3], transform);
			this.haveAnimation = true;
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
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [3];
			Instantiate (animations [4], transform);
			this.haveAnimation = true;
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
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [4];
			Instantiate (animations [5], transform);
			this.haveAnimation = true;
			break;
		case(6): //PENADINHO
			this.vidaMax = 3;
			this.vida = 3;
			this.reach = 2;
			this.damage = 2;
			this.damageSpeed = 3;
			this.range = 3;
			this.speed = 2;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = true;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [5];
			//this.GetComponent<SpriteRenderer> ().flipX = true;
			Instantiate (animations [6], transform);
			this.haveAnimation = true;
			break;
		case(7): //MAURICIO ->
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
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 1;
			this.damage = 3;
			this.damageSpeed = 3;
			this.range = 1;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [7];
			Instantiate (animations [8], transform);
			this.haveAnimation = true;
			break;
		case(9): //MINGAU
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 1;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 1;
			this.speed = 4;
			this.energyMax = 1;
			this.energy = 200;
			this.summon = false;
			//this.GetComponent<SpriteRenderer> ().sprite = tropasSprites [8];
			Instantiate (animations [9], transform);
			this.haveAnimation = true;
			break;
		case(10): //ALFREDO
			this.vidaMax = 9;
			this.vida = 9;
			this.reach = 2;
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
			this.reach = 2;
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
			platform.GetComponent<SpriteRenderer> ().color = Color.red;

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
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {//PROCURA TROPA
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (obj.GetComponent<SoldierControler> () != null) {
						if (dist <= reach) {
							if (dist < minDis && obj.GetComponent<SpriteRenderer> ().enabled == true) {
								Emin = obj;
								minDis = dist;
							}
						}
					} else if (obj.GetComponent<WPIASoldierControler> () != null) {
						if (obj.GetComponent<SpriteRenderer> ().enabled == true) {
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
								Emin = obj;
								minDis = dist;
							}
						}
					} else if (obj.GetComponent<WPSoldierControler> () != null) {
						if (obj.GetComponent<SpriteRenderer> ().enabled == true) {
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
		danoCD = 0;
		yield return new WaitForSeconds (0.5f);
		if (targetEnemy != null) {
			if (targetEnemy.GetComponent<WPIASoldierControler> () != null) {//ALVO HEROI
				//targetEnemy.GetComponent<WPIASoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<WPIASoldierControler> ().UpdateLife ();
				targetEnemy.GetComponent<WPIASoldierControler> ().ReceiveDamage (damage);
				if (this.range > 1)
					TrowArrow ();
				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
					this.targetEnemy = null;
					//lockedTarget = false;
				}
			} else if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO TROPA
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				targetEnemy.GetComponent<SoldierControler> ().ReceiveDamage (damage);
				if (this.range > 1)
					TrowArrow ();
				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
					this.targetEnemy = null;
					//lockedTarget = false;
				}
			} else if (targetEnemy.GetComponent<ChargesScript> () != null) {//ALVO TORRE
				//targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
				//targetEnemy.GetComponent<SoldierControler> ().UpdateLife ();
				targetEnemy.GetComponent<ChargesScript> ().progress += 0.1f;
				if (this.range > 1)
					TrowArrow ();
				//				if (targetEnemy.GetComponent<SpriteRenderer> ().enabled == false) { // ALVO MORREU
				//					this.targetEnemy = null;
				//					lockedTarget = false;
				//				}
				//ALVO BASE
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

	IEnumerator DeathEvent(){
		if (haveAnimation) {
			this.transform.Find("Animation(Clone)").gameObject.GetComponentInChildren<Animator> ().SetTrigger ("Death");
			yield return new WaitForSeconds (0.2f);
		} 
		Destroy (this.gameObject);
	}
		

	public void TrowArrow(){
		
	GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		if (targetEnemy != null) {
		arrow.GetComponent<ArrowScript> ().target = targetEnemy;
	} else {
		Destroy (arrow);
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
