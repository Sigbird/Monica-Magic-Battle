using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarSolid : MonoBehaviour {
	public Scrollbar bar;
	public float maxValue;
	public float atualValue;
	public float percentage;
	// Use this for initialization
	void Start () {
		bar = GetComponent<Scrollbar> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (atualValue <= maxValue) {
			percentage = (atualValue / maxValue);
			bar.size = percentage;
		}
	}
}
