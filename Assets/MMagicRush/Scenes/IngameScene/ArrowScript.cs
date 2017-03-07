using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 relativePos = transform.position - target.transform.position;

		float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

		transform.rotation = q;

		if (Vector3.Distance (this.transform.position, target.transform.position) < 0.5f || target.GetComponent<ChargesScript>() != null || target.GetComponent<SpriteRenderer>().enabled == false) {
			Destroy (gameObject);
		} else {
			this.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, Time.deltaTime * 5);		
		}
	}
		
}
