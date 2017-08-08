using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRanking : MonoBehaviour {
	public dreamloLeaderBoard LeaderBoard;
	public List<dreamloLeaderBoard.Score> LeaderList;
	public GameObject ScoreElement;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		//
		LeaderList = LeaderBoard.ToListHighToLow ();
		ShowScores ();
	}

	public void ShowScores(){
		Debug.Log ("objects: " + LeaderBoard.ToListHighToLow ().Count);
		for (int i = 0; i < LeaderList.Count; i++) {
			ScoreElement.GetComponent<ElementInfo> ().UIname.text = LeaderList [i].playerName;
			ScoreElement.GetComponent<ElementInfo> ().UIScore.text = LeaderList [i].score.ToString();
			ScoreElement.GetComponent<ElementInfo> ().UIpos.text = (i+1).ToString ();;
			Instantiate (ScoreElement, this.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
