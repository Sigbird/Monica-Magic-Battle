using UnityEngine;
using System.Collections;

public class SplashAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SplashEnd(){
		Application.LoadLevel ("Main");
	}
}
