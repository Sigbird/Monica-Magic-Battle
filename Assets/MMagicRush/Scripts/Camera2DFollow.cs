using UnityEngine;
using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public float yPosRestriction = -1; //Stops the camera to follow at -1 
	public float MinXPosRestriction;
	public float MaxXPosRestriction;
	public float MinYPosRestriction;
	public float MaxYPosRestriction;


	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;

	float nextTimeToSearch = 0;

	// Use this for initialization
	void Start () {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}

	// Update is called once per frame
	void Update () {

		if (target == null) {
			FindPlayer ();
			return;
		}

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);    
		}

		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

		newPos = new Vector3 (Mathf.Clamp(newPos.x, MinXPosRestriction, MaxXPosRestriction), Mathf.Clamp (newPos.y, MinYPosRestriction, MaxYPosRestriction), newPos.z);

		transform.position = newPos;

		lastTargetPosition = target.position;        
	}

	void FindPlayer () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.Find ("Hero");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}
	}
}