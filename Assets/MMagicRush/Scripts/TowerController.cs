using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {
	public int towerID;
	public int team;

	public GameObject targetEnemy;

	public GameObject healtbarSoldier;

	public AudioManager audioManager;

	public int vidaMax;
	public int vida;
	public int reach;
	public int damage;
	public int damageSpeed;
	public int range;
	public Sprite[] torresSprites;


	//FLAGS
	public float danoCD;
	public bool inCombat;
	public bool healing;
	public bool GainXP;
	public bool seeking;
	public float cdSeek;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.Find ("GameController").GetComponent<AudioManager> ();
		audioManager.PlayAudio ("tower");
		SetupTower (towerID);

		UpdateLife ();
		this.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();


	}

	// Update is called once per frame
	void Update () {

		//EVENTO DE MORTE
		if (this.vida <= 0) {
			audioManager.PlayAudio ("tower");
			Destroy (this.gameObject);
		}
		if (this.vida > this.vidaMax) {
			this.vida = this.vidaMax;
		}
		
		if (seeking && cdSeek >= 1) {
			this.targetEnemy = SeekEnemyTarget ();
			cdSeek = 0;
		} else {
			cdSeek += Time.deltaTime;
		}
			

			if (targetEnemy == null) {//CONFIRMA SE ALVO VIVE
				targetEnemy = SeekEnemyTarget ();
			} else {

				//TRAVA EM UM ALVO
				if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range) { //MOVE EM DIRECAO
					//FICA PARADA

				} else if (targetEnemy != null) { //ATACA ALVO
					//anim.SetTrigger ("Attack");
					if (danoCD < damageSpeed) { //TEMPO ENTRE ATAQUES
						if (targetEnemy.GetComponent<SoldierControler> () != null) {//ALVO HEROI
							targetEnemy.GetComponent<SoldierControler> ().vida -= damage;
							targetEnemy.GetComponent<SoldierControler> ().UpdateLife();
							audioManager.PlayAudio ("shot");
						} 
						danoCD = 4;
					} else {
						danoCD -= Time.deltaTime;
					}

				} 
			}

	}

	public void SetupTower(int id){

		switch (id) {
		case(1): // PAPEL
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 4;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites[0];
			break;
		case(2): // AGUA
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 5;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2; //4
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [1];
			break;
		case(3): //DESENTUPIDOR
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 4;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 3;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [2];
			break;
		case(4): //NEVE
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 4;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [3];
			break;
		case(5): //CURA
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 4;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 1;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [4];
			break;
		case(6): //TESOURO
			this.vidaMax = 2;
			this.vida = 2;
			this.reach = 5;
			this.damage = 1;
			this.damageSpeed = 3;
			this.range = 2;//5
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [5];
			break;
		case(7): //SONO
			this.vidaMax = 10;
			this.vida = 10;
			this.reach = 5;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 1;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [6];
			break;
		case(8): //ANTI TORRE
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 4;
			this.damage = 3;
			this.damageSpeed = 3;
			this.range = 1;
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [7];
			break;
		case(9): //PROTETORA
			this.vidaMax = 6;
			this.vida = 6;
			this.reach = 8;
			this.damage = 5;
			this.damageSpeed = 3;
			this.range = 2;//6
			this.GetComponent<SpriteRenderer> ().sprite = torresSprites [8];
			break;
		default:
			Debug.LogWarning ("TowerOutofRange");
			break;

		}

		// CONFIGURAÇÃO DE EQUIPE
		if (this.team == 1) {
			this.tag = "enemytower1";
		} else {
			this.tag = "enemytower2";
			this.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	public void UpdateLife(){
		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarSoldier.GetComponent<HealtBar> ().MaxLife = this.vidaMax;
		this.healtbarSoldier.GetComponent<HealtBar> ().UpdateHealtbars();
	}

	public GameObject SeekEnemyTarget (){

		GameObject Emin = null;
		float minDis = Mathf.Infinity;
		if (this.tag == "enemytower1") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier2") != null) {
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier2")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist <= reach) {
						if (dist < minDis) {
							Emin = obj;
							minDis = dist;
						}
					}
				}
			} 
			if(Emin == null) {
				return GameObject.Find ("HeroBaseEnemy");
			}
		} else if (this.tag == "enemysoldier2") {
			if (GameObject.FindGameObjectsWithTag ("enemysoldier1") != null) {
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("enemysoldier1")) {
					float dist = Vector3.Distance (transform.position, obj.transform.position);
					if (dist <= reach) {
						if (dist < minDis) {
							Emin = obj;
							minDis = dist;
						}
					}
				}
			}
			if(Emin == null) {
				return GameObject.Find ("HeroBase");
			}
		}
		return Emin;
	}

}
