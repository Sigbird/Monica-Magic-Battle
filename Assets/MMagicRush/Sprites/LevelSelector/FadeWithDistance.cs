using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeWithDistance : MonoBehaviour {

	public GameObject target;
	public float proximity;
	public float percentage;
	public Image imagem;
	public Image imagem1;
	public Image imagem2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		proximity = Vector2.Distance (this.transform.position, target.transform.position) - 1.0f;
		percentage = proximity / 10;
		if (proximity < 5) {
			imagem.color = new Color (1, 1, 1, 1);
			imagem1.color = new Color (1, 1, 1, percentage);
			imagem2.color = new Color (1, 1, 1, percentage);
		} else {
//			imagem1.color = new Color (1, 1, 1, 1);
//			imagem2.color = new Color (1, 1, 1, 1);
		}
	}
}
