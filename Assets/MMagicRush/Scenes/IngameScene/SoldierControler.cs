using UnityEngine;
using System.Collections;

public class SoldierControler : MonoBehaviour {

	public enum TipoSoldado {Guerreiro, Mago, Lanceiro, General};

	private enum STATE
	{
		MOVE,
		IDLE,
		DEFAULT,
		ATACKING,
		RETREAT,
		SEEKING,
	}

	public TipoSoldado Tipo;

	private STATE state = STATE.DEFAULT;

	public int team;

	[HideInInspector]
	public Vector3 touchStartPosition;

	[HideInInspector]
	public Vector3 touchEndPosition;

	[HideInInspector]
	public GameObject targetEnemy;

	public GameObject healtbarGeneral;

	public GameObject healtbarSoldier;

	public GameObject healtbarLanceiro;

	public GameObject healtbarMago;

	public Sprite warrior;

	public Sprite archer;

	public Sprite mage;

	public Sprite general;

	public GameObject light;

	public GameObject platform;

	public GameObject heroBase;

	public bool inCombat;

	public int vidaMax;

	public int vida;

	public float range;

	public int dano;

	public float speed;

	public GameObject arrowSlot;

	public GameObject arrowModel;

	public GameObject victoryScreen;

	public GameObject defeatScreen;

	private float count; 
	[HideInInspector]
	public float desloc;
	public float deslocTimer;

