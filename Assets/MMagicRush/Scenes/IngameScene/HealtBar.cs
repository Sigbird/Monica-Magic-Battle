using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealtBar : MonoBehaviour {

	public bool energy;

	public SpriteRenderer[] healtBars;
	public List<GameObject> HBars = new List<GameObject>();
	public GameObject HPoint;


	public int Life;
	public int MaxLife;


	void Start () {
		for (int i = 0; i < MaxLife; i++) {
			GameObject H = (GameObject)Instantiate (HPoint, this.transform.position, Quaternion.identity);
			H.transform.parent = this.gameObject.transform;
			HBars.Add (H);
		}
	}
	
	// Update is called once per frame
	void Update () {

		UpdateHealtbars ();


	}

	public void RefreshMaxLIfe(){
		HBars.Clear();
		foreach (Transform child in transform)
		{
			Destroy (child.gameObject);
		}
		for (int i = 0; i < MaxLife; i++) {
			GameObject H = (GameObject)Instantiate (HPoint, this.transform.position, Quaternion.identity);
			H.transform.parent = this.gameObject.transform;
			HBars.Add (H);
		}
	}

	public void UpdateHealtbars(){
//		for(int i = 0; i < HBars.; i++)
//		{
//			if (i < Life) {
//				HBars [i].color = Color.green;
//			} else {
//				HBars [i].color = Color.red;
//			}
//		}

		int x = 0;
		foreach (GameObject target in HBars){
			if (energy) {
				if (x < Life) {
					target.GetComponent<SpriteRenderer> ().color = Color.yellow;
				} else {
					target.GetComponent<SpriteRenderer> ().color = Color.gray;
				}
			} else {
				if (x < Life) {
					target.GetComponent<SpriteRenderer> ().color = Color.green;
				} else {
					target.GetComponent<SpriteRenderer> ().color = Color.red;
				}
			}
			x++;
		}
	

	}

}
