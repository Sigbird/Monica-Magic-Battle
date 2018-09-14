using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSanp : MonoBehaviour {

	// Use this for initialization
	//Public Variable
	public RectTransform ScrollPanel;
	public Button[] _Bttn;
	public RectTransform _center;
	public ScrollRect _RectPanel;

	// Private Variable

	private float[] _distance;
	private bool isDragging = false;
	private int bttnDistance;
	private int minButtonNum;
	private bool isRunning = false;
	private bool isEnable = false;
	private int cleared;

	public int idx;

	void Start () {
		
		isEnable = false;
		int bttnLenght = _Bttn.Length;
		_distance=new float[bttnLenght];

		bttnDistance = (int)Mathf.Abs (_Bttn[1].GetComponent<RectTransform>().anchoredPosition.x-
			_Bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
		
		 


		
	}
	
	// Update is called once per frame
	void Update () {

		cleared = PlayerPrefs.GetInt ("ClearedLevels");

		for (int i = 0; i < _Bttn.Length; i++) {

			_distance [i] = Mathf.Abs (_center.anchoredPosition.x-_Bttn[i].transform.position.x);


		}

		float minDistance = Mathf.Min (_distance);

		for (int k = 0; k < _Bttn.Length; k++) {

			if (minDistance == _distance [k]) {

				minButtonNum = k;

				Debug.Log (k);

				ScaleUpAndScaleDown (k);


			}

		}


		if ((!isRunning)) {

			LerpToTargetPosition (minButtonNum * -bttnDistance);
		} 
	
	
		
	}


	public void LerpToTargetPosition(int pos){



		float newX = Mathf.Lerp (ScrollPanel.anchoredPosition.x,pos,Time.deltaTime*5f);

		Vector2 newPosition = new Vector2 (newX, ScrollPanel.anchoredPosition.y);

		ScrollPanel.anchoredPosition = newPosition;

	}

	public void Onvaluechange()
	{
		//GameObject.Find ("MapManager").GetComponent<MapManager> ().ChangeMap ("Next");
//		if (_RectPanel.velocity.x > 0f)
		{
//			Debug.Log (_RectPanel.velocity);

			if (Mathf.Abs(_RectPanel.velocity.x) > 100.0f) {
				GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (false);
				isRunning = true;
			} else {
				//GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (true);
				StartCoroutine(ActivateCharacter(true));
				isRunning = false;
			}
		}

		isEnable = true;
	}

	IEnumerator ActivateCharacter(bool x){
		yield return new WaitForSeconds (0.5f);
		if (x == true) {
			if (idx == 1 && cleared > 4)
				GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (x);
			if (idx == 2 && cleared > 8)
				GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (x);
			if (idx == 0)
				GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (x);
		} else {
			GameObject.Find ("MapManager").GetComponent<MapManager> ().Character.gameObject.SetActive (x);
		}

	}

	Vector3 scale =new  Vector3(0.0085f,0.0085f,0.0085f);
	void ScaleUpAndScaleDown(int index){
		idx = index;
		for (int i = 0; i < _Bttn.Length; i++) {

			if (i == index) {
				if (_Bttn [i].GetComponent<RectTransform> ().localScale.x <= 1.0f) {

					_Bttn [i].GetComponent<RectTransform> ().localScale += Vector3.Lerp (scale, _Bttn [i].GetComponent<RectTransform> ().localScale, Time.deltaTime * 0.5f);


				}

			} else {
				if (_Bttn [i].GetComponent<RectTransform> ().localScale.x >= 0.85f) {
					
					_Bttn [i].GetComponent<RectTransform> ().localScale -= Vector3.Lerp (scale, _Bttn [i].GetComponent<RectTransform> ().localScale, Time.deltaTime * 0.5f);
//					
				}

			}

		}

	}
}
