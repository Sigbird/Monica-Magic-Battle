using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigOptions : MonoBehaviour {

	public Toggle MusicToggle;
	public Toggle EffectToggle;
	public AudioManager manager;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("GameVolume") && PlayerPrefs.GetFloat ("GameVolume") == 1) {
			MusicToggle.isOn = true;
		}else{
			MusicToggle.isOn = false;
			}

		if (PlayerPrefs.HasKey ("GameVolumeEffects") && PlayerPrefs.GetFloat ("GameVolumeEffects") == 1) {
			EffectToggle.isOn = true;
		}else{
			EffectToggle.isOn = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (MusicToggle.isOn) {
			PlayerPrefs.SetFloat ("GameVolume", 1);
			//manager.source.Play();
		} else {
			PlayerPrefs.SetFloat ("GameVolume", 0);
			//manager.source.Stop ();
		}

		if (EffectToggle.isOn) {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 1);
		} else {
			PlayerPrefs.SetFloat ("GameVolumeEffects", 0);
		}

	}
		
}
