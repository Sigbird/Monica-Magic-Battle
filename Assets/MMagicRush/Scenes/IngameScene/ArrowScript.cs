using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	public GameObject HitAnimationObject;
	public GameObject target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;

	public bool leftcurve;

	public float curve;

	public Sprite[] Images;

	public Transform Projectile;      
	private Transform myTransform;
	public Vector2 targetpositionstatic;
	public bool SkillShot;
	// Use this for initialization

	public int type;

	void Awake()
	{
		GetComponent<SpriteRenderer> ().sprite = Images [type];

		myTransform = transform; 
		Projectile = transform;
	}

	void Start () {
		if (transform.position.x > target.transform.position.x) {
			leftcurve = false;
		} else {
			leftcurve = true;
		}
		curve = (Vector2.Distance (transform.position, target.transform.position)-0.5f);
		//StartCoroutine (SimulateProjectile ());
	}
	
	// Update is called once per frame
	void Update () {
//		if (target != null) {
//			if (Vector3.Distance (this.transform.position, target.transform.position) < 1f || target.GetComponent<ChargesScript> () != null || target.GetComponent<SpriteRenderer> ().enabled == false) {
//				Instantiate (HitAnimationObject, target.transform);				
//				Destroy (gameObject);
//			}
//		}
		if(target.GetComponent<WPIASoldierControler>() != null){
			if(target.GetComponent<WPIASoldierControler>().alive == false){
				Destroy (this.gameObject);
			}
		}

		if(target.GetComponent<WPSoldierControler>() != null){
			if(target.GetComponent<WPSoldierControler>().alive == false){
				Destroy (this.gameObject);
			}
		}




		if (target != null) {
			Vector3 relativePos = transform.position - target.transform.position;

			float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = q;

			if (Vector3.Distance (this.transform.position, target.transform.position) < 0.1f || target == null) {
				Destroy (this.gameObject);
			} else {
				this.transform.position = Vector3.MoveTowards (this.transform.position,target.transform.position, Time.deltaTime * 5);		
//				if(leftcurve == false)
//				transform.Translate (Vector2.up * Time.deltaTime * curve);
//
//				if(leftcurve == true)
//					transform.Translate (Vector2.down * Time.deltaTime * curve);
//
//				if(curve>0)
//				curve -= Time.deltaTime * 4;
			}
		} else {
			Destroy (this.gameObject);
		}
	}

	IEnumerator SimulateProjectile()
	{
		// Short delay added before Projectile is thrown
		yield return new WaitForSeconds(0.01f);

		// Move projectile to the position of throwing object + add some offset if needed.
		Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

		// Calculate distance to target
		float target_Distance = Vector3.Distance(Projectile.position, target.transform.position);

		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;

		// Rotate projectile to face the target.
		Projectile.right = (target.transform.position - Projectile.position);
		//Projectile.rotation = Quaternion.LookRotation(target.transform.position - Projectile.position);

		float elapse_time = 0;

		while (elapse_time < flightDuration)
		{
			Projectile.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);

			elapse_time += Time.deltaTime;

			yield return null;
		}
		Instantiate (HitAnimationObject,target.transform);
		Destroy (this.gameObject);
	}
} 
		

