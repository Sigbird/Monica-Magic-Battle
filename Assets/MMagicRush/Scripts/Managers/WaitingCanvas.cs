using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
using GooglePlayGames.BasicApi.Multiplayer;
#endif

namespace YupiPlay {
    public class WaitingCanvas : CanvasAbstract {
        public Text StatusTitle;
        public GameObject LoadingProgression;

		private WaitingCanvas instance;

		protected void Awake() {
			if (instance == null) {
				instance = this;
				DontDestroyOnLoad(this.gameObject);
			} else {
				Destroy(this.gameObject);
			}
		}
        
        protected void ResetLoading() {
            base.ResetLoading();
            LoadingProgression.SetActive(false);
        }

        public void ShowWaitingScreen() {
            ResetLoading();
            Container.SetActive(true);
            StatusTitle.text = "Searching...";
        }

        public void OnSetupProgress(float progress) {                 
            StatusTitle.text = "Found match. Setting up..";

            if (progress > 0f) {
                if (!LoadingProgression.activeInHierarchy) {
                    LoadingProgression.SetActive(true);
                }

                StatusTitle.text = "Preparing Match";

                Percentage.text = Mathf.RoundToInt(progress).ToString() + "%";
                ProgressBar.fillAmount = progress / 100f;
            }
        }

        public void OnRoomConnectedSuccess() {
            Container.SetActive(false);            
        }

        public void OnRoomConnectedFailure() {
            StatusTitle.text = "Error";
            StartCoroutine(CloseAfterError());
        }

        IEnumerator CloseAfterError() {
            yield return new WaitForSeconds(2);
            Container.SetActive(false);
        }

		#if UNITY_ANDROID
		public void OnParticipantLeft(Participant participant) {
			StatusTitle.text = "Participant left";
			StartCoroutine(CloseAfterError());
		}
		#endif

        void OnEnable() {
            #if UNITY_ANDROID
            GoogleMultiplayer.OnWaitingForGame += ShowWaitingScreen;
            GoogleMultiplayer.OnGameSetupProgress += OnSetupProgress;
            GoogleMultiplayer.OnRoomConnectedSuccess += OnRoomConnectedSuccess;
            GoogleMultiplayer.OnRoomConnectedFailure += OnRoomConnectedFailure;
			GoogleMultiplayer.OnParticipantLeftGame += OnParticipantLeft;
            #endif
        }

        void OnDisable() {
            #if UNITY_ANDROID
            GoogleMultiplayer.OnWaitingForGame -= ShowWaitingScreen;
            GoogleMultiplayer.OnGameSetupProgress -= OnSetupProgress;
            GoogleMultiplayer.OnRoomConnectedSuccess -= OnRoomConnectedSuccess;
            GoogleMultiplayer.OnRoomConnectedFailure -= OnRoomConnectedFailure;
            #endif
        }
    }

}

