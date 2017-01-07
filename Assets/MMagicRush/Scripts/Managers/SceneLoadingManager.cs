using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour {

	public delegate void ActivateLoading();
	public static event ActivateLoading OnActivateLoading;
	public delegate void LoadingProgress(float progress);
	public static event LoadingProgress OnLoadingProgress;

	public static SceneLoadingManager Instance {
		get { 
			return instance;
		}
	}

	private static string sceneToLoad;

	private static SceneLoadingManager instance;

	void Awake() {
		DontDestroyOnLoad(this);
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScene(string scene) {
		if (OnActivateLoading != null) {
			OnActivateLoading();
		}

		sceneToLoad = scene;	
	}

	public void CallLoadSceneAsync() {		
		StartCoroutine(LoadSceneAsync(sceneToLoad));
	}

	IEnumerator LoadSceneAsync(string scene) {		
		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (!ao.isDone) {

			if (OnLoadingProgress != null) {
				OnLoadingProgress(ao.progress / 0.9f);
			}

			if (ao.progress >= 0.9f) {
				if (LoadingCanvasManager.LoadingReady)
					ao.allowSceneActivation = true;	

			}

			yield return null;
		}

		yield return null;
	}
}
