using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SplashAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SplashEnd(){
		SceneLoadingManager.Instance.LoadScene("Main");
	}
}
