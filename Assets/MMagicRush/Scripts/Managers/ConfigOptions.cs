using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigOptions : MonoBehaviour {

	public Toggle MusicToggle;
	public Toggle EffectToggle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MusicToggle.isOn) {
			PlayerPrefs.SetFloat ("GameVolume", 1);
		} else {
			PlayerPrefs.SetFloat ("GameVolume", 0);
		}

		if (EffectToggle.isOn) {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 1);
		} else {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 0);
		}

	}
		
}
