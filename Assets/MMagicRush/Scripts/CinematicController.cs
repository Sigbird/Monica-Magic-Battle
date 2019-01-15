using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour {
	public GameObject[] AnimatorsArray;
	public GameObject[] TextWindowsArray;
	public int AtualAnimation;
	public int textinput;
	public GameObject text1a;
	public GameObject text1b;
	public GameObject text1c;
	public GameObject text1d;
	public GameObject text2a;
	public GameObject text2b;
	public GameObject text2c;
	public GameObject text2d;
	public AudioManager manager;
	// Use this for initialization
	void Start () {
		textinput = 0;
//		if (PlayerPrefs.HasKey ("PlayedIntro") == false) {
//			AtualAnimation = 0;
//			PlayerPrefs.SetString ("PlayedIntro", "true");
//			StartCoroutine (LateStart ());
//
//		} else {
			if (PlayerPrefs.HasKey ("AnimationToPlay")) {
			AtualAnimation = PlayerPrefs.GetInt ("AnimationToPlay");

		} else {
				AtualAnimation = 0;
				//SceneManager.LoadScene ("Level Select");
			}
			PlayerPrefs.DeleteKey ("AnimationToPlay");
			StartCoroutine (LateStart ());
		//}

	}

	IEnumerator LateStart(){
		yield return new WaitForSeconds (0.1f);
		AnimatorsArray [AtualAnimation].SetActive (true);
		TextWindowsArray [AtualAnimation].SetActive (true);
		AnimatorsArray [AtualAnimation].GetComponent<CinematicAnimation> ().ScenetoCall = "Level Select";
		manager.PlayAudio ("cinematics"+AtualAnimation.ToString());


	}

	public void EndOfAnimation(){
		SceneManager.LoadScene ("Level Select");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SkipAnimation(){
		textinput++;
		if (textinput == 1) {
			text1a.SetActive (false);
			text1b.SetActive (false);
			text1c.SetActive (false);
			text1d.SetActive (false);
			text2a.SetActive (true);
			text2b.SetActive (true);
			text2c.SetActive (true);
			text2d.SetActive (true);
		} else if (textinput == 2) {
			SceneManager.LoadScene ("Level Select");
		} 
	}
}
