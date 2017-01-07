using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Components {

	public class UIImageLanguageSelector : MonoBehaviour {

		[System.Serializable]
		public struct ImageLanguageObject {
			public SystemLanguage Language;
			public Sprite Image;
		}

		public Sprite DefaultImageEnglish;

		public ImageLanguageObject[] LanguageObjects;

		void Awake() {
			changeImageForLanguage();
		}

		private void changeImageForLanguage() {
			Image imageComponent = GetComponent<Image>();

			foreach(ImageLanguageObject tl in LanguageObjects) {
				if (Application.systemLanguage == tl.Language) {
					imageComponent.sprite = tl.Image;
					return;
				}
			}

			imageComponent.sprite = DefaultImageEnglish;
		}


		void OnApplicationFocus(bool hasFocus) {
			if (hasFocus) {
				changeImageForLanguage();
			}
		}

		// Use this for initialization
		void Start () {

		}
	}

}


