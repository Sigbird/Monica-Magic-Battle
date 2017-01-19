using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using YupiPlay;


public class SplashAnim : MonoBehaviour {

	public bool noLoading;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SplashEnd(){
		if (noLoading) {
			SceneManager.LoadScene ("Main");
		} else {
			SceneLoadingManager.LoadScene ("Main");
		}

	}
}
