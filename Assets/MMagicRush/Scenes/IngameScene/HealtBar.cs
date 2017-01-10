using UnityEngine;
using System.Collections;

public class HealtBar : MonoBehaviour {
	
	public SpriteRenderer[] healtBars;

	public int Life;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		UpdateHealtbars ();


	}

	public void UpdateHealtbars(){
		for(int i = 0; i < healtBars.Length; i++)
		{
			if (i < Life) {
				healtBars [i].color = Color.green;
			} else {
				healtBars [i].color = Color.red;
			}
		}


	}

}
