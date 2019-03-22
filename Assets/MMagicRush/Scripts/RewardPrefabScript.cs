using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPrefabScript : MonoBehaviour {

	public int IdPos;
	public Sprite[] images;

	// Use this for initialization
	void Start () {
		int x = Random.Range (0, images.Length - 1);
		this.GetComponent<SpriteRenderer> ().sprite = images [x];
	}
	
	// Update is called once per frame
	void Update () {
		if (IdPos == 0) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (0, 2,0), Time.deltaTime * 10);
		}
		if (IdPos == 1) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (-1.7f, 2,0), Time.deltaTime * 10);
		}
		if (IdPos == 2) {
			transform.position = Vector2.MoveTowards (this.transform.position, new Vector3 (1.7f, 2,0), Time.deltaTime * 10);
		}
	}
}
