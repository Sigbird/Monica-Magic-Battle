using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScript : MonoBehaviour {


	public GameObject EnemyWaypointMarker;

	public GameObject WaypointMarker;
	public Sprite CaptureIcon;
	public Sprite MineIcon;
	public Sprite DefendIcon;
	public Sprite MovementIcon;
	public Sprite AttackIcon;

	public Transform Hero;
	public Transform Enemy;
	public Transform Base;

	public static bool UIopen;
	public int progress;
	public int enemyprogress;

	// Use this for initialization
	void Start () {
		progress = 1;
		enemyprogress = 1;
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

	}

	void OnMouseDown(){
		if (Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),Enemy.position) < 1 && UIopen == false) {
			Hero.GetComponent<WPSoldierControler> ().targetEnemy = Enemy.gameObject;
			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
		}else if(Vector2.Distance(Camera.main.ScreenToWorldPoint (Input.mousePosition),Base.position) < 1 && UIopen == false){
			foreach (GameObject o in GameObject.FindGameObjectsWithTag ("herowaypoint")) {
				Destroy (o.gameObject);
			}
			Hero.GetComponent<WPSoldierControler> ().lockedTarget = true;
			Hero.GetComponent<WPSoldierControler> ().targetEnemy = Base.gameObject;
		}else if (MovementCounter () < 2 && Camera.main.ScreenToWorldPoint (Input.mousePosition).y > -3 && UIopen == false) {
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			ChangeIcon (WaypointMarker.GetComponent<SpriteRenderer>());
			Instantiate (WaypointMarker, new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), Quaternion.identity).gameObject.name = "Waypoint"+progress;
			progress++;
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

	public void ChangeIcon(SpriteRenderer renderer){
		if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("treasureChest").transform.position) < 0.5f) {
			renderer.sprite = CaptureIcon; //CAPTURA DE BAU
		} else if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("HeroBaseEnemy").transform.position) < 0.5f) {
			renderer.sprite = CaptureIcon; //CAPTURA DE BASE INIMIGA
		} else if (GemNearbyClick()) {
			renderer.sprite = MineIcon; //CAPTURA DE GEMA
		} else if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("HeroBase").transform.position) < 0.5f) {
			renderer.sprite = DefendIcon; //DEFESA DE BASE
		} else if (Vector3.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), GameObject.Find ("HeroEnemy").transform.position) < 0.5f) {
			renderer.sprite = AttackIcon; //ATAQUE DE INIMIGO
		} else{
			renderer.sprite = MovementIcon; //ICONE DE MOVIMENTO
		}

	}

	public bool GemNearbyClick(){
		GameObject[] Gems = GameObject.FindGameObjectsWithTag ("gem");
		foreach (GameObject o in Gems) {
			if (Vector2.Distance (new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y), o.transform.position) <= 0.5f) {
				return true;
			}
		}
		return false;
	}

	public void EnemyMovementPlacement(Vector2 position){
		if (EnemyMovementCounter () < 3) {
			WaypointMarker.GetComponent<MovementMarkerScript> ().progress = progress;
			if (position == null) {
				position = new Vector2 (Random.Range (-2.5f, 2.5f), Random.Range (0f, 4.5f));
			}
			Instantiate (EnemyWaypointMarker, position, Quaternion.identity);
			enemyprogress++;
		}
	}
}
