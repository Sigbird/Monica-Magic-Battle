using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTestHelper : MonoBehaviour {	

    public static void LoadTestGame() {
        SceneManager.LoadSceneAsync("ProtoGame");
    }

    public static void LoadMenu() {
        SceneManager.LoadSceneAsync("ProtoMenu");
    }
}
