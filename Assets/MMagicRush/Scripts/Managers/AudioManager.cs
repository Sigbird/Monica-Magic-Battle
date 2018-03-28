using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	public AudioClip[] audios;
	public AudioSource source;
	public float MusicVolume;
	public float EffectVolume;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("GameVolume")) {
			MusicVolume = PlayerPrefs.GetFloat ("GameVolume");
		} else {
			MusicVolume = 1;
		}

		if (PlayerPrefs.HasKey ("GameVolumeEffects")) {
			EffectVolume = PlayerPrefs.GetFloat ("GameVolumeEffects");
		}else {
			EffectVolume = 1;
		}
		source = this.GetComponent<AudioSource> ();
		PlayAudio ("ingame");
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerPrefs.GetFloat ("GameVolume") == 1) {
			MusicVolume = 1;
			if (source.isPlaying == false) {
				PlayAudio ("ingame");
			}
		} else {
			StopAudio();
			MusicVolume = 0;
		}

		if (PlayerPrefs.GetFloat ("GameVolumeEffects") == 1) {
			EffectVolume = 1;
		}else {
			EffectVolume = 0;
		}

	}

	public void StopAudio(){
		source.Stop ();
	}

	public void SetVolume(float x){
		source.volume = x;
	}

	public void PlayAudio(string track){

		if (EffectVolume > 0) {
			if (track == "magic") { 
				source.PlayOneShot (audios [0]);
			}
			if (track == "death") { //ok
				source.PlayOneShot (audios [1]);
			}
			if (track == "shot") { //ok
				source.PlayOneShot (audios [3]);
			}
			if (track == "spawn") { //ok 
				source.PlayOneShot (audios [4]);
			}
			if (track == "atack") { //ok
				source.PlayOneShot (audios [5]);
			}
			if (track == "tower") { //ok
				source.PlayOneShot (audios [6]);
			}
			if (track == "button") {
				source.PlayOneShot (audios [8]);
			}
		}
		if (MusicVolume > 0) {
			if (track == "victory") { //ok
				source.Stop();
				source.PlayOneShot (audios [7]);
			}
			if (track == "defeat") { //ok
				source.Stop();
				source.loop = true;
				source.clip = audios [2];
				source.Play ();
			}
			if (track == "menu") {
				source.loop = true;
				source.clip = audios [9];
				source.Play ();
			}
			if (track == "ingame" && StaticController.instance.GameController.GameOver == false) {
				source.loop = true;
				if (SceneManager.GetActiveScene ().name == "Main") {
					source.clip = audios [9];
				} else {
					source.clip = audios [10];
				}
				source.Play ();
			}
		}


	}

}
