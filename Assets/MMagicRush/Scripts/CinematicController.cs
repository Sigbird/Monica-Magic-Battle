using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour {
	public GameObject[] AnimatorsArray;
	public int AtualAnimation;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("AnimationToPlay")) {
			AtualAnimation = PlayerPrefs.GetInt ("AnimationToPlay");
		} else {
			AtualAnimation = 0;
			//SceneManager.LoadScene ("Level Select");
		}
		StartCoroutine (LateStart ());
	}

	IEnumerator LateStart(){
		yield return new WaitForSeconds (0.1f);
		AnimatorsArray [AtualAnimation].SetActive (true);
		AnimatorsArray [AtualAnimation].GetComponent<CinematicAnimation> ().ScenetoCall = "Level Select";
	}

	public void EndOfAnimation(){
		SceneManager.LoadScene ("Level Select");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
