using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Components {

	public class UITextLanguageSelector : MonoBehaviour {

		[System.Serializable]
		public struct TextLanguageObject {
			public SystemLanguage Language;
			[Multiline]
			public string Text;
		}

		[Multiline]
		public string DefaultTextEnglish;

		public TextLanguageObject[] LanguageObjects;

		void Awake() {
			changeTextForLanguage();
		}

		private void changeTextForLanguage() {
			Text textComponent = GetComponent<Text>();

			foreach(TextLanguageObject tl in LanguageObjects) {
				if (Application.systemLanguage == tl.Language) {
					textComponent.text = tl.Text;
					return;
				}
			}

			textComponent.text = DefaultTextEnglish;
		}


		void OnApplicationFocus(bool hasFocus) {
			if (hasFocus) {
				changeTextForLanguage();
			}
		}

		// Use this for initialization
		void Start () {

		}
	}

}

