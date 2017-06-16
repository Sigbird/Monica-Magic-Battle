using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {
	public int line;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public GameObject startObject;
	public GameObject endObject;
	public bool firstLineDraw;

	public bool mirrorZ = true;

	void Start () {
		//Strech(gameObject, startPosition, endPosition, mirrorZ);
	}

	// Update is called once per frame
	void Update () {
		if (line == 1) {
			startObject = GameObject.Find ("Hero");
			endObject = GameObject.Find ("Waypoint1");
			if (startObject != null && endObject != null && firstLineDraw == false) {
				startPosition = new Vector2(startObject.transform.position.x,startObject.transform.position.y-0.5f);
				endPosition = endObject.transform.position;
				Strech (gameObject, startPosition, endPosition, mirrorZ);
				firstLineDraw = true;
			} else if (endObject == null) {
				transform.position = new Vector2 (50, 50);
				if (GameObject.FindGameObjectsWithTag ("herowaypoint").Length <= 0)
					firstLineDraw = false;
			}
		} else if(line == 2) {
			if (GameObject.Find ("Waypoint1")!= null)
			if (GameObject.Find ("Waypoint1").GetComponent<MovementMarkerScript> ().reached == true) {
				startObject = GameObject.Find ("Hero");
			} else {
				startObject = GameObject.Find ("Waypoint1");
			}
			endObject = GameObject.Find ("Waypoint2");
			if (startObject != null && endObject != null) {
				startPosition = startObject.transform.position;
				endPosition = endObject.transform.position;
				Strech (gameObject, startPosition, endPosition, mirrorZ);
			} else if (endObject == null || startObject == null) {
				transform.position = new Vector2 (50, 50);
			}
		} else if(line == 3) {
			if (GameObject.Find ("Waypoint2")!= null)
			if (GameObject.Find ("Waypoint2").GetComponent<MovementMarkerScript> ().reached == true) {
				startObject = GameObject.Find ("Hero");
			} else {
				startObject = GameObject.Find ("Waypoint2");
			}
			endObject = GameObject.Find ("Waypoint3");
			if (startObject != null && endObject != null) {
				startPosition = startObject.transform.position;
				endPosition = endObject.transform.position;
				Strech (gameObject, startPosition, endPosition, mirrorZ);
			} else if (endObject == null) {
				transform.position = new Vector2 (50, 50);
			}
		} else if(line == 4) {
			if (GameObject.Find ("Waypoint3")!= null)
			if (GameObject.Find ("Waypoint3").GetComponent<MovementMarkerScript> ().reached == true) {
				startObject = GameObject.Find ("Hero");
			} else {
				startObject = GameObject.Find ("Waypoint3");
			}
			endObject = GameObject.Find ("Waypoint1");
			if (startObject != null && endObject != null && GameObject.Find ("Line1").transform.position.x > 40) {
				startPosition = startObject.transform.position;
				endPosition = endObject.transform.position;
				Strech (gameObject, startPosition, endPosition, mirrorZ);
			} else if (endObject == null) {
				transform.position = new Vector2 (50, 50);
			}
		}

	}


	public void Strech(GameObject _sprite,Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ) {
		Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
		_sprite.transform.position = centerPos;
		Vector3 direction = _finalPosition - _initialPosition;
		direction = Vector3.Normalize(direction);
		_sprite.transform.right = direction;
		if (_mirrorZ) _sprite.transform.right *= -1f;
		Vector3 scale = new Vector3(1,0.2f,1);
		scale.x = Vector3.Distance(_initialPosition, _finalPosition) - 0.5f;
		GetComponent<SpriteRenderer> ().size = scale;
		//_sprite.transform.localScale = scale;
	}

}