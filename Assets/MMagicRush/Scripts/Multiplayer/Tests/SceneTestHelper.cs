using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTestHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void LoadGame() {
		SceneManager.LoadSceneAsync("JogoMulti");
	}

    public static void LoadTestGame() {
        SceneManager.LoadSceneAsync("ProtoGame");
    }

    public static void LoadMenu() {
        SceneManager.LoadSceneAsync("ProtoMenu");
    }
}
