using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using YupiPlay;


public class SplashAnim : MonoBehaviour {
	public bool noLoading;
	public string firstscene;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetFloat ("GameVolume", 1);
		PlayerPrefs.SetFloat ("GameVolumeEffects", 1);

		PlayerPrefs.SetInt ("Lesson", 1);
		PlayerPrefs.SetString ("Tutorial", "False");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SplashEnd(){
		if (noLoading) {
			SceneManager.LoadScene (firstscene);
		} else {
			SceneLoadingManager.LoadScene (firstscene);
		}
	}
}
