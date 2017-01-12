using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using YupiPlay;


public class SplashAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SplashEnd(){
		SceneManager.LoadScene ("Main");
	}
}
