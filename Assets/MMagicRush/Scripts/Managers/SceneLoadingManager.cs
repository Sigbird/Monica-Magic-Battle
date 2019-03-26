using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YupiPlay {

	public class SceneLoadingManager : MonoBehaviour {

		public delegate void ActivateLoading();
		public static event ActivateLoading OnActivateLoading;
		public delegate void LoadingProgress(float progress);
		public static event LoadingProgress OnLoadingProgress;

		private static string sceneToLoad;

		private static GameObject staticGameObject;
		private static SceneLoadingManager instance;

//		void Awake() {
//			if (staticGameObject == null) {
//				staticGameObject = this.gameObject;
//				instance = staticGameObject.GetComponent<SceneLoadingManager>();
//				DontDestroyOnLoad(this.gameObject);
//			} else {
//				Destroy(this.gameObject);
//			}				
//		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}			

		public static void LoadScene(string scene) {
			instance.loadScene(scene);
		}

		private void loadScene(string scene) {
			if (OnActivateLoading != null) {
				OnActivateLoading();
			}

			sceneToLoad = scene;	
		}

		public static void CallLoadSceneAsync() {
			instance.callLoadSceneAsync();
		}

		private void callLoadSceneAsync() {		
			StartCoroutine(LoadSceneAsync(sceneToLoad));
		}

		private IEnumerator LoadSceneAsync(string scene) {		
			AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
			ao.allowSceneActivation = false;

			while (!ao.isDone) {

				if (OnLoadingProgress != null) {
					OnLoadingProgress(ao.progress / 0.9f);
				}

				if (ao.progress >= 0.9f) {					
					ao.allowSceneActivation = true;	
				}

				yield return null;
			}

			yield return null;
		}
	}

}


