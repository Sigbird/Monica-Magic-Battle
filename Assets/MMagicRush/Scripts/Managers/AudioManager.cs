using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	public AudioClip[] audios;
	public AudioSource source;
	public float MusicVolume;
	public float EffectVolume;
	public string terraintype;
	public bool boss;
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
		terraintype = PlayerPrefs.GetString ("TerrainType");



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
			if (track == "cabrum") { 
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
			if (track == "chuva2") {
				source.PlayOneShot (audios [11]);
			}
			if (track == "tadan") {
				source.PlayOneShot (audios [12]);
			}
			if (track == "terremoto") {
				source.PlayOneShot (audios [13]);
			}
			if (track == "nervosa") {
				source.PlayOneShot (audios [14]);
			}
			if (track == "nao") {
				source.PlayOneShot (audios [15]);
			}
			if (track == "alivio") {
				source.PlayOneShot (audios [16]);
			}
			if (track == "suspiro") {
				source.PlayOneShot (audios [17]);
			}
			if (track == "passos") {
				source.PlayOneShot (audios [18]);
			}
			if (track == "selecaocarta") {
				source.PlayOneShot (audios [24]);
			}
			if (track == "selecaofase") {
				source.PlayOneShot (audios [25]);
			}

		}
		if (MusicVolume > 0) {
			if (track == "victory") { //ok
				source.Stop();
				source.PlayOneShot (audios [7]);
			}
			if (track == "defeat") { //ok
				source.Stop();
				source.PlayOneShot (audios [2]);
//				source.Stop();
//				source.loop = true;
//				source.clip = audios [2];
//				source.Play ();
			}
			if (track == "menu") {
				source.loop = true;
				source.clip = audios [9];
				source.Play ();
			}

			if (track == "ingame") {
				source.loop = true;

				if (SceneManager.GetActiveScene ().name == "JogoOffline" || SceneManager.GetActiveScene ().name == "GamePlayReview" || SceneManager.GetActiveScene ().name == "TutorialScene") {
						if(StaticController.instance.GameController.GameOver == false){ // CENAS DE GAMEPLAY
							if (terraintype == "Forest") {
								if (boss) {
									source.clip = audios [18];
								} else {
									source.clip = audios [21];
								}
							} else if (terraintype == "Winter") {
								if (boss) {
									source.clip = audios [19];
								} else {
									source.clip = audios [22];
								}
							} else if (terraintype == "Dungeon") {
								if (boss) {
									source.clip = audios [20];
								} else {
									source.clip = audios [23];
								}
							} else {
								source.clip = audios [10];
							}
						}
				} else { 																// CENAS DE MENU
					source.clip = audios [9];
				}
				source.Play ();
			}
		}


	}

}
