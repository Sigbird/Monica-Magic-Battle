using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicAnimation : MonoBehaviour {
	public string ScenetoCall;
	// Use this for initialization
	void Start () {
		
	}

	public void AnimationEnding(){
		SceneManager.LoadScene (ScenetoCall);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
