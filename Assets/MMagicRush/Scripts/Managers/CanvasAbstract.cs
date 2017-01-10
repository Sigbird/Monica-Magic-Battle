using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay {

    public class CanvasAbstract : MonoBehaviour {
        public GameObject Container;
        public Text Percentage;
        public Image ProgressBar;

        protected static GameObject instance = null;

        protected void Awake() {
            if (instance == null) {
                instance = this.gameObject;
                DontDestroyOnLoad(this.gameObject);
            } else {
                Destroy(this.gameObject);
            }
        }

        protected void Start () {
            ResetLoading();
        }

        protected void ResetLoading() {            
            Percentage.text = "0%";
            ProgressBar.fillAmount = 0;
        }

    }

}

