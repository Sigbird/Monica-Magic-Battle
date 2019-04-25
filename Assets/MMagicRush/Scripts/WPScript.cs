using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScript : MonoBehaviour {

	public bool tutorial;
	public GameObject EnemyWaypointMarker;

	public GameObject WaypointMarker;
	public Sprite CaptureIcon;
	public Sprite MineIcon;
	public Sprite DefendIcon;
	public Sprite MovementIcon;
	public Sprite AttackIcon;

	public Transform Hero;
	public Transform Enemy;
	public Transform EnemyTroop;
	public Transform EnemyBase;
	public Transform EnemyBaseTower;
	public Transform EnemyBaseTower2;
	public Transform Base;
	public Transform BaseTower;
	public Transform BaseTower2;
	public Transform HeroTroop;
	public GameObject[] EnemiesOnScene;

	public static bool UIopen;
	public int progress;
	public int enemyprogress;

	public GameController gameController;

	// Use this for initialization
	void Start () {
		UIopen = false;
		progress = 1;
		enemyprogress = 1;
		EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().enemy = true;
		WaypointMarker.GetComponent<MovementMarkerScript> ().enemy = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (progress >= 4) {
			progress = 1;
		}

		if (enemyprogress >= 4) {
			enemyprogress = 1;
		}

		if (MovementCounter () < 1) {
			progress = 1;
		}


//		if (Input.GetMouseButtonUp (0)) {
//			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
//
//			if (hit.collider != null) {
//				Debug.Log ("Troop");
//				if (hit.collider.transform.tag == "enemysoldier2") {
//					
//					EnemyTroop = hit.collider.transform;
//				}
//
//			}
//		}
		EnemiesOnScene = GameObject.FindGameObjectsWithTag("enemysoldier2");

	}

	void __onMouseUp(Transform Hero, Transform Enemy, Transform EnemyTroop, 
	Transform EnemyBase, Transform EnemyBaseTower, Transform EnemyBaseTower2) {
		bool cancel = false;
		int lastprogress = 0;
		string lastname = "";
		Hero.GetComponent<WPSoldierControler> ().targetEnemy = null;
		EnemyTroop = EnemyNearbyClick ();

		if (MovementCounter () > 0) {
			foreach (GameObject a in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), a.transform.position) < 1) {
					if (a.name == "Waypoint1") {
//						lastname = a.name;
//						lastprogress = a.GetComponent<MovementMarkerScript> ().progress;
						cancel = true;
					} else {
						progress--;
					}
					Destroy (a.gameObject);
				} 
			}
		}