	// Use this for initialization
	void Start () {


		//CONFIGURAÇÃO DE TIPO
		switch (Tipo) {
		case(TipoSoldado.General):
			this.vidaMax = 10;
			this.range = 4;
			this.dano = 3;
			this.speed = 0;
			platform.SetActive (false);
			this.healtbarGeneral.SetActive (true);
			this.GetComponent<SpriteRenderer> ().sprite = general;
			break;
		case(TipoSoldado.Guerreiro):
			this.vidaMax = 10;
			this.range = 1;
			this.dano = 2;
			this.speed = 2;
			this.healtbarSoldier.SetActive (true);
			this.GetComponent<SpriteRenderer> ().sprite = warrior;
			this.state = STATE.SEEKING;
			break;
		case(TipoSoldado.Lanceiro):
			this.vidaMax = 4;
			this.range = 4;
			this.dano = 4;
			this.speed = 2.5f;
			this.healtbarLanceiro.SetActive (true);
			this.GetComponent<SpriteRenderer> ().sprite = archer;
			break;
		case(TipoSoldado.Mago):
			this.vidaMax = 4;
			this.range = 3;
			this.dano = 1;
			this.speed = 2;
			this.healtbarMago.SetActive (true);
			this.GetComponent<SpriteRenderer> ().sprite = mage;
			break;
		default:
			break;

		}

		// CONFIGURAÇÃO DE EQUIPE
		if (this.team == 1) {
			this.tag = "enemysoldier1";
			//this.GetComponent<SpriteRenderer> ().color = Color.blue;
		} else {
//			this.light.SetActive (false);
//			platform.SetActive (false);
			this.tag = "enemysoldier2";
			platform.GetComponent<SpriteRenderer> ().color = Color.red;
			//this.GetComponent<SpriteRenderer> ().color = Color.red;
//			this.GetComponent<SpriteRenderer> ().color = new Color(this.GetComponent<SpriteRenderer> ().color.r, this.GetComponent<SpriteRenderer> ().color.g, this.GetComponent<SpriteRenderer> ().color.b, 0.0f);
//			this.healtbarGeneral.SetActive (false);
//			this.healtbarLanceiro.SetActive (false);
//			this.healtbarMago.SetActive (false);
//			this.healtbarSoldier.SetActive (false);
		}


		this.vida = vidaMax;
	
		this.speed = speed - 1.5f;

		//DESLOCAMENTO INICIAL
		if (Random.value < 0.5f) {
			desloc = 1f;
		} else {
			desloc = -1f;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		//ORDEM DE LAYER
		if (this.Tipo != TipoSoldado.General) {
			this.GetComponent<SpriteRenderer> ().sortingOrder = -(int)(this.transform.position.y - 0.5f);
		}

		//DESLOCAMENTO INICIAL
		if (this.Tipo != SoldierControler.TipoSoldado.General && deslocTimer < Random.Range(30,50)) {
			transform.Translate (Vector2.left * Time.deltaTime * desloc);
			deslocTimer++;
		}


		//ATUALIZAÇÃO DE BARRAS DE VIDA
		this.healtbarGeneral.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarSoldier.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarLanceiro.GetComponent<HealtBar> ().Life = this.vida;
		this.healtbarMago.GetComponent<HealtBar> ().Life = this.vida;


		// CONDICÃO DE DERROTA
		if (Tipo == TipoSoldado.General && team == 1 && vida <=0) {
			defeatScreen.SetActive (true);
			//Time.timeScale = 0;
		}

		// CONDICÃO DE VITORIA
		if (Tipo == TipoSoldado.General && team == 2 && vida <=0) {
			victoryScreen.SetActive (true);
			//Time.timeScale = 0;
		}

		//MAQUINA DE ESTADOS PARA GENERAL
		if (Tipo == TipoSoldado.General) {
			if (SeekEnemyTarget () != null) {
				targetEnemy = SeekEnemyTarget ();
				if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range - 0.5f) {
				//	transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
				} else if (inCombat == false) {
					//this.state = STATE.ATACKING;
					inCombat = true;
					StartCoroutine (Atackenemy (targetEnemy));
				} 

				if (inCombat == true && arrowSlot != null && targetEnemy != null) {
					if (Vector3.Distance (arrowSlot.transform.position, targetEnemy.transform.position) > 0.1f && targetEnemy != null) {
						arrowSlot.transform.position = Vector3.MoveTowards (arrowSlot.transform.position, targetEnemy.transform.position, Time.deltaTime * 5);
						Vector3 moveDirection = arrowSlot.transform.position - targetEnemy.transform.position; 
						if (moveDirection != Vector3.zero) {
							float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
							arrowSlot.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
						}
					} else {
						//arrowSlot.GetComponent<ArrowScript> ().DestroyArrow ();
						//arrowSlot = null;
					}
				}

				if (targetEnemy.GetComponent<SoldierControler> ().vida <= 0 || targetEnemy == null) {
					inCombat = false;
				}

				if (this.tag == "enemysoldier2" && Vector3.Distance (transform.position, targetEnemy.transform.position) < 4) {
					FadeIn ();
				}
			}
		}

		//MAQUINA DE ESTADOS PARA SOLDADOS
		if (Tipo != TipoSoldado.General) {

			if (this.vida <= 0) {
				Destroy (this.gameObject);
			}

			//
			//ENTRADA DE TOUCH
			//
//			if (Input.touchCount > 0) {
//				// The screen has been touched so store the touch
//				Touch touch = Input.GetTouch (0);
//				if (touch.phase == TouchPhase.Began) {
//					touchStartPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 10));
//
//					if (state == STATE.IDLE) {
//						state = STATE.DEFAULT;
//					}
//				}
//
//				if (touch.phase == TouchPhase.Stationary) {
//					count = count++;
//					if (count >= 1) {
//						Debug.Log ("Stop");
//						this.state = STATE.IDLE;
//						count = 0;
//					}
//			
//				}
//
//				//colocar um this.state != move se necessario
//				if (touch.phase == TouchPhase.Ended && this.state != STATE.IDLE && Vector3.Distance (transform.position, touchStartPosition) < 0.5) {
//					// If the finger is on the screen, move the object smoothly to the touch position
//					touchEndPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 10));                
//					this.state = STATE.MOVE;
//				}
//			}
				
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

			if (this.state == STATE.RETREAT) {
				//targetPosition = this.transform.position;
				if (Vector3.Distance (transform.position, heroBase.transform.position) > range - 0.5f) {
					transform.position = Vector3.MoveTowards (transform.position, heroBase.transform.position, Time.deltaTime * speed);
				} else {
					this.state = STATE.SEEKING;
				}
			}

//			if (this.state == STATE.RETREAT) {
//				//targetPosition = this.transform.position;
//				transform.position = Vector3.MoveTowards (heroBase.transform.position, heroBase.transform.position, Time.deltaTime * speed);
//				if(Vector3.Distance (arrowSlot.transform.position, heroBase.transform.position) > 0.1f){
//					this.state = STATE.SEEKING;
//				}
//			}

			if (this.state == STATE.SEEKING) {
				targetEnemy = SeekEnemyTarget ();
				if (targetEnemy != null) {
					this.state = STATE.DEFAULT;
				}
			}

			if (this.state == STATE.DEFAULT) {
				
					//DESLOCAMENTO ATE INIMIGO
					if (Vector3.Distance (transform.position, targetEnemy.transform.position) > range - 0.5f) {
						transform.position = Vector3.MoveTowards (transform.position, targetEnemy.transform.position, Time.deltaTime * speed);
					} else if (inCombat == false) {
						//this.state = STATE.ATACKING;
						inCombat = true;
						StartCoroutine (Atackenemy (targetEnemy));
					} 

					//ANIMACAODE TIRO DE PROJETEIS
					if (inCombat == true && arrowSlot != null && this.Tipo == TipoSoldado.Lanceiro && targetEnemy != null) {
						if (Vector3.Distance (arrowSlot.transform.position, targetEnemy.transform.position) > 0.1f && targetEnemy != null) {
							arrowSlot.transform.position = Vector3.MoveTowards (arrowSlot.transform.position, targetEnemy.transform.position, Time.deltaTime * 5);
							Vector3 moveDirection = arrowSlot.transform.position - targetEnemy.transform.position; 
							if (moveDirection != Vector3.zero) {
								float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
								arrowSlot.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
							}
						} else {
							//arrowSlot.GetComponent<ArrowScript> ().DestroyArrow ();
							//arrowSlot = null;
						}
					}

					//FIM DO COMBATE
					if (targetEnemy.GetComponent<SoldierControler> ().vida <= 0 || targetEnemy == null) {
						inCombat = false;
					}
						
				} else {
					
				}
				
			}



		Debug.Log (this.state);
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

	public void FadeOut(){
		float duration = 2f;
		Color colorStart = this.GetComponent<SpriteRenderer>().color;
		Color colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
		platform.SetActive (false);
		for (float t = 0.0f; t < duration; t += Time.deltaTime) {
			this.GetComponent<SpriteRenderer>().color = Color.Lerp (colorStart, colorEnd, t/duration);
		}
	}

	public void FadeIn(){
		float duration = 2f;
		Color colorStart = this.GetComponent<SpriteRenderer>().color;
		Color colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 255);

		switch (Tipo) {
		case(TipoSoldado.General):
			this.healtbarGeneral.SetActive (true);
			break;
		case(TipoSoldado.Guerreiro):
			platform.SetActive (true);
			this.healtbarSoldier.SetActive (true);

			break;
		case(TipoSoldado.Lanceiro):
			platform.SetActive (true);
			this.healtbarLanceiro.SetActive (true);
			break;
		case(TipoSoldado.Mago):
			platform.SetActive (true);
			this.healtbarMago.SetActive (true);
			break;
		default:
			break;

		}
		for (float t = 0.0f; t < duration; t += Time.deltaTime) {
			this.GetComponent<SpriteRenderer>().color = Color.Lerp (colorStart, colorEnd, t/duration);
		}
	}


//	public void TrowArrow(){
//		GameObject arrow = Instantiate (arrowModel, this.transform.position, Quaternion.identity);
//		if (Vector3.Distance (arrow.transform.position, targetEnemy.transform.position) > 0.1f) {
//			arrow.transform.position = Vector3.MoveTowards (arrow.transform.position, targetEnemy.transform.position, Time.deltaTime * 5);
//		} else {
//			Destroy (arrow);
//		}
//	}

	IEnumerator Atackenemy(GameObject enemy){
		if (this.Tipo == TipoSoldado.Lanceiro || this.Tipo == TipoSoldado.General) {
			arrowSlot = (GameObject)Instantiate (arrowModel, this.transform.position, Quaternion.identity);
		}
		yield return new WaitForSeconds (0.7f);
		if (enemy != null) {
			enemy.GetComponent<SoldierControler> ().vida = enemy.GetComponent<SoldierControler> ().vida - 1;
		}
		yield return new WaitForSeconds (1f);
		if (targetEnemy != null && Vector3.Distance (transform.position, targetEnemy.transform.position) <= range-0.5f) {
			StartCoroutine (Atackenemy (targetEnemy));
		}else {
			this.state = STATE.DEFAULT;
		}
	}

	


}
