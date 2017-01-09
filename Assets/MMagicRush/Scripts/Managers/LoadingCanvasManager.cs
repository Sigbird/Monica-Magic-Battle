using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay {

	public class LoadingCanvasManager : MonoBehaviour {
		public GameObject LoadingContainer;
		public Text Percentage;
		public Image ProgressBar;

		//	private float timeAmount = 5;
		//	private float time = 0;
		//
		private static GameObject instance = null;

		public static bool LoadingReady = false;

		void Awake() {
			if (instance == null) {
				instance = this.gameObject;
				DontDestroyOnLoad(this.gameObject);
			} else {
				Destroy(this.gameObject);
			}
		}

		// Use this for initialization
		void Start () {
			ResetLoading();
			//GetComponent<Animator>().SetTrigger("ActivateLoading");
		}

		// Update is called once per frame
		void Update () {
			//		TESTING		
			//		if (loadingReady) {
			//			time += Time.fixedDeltaTime;
			//
			//			float percentage = time / timeAmount;
			//			ProgressBar.fillAmount = percentage;
			//			float total = percentage * 100;
			//			int totalInt = Mathf.RoundToInt(total);
			//			Percentage.text = totalInt.ToString() + "%";
			//
			//			if (time >= timeAmount) {
			//				GetComponent<Animator>().SetTrigger("ActivateLoading");
			//				return;
			//			}
			//		}


		}		

		public void ToIdle() {				
			LoadingReady = true;
			GetComponent<Animator>().SetTrigger("ToIdle");
			SceneLoadingManager.Instance.CallLoadSceneAsync();
		}

		public void ResetLoading() {
			LoadingReady = false;
			Percentage.text = "0%";
			ProgressBar.fillAmount = 0;
			//time = 0;
		}


		private void OnActivateLoadingEvent() {	
			ResetLoading()	;
			GetComponent<Animator>().SetTrigger("ActivateLoading");

		}

		private void OnLoadingProgressEvent(float progress) {
			ProgressBar.fillAmount = progress;
			Percentage.text = Mathf.RoundToInt(progress * 100).ToString() + "%";

			if (progress >= 1) {
				GetComponent<Animator>().SetTrigger("DeactivateLoading");
			}
		}

		void OnEnable() {
			SceneLoadingManager.OnActivateLoading += OnActivateLoadingEvent;
			SceneLoadingManager.OnLoadingProgress += OnLoadingProgressEvent;
		}

		void OnDisable() {
			SceneLoadingManager.OnActivateLoading -= OnActivateLoadingEvent;
			SceneLoadingManager.OnLoadingProgress -= OnLoadingProgressEvent;
		}
	}

}


