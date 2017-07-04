using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScroller : MonoBehaviour {

	public Vector3 buttonvelocity = Vector3.zero;
	public int scrollposition;
	public int rewardWindow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (rewardWindow == 1) {
			if (scrollposition == 1) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (550f, 163f, 0), ref buttonvelocity, 0.1f);
			}

			if (scrollposition == 2) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (-550f, 163f, 0), ref buttonvelocity, 0.1f);
			}
				
		}

		if (rewardWindow == 2) {
			if (scrollposition == 1) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (1250f, 163f, 0), ref buttonvelocity, 0.1f);
			}

			if (scrollposition == 2) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (-0f, 163f, 0), ref buttonvelocity, 0.1f);
			}

			if (scrollposition == 3) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (-1250f, 163f, 0), ref buttonvelocity, 0.1f);
			}
		}

		if (rewardWindow == 3) {
			if (scrollposition == 1) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (0f, 236f, 0), ref buttonvelocity, 0.1f);
			}

			if (scrollposition == 2) {
				GetComponent<RectTransform> ().localPosition = Vector3.SmoothDamp (this.GetComponent<RectTransform> ().localPosition, new Vector3 (-1000f, 236f, 0), ref buttonvelocity, 0.1f);
			}

		}

	}

	public void SetScrollPosition(int x){
		this.scrollposition = x;
	}

	public void GotoPosition(int x){
	
		if (x == 1) {
			GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.GetComponent<RectTransform>().localPosition, new Vector3(500f,163f,0),ref buttonvelocity , 1f);
		}

		if (x == 2) {
			GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.GetComponent<RectTransform>().localPosition, new Vector3(-500f,163f,0),ref buttonvelocity , 1f);
		}

		if (x == 3) {
			GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.GetComponent<RectTransform>().localPosition, new Vector3(-1000f,163f,0),ref buttonvelocity , 0.5f);

		}

	}
}
