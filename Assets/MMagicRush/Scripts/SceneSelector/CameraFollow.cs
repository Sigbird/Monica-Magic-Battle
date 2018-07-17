using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	public void Update ()
	{

		if (transform.position.y < 8.10f) {
			transform.position = new Vector3 (
				transform.position.x,
				Target.transform.position.y + 4,
				transform.position.z
			);
		}
	}
}
