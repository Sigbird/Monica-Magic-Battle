using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay {

	public class LoadingCanvasManager : CanvasAbstract {		

		new private static LoadingCanvasManager instance;

//		protected void Awake() {
//			if (instance == null) {
//				instance = this;
//				DontDestroyOnLoad(this.gameObject);
//			} else {
//				Destroy(this.gameObject);
//			}
//		}

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
			GetComponent<Animator>().SetTrigger("ToIdle");
			SceneLoadingManager.CallLoadSceneAsync();
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