//		if (cancel == true) {
//			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
//				o.name = "Waypoint1";
//				o.GetComponent<MovementMarkerScript> ().progress = 1;
////				GameObject.Find ("Line1").GetComponent<LineScript> ().endObject = o;
//			}
//			StartCoroutine (RedrawLine ());
//			progress--;
//			cancel = false;
//		}else 
		if (/*MovementCounter ()< 1  &&*/  Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),Enemy.position) < 1 && 
			UIopen == false) {
	//		Debug.Log ("HeroiInimigo");
			Hero.GetComponent<WPSoldierControler> ().targetEnemy = Enemy.gameObject;
			Hero.GetComponent<WPSoldierControler> ().seeking = false;
			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = true;
		//	Debug.Log ("Oi");
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
					Destroy (o.gameObject);
			}
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			Instantiate (WaypointMarker, Enemy).transform.position = Enemy.transform.position;
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity);
		}else if (/*MovementCounter ()< 1  &&*/ EnemyTroop != null && Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),EnemyTroop.position) < 1 && UIopen == false && EnemyTroop.GetComponent<SoldierControler>() != null) {
			//		Debug.Log ("HeroiInimigo");
			Hero.GetComponent<WPSoldierControler> ().targetEnemy = EnemyTroop.gameObject;
			Hero.GetComponent<WPSoldierControler> ().seeking = false;
			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = true;
				Debug.Log ("TroopDetected");
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			Instantiate (WaypointMarker, EnemyTroop).transform.position = EnemyTroop.transform.position;
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity);
		}else if (/*MovementCounter ()< 1  &&*/  Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),EnemyBase.position) < 1 && UIopen == false /*&& Enemy.GetComponent<WPIASoldierControler>().alive == true*/) {
//			Hero.GetComponent<WPSoldierControler> ().targetEnemy = EnemyBase.gameObject;
//			Hero.GetComponent<WPSoldierControler> ().seeking = false;
//			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
//			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
//				Destroy (o.gameObject);
//			}
//			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
//			Instantiate (WaypointMarker, EnemyBase).transform.position = EnemyBase.transform.position;
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = null;
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = true;
			StartCoroutine (CreateWaypoint (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y)));
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity).gameObject.name = "Waypoint"+progress;
			progress++;
		}else if(EnemyBaseTower != null && Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),EnemyBaseTower.position) < 1 && UIopen == false /*&& Enemy.GetComponent<SpriteRenderer>().enabled == true*/) {
//			Hero.GetComponent<WPSoldierControler> ().targetEnemy = EnemyBaseTower.gameObject;
//			Hero.GetComponent<WPSoldierControler> ().seeking = false;
//			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
//			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
//				Destroy (o.gameObject);
//			}
//			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
//			Instantiate (WaypointMarker, EnemyBaseTower).transform.position = EnemyBaseTower.transform.position;
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = true;
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = null;
			StartCoroutine (CreateWaypoint (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y)));
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity).gameObject.name = "Waypoint"+progress;
			progress++;
		}else if(EnemyBaseTower2 != null && Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),EnemyBaseTower2.position) < 1 && UIopen == false /*&& Enemy.GetComponent<SpriteRenderer>().enabled == true*/) {
//			Hero.GetComponent<WPSoldierControler> ().targetEnemy = EnemyBaseTower2.gameObject;
//			Hero.GetComponent<WPSoldierControler> ().seeking = false;
//			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
//			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
//				Destroy (o.gameObject);
//			}
//			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
//			Instantiate (WaypointMarker, EnemyBaseTower2).transform.position = EnemyBaseTower2.transform.position;
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = true;
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = null;
			StartCoroutine (CreateWaypoint (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y)));
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity).gameObject.name = "Waypoint"+progress;
			progress++;

		}else if(Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),Base.position) < 1 && UIopen == false){

			Debug.Log ("Clicou!");
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = DefendIcon;
			//WaypointMarker.GetComponent<MovementMarkerScript> ().herobase = true;
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity);
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = false;
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				if(o.GetComponent<MovementMarkerScript>().herobase == false)
					Destroy (o.gameObject);
			}
			Instantiate (WaypointMarker, Base.position, Quaternion.identity);

			//Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
			//Hero.GetComponent<WPSoldierControler> ().targetEnemy = Base.gameObject;
		}else if (/*MovementCounter () < 1 &&*/ Camera.main.ScreenToWorldPoint (Input.mousePosition).y > -3 && UIopen == false) {
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			WaypointMarker.GetComponent<MovementMarkerScript> ().targetMarker = false;
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = null;
			StartCoroutine (CreateWaypoint (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y)));
			//Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity).gameObject.name = "Waypoint"+progress;
			progress++;
		}
	}

	void OnMouseUp(){
		if (!gameController.multiplayer) {
			if (Hero.GetComponent<WPSoldierControler> ().alive == true) {
				__onMouseUp (Hero, Enemy, EnemyTroop, EnemyBase, EnemyBaseTower, EnemyBaseTower2);
			}
			return;
		}

		if (gameController.multiplayer) {
			if (BoltNetwork.IsClient) {
				if (Enemy.GetComponent<WPSoldierControler> ().alive == true) {
					__onMouseUp (Enemy, Hero, HeroTroop, Base, BaseTower, BaseTower2);
				}
				return;
			}

			if (BoltNetwork.IsServer) {
				if (Hero.GetComponent<WPSoldierControler> ().alive == true) {
					__onMouseUp (Hero, Enemy, EnemyTroop, EnemyBase, EnemyBaseTower, EnemyBaseTower2);
				}
			}
		}
	}
		
	IEnumerator CreateWaypoint(Vector2 pos){
		yield return new WaitForSeconds (0.1f);

		if (gameController.multiplayer) {
			if (BoltNetwork.IsServer) {
				__createWaypoint(Hero, pos);
			}
			if (BoltNetwork.IsClient) {
				__createWaypoint(Enemy, pos);
			}

			yield return null;
		}
		
		if (!gameController.multiplayer) {
			__createWaypoint(Hero, pos);
		}
	}

	void __createWaypoint(Transform Hero, Vector2 pos) {
		if(pos.y > 0.5f && Hero.transform.position.y<0.5){
			Instantiate(WaypointMarker, Hero.GetComponent<WPSoldierControler>().NearestPass.transform.position, Quaternion.identity).gameObject.name = "Waypoint"+progress;
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = MovementIcon;
			Instantiate(WaypointMarker, pos, Quaternion.identity).gameObject.name = "Waypoint"+progress;
		} else if(pos.y < 0.5f && Hero.transform.position.y>0.5){
			Instantiate(WaypointMarker, Hero.GetComponent<WPSoldierControler>().NearestPass.transform.position, Quaternion.identity).gameObject.name = "Waypoint"+progress;
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = MovementIcon;
			Instantiate(WaypointMarker, pos, Quaternion.identity).gameObject.name = "Waypoint"+progress;
		}else{
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			WaypointMarker.GetComponent<SpriteRenderer> ().sprite = MovementIcon;
			Instantiate(WaypointMarker, pos, Quaternion.identity).gameObject.name = "Waypoint"+progress;
		}
	}

	public int MovementCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("herowaypoint");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}

	public int EnemyMovementCounter(){
		int x = 0;
		GameObject[] go = GameObject.FindGameObjectsWithTag ("enemywaypoint");
		foreach(GameObject c in go) {
			x++;
		}
		return x;
	}

	public void ChangeIcon(SpriteRenderer renderer) {
//		if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("treasureChest").transform.position) < 0.5f) {
//			renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
//			renderer.sprite = CaptureIcon; //CAPTURA DE BAU
//		} else 
		// if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("HeroBaseEnemy").transform.position) < 0.5f) {
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	if (tutorial == true && GameObject.Find ("TutorialHand") != null && GameObject.Find ("Tut12") != null) {
		// 		GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 		GameObject.Find ("Tut12").SetActive(false);
		// 	}
		// 	renderer.sprite = CaptureIcon; //CAPTURA DE BASE INIMIGA
		// } else 
		// 	if (GemNearbyClick()) {
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	renderer.sprite = MineIcon; //CAPTURA DE GEMA

		// } else
		// 	if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("HeroBase").transform.position) < 0.5f) {
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = true;
		// 	renderer.sprite = DefendIcon; //DEFESA DE BASE
		// } else if(Enemy != null && Enemy.GetComponent<WPIASoldierControler>().alive == true && Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Enemy.transform.position) < 1f){
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 		if (tutorial == true && GameObject.Find ("TutorialHand") != null)
		// 			GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 	renderer.sprite = AttackIcon; //ATAQUE DE INIMIGO
		// }else if(EnemyTroop != null && EnemyTroop.GetComponent<SoldierControler>() != null && Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), EnemyTroop.transform.position) < 1f){
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	if (tutorial == true && GameObject.Find ("TutorialHand") != null)
		// 		GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 	renderer.sprite = AttackIcon; //ATAQUE DE TROPA
		// } else if(EnemyBase != null && Vector3.Distance (new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0), EnemyBase.transform.position) <= 1f){
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	if (tutorial == true && GameObject.Find ("TutorialHand") != null) {
		// 		GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 	}
		// 	renderer.sprite = AttackIcon; //ATAQUE DE BASE INIMIGO
		// } else if(GameObject.Find("EnemyTower1") != null && Vector3.Distance (new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0), GameObject.Find ("EnemyTower1").transform.position) <= 1f){
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	if (tutorial == true && GameObject.Find ("TutorialHand") != null) {
		// 		GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 	}
		// 	renderer.sprite = AttackIcon; //ATAQUE DE BASE INIMIGO
		// } else if(GameObject.Find("EnemyTower2") != null && Vector3.Distance (new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0), GameObject.Find ("EnemyTower2").transform.position) <= 1f){
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = true;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	if (tutorial == true && GameObject.Find ("TutorialHand") != null) {
		// 		GameObject.Find ("TutorialHand").GetComponent<DestroyAfter> ().DisableObj ();
		// 	}
		// 	renderer.sprite = AttackIcon; //ATAQUE DE BASE INIMIGO
		// } else{
			
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().capture = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().targetMarker = false;
		// 	renderer.gameObject.GetComponent<MovementMarkerScript> ().herobase = false;
		// 	renderer.sprite = MovementIcon; //ICONE DE MOVIMENTO
		// }

	}

	public bool GemNearbyClick(){
		GameObject[] Gems = GameObject.FindGameObjectsWithTag ("enemygem");
		foreach (GameObject o in Gems) {
			if (Vector2.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), o.transform.position) <= 0.1f && o.GetComponent<GemScript>().enabled == true) {
				return true;
			}
		}
		return false;
	}

	public Transform EnemyNearbyClick(){
		
		foreach (GameObject o in EnemiesOnScene) {
			if (Vector2.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), o.transform.position) <1f ) {
				if (o.GetComponent<SoldierControler> () != null) {
					return o.transform;
				}
			}
		}
		return null;
	}

	public void EnemyMovementPlacement(Vector2 position){
		if (EnemyMovementCounter () < 3) {
			EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			if (position == null) {
				position = new Vector2 (Random.Range (-2.5f, 2.5f), Random.Range (0f, 4.5f));
			}
			EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().capture = false;
//			GameObject[] Gems = GameObject.FindGameObjectsWithTag ("enemygem");
//			foreach (GameObject o in Gems) {
//				if (Vector2.Distance (position, o.transform.position) <= 0.6f && o.GetComponent<GemScript> ().enabled == true) {
//					EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().capture = true;
//				
//				} 
//			}

			if(position.y > 0.5f && Enemy.transform.position.y<0.5){
				EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = 1;
				Instantiate(EnemyWaypointMarker, Enemy.GetComponent<WPIASoldierControler>().NearestPass.transform.position, Quaternion.identity).gameObject.name = "Waypoint"+1;
				EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = 2;
				Instantiate(EnemyWaypointMarker, position, Quaternion.identity).gameObject.name = "Waypoint"+2;
			} else if(position.y < 0.5f && Enemy.transform.position.y>0.5){
				EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = 1;
				Instantiate(EnemyWaypointMarker, Enemy.GetComponent<WPIASoldierControler>().NearestPass.transform.position, Quaternion.identity).gameObject.name = "Waypoint"+1;
				EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = 2;
				Instantiate(EnemyWaypointMarker, position, Quaternion.identity).gameObject.name = "Waypoint"+2;
			}else{
				EnemyWaypointMarker.GetComponent<MovementMarkerScript> ().progress = 1;
				Instantiate(EnemyWaypointMarker, position, Quaternion.identity).gameObject.name = "Waypoint"+1;
			}
			Debug.Log("Instanciou");
			//Instantiate (EnemyWaypointMarker, position, Quaternion.identity);
			//enemyprogress++;
		}
	}

	IEnumerator RedrawLine(){
		yield return new WaitForSeconds (0.01f);
		///GameObject.Find ("Line1").GetComponent<LineScript> ().firstLineDraw = false;
	}
}
