using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	public AudioClip[] audios;
	public AudioClip[] clickaudios;
	public AudioClip[] reactions;
	public AudioClip[] endingGameSounds;
	public AudioSource source;
	public float MusicVolume;
	public float EffectVolume;
	public string terraintype;
	public bool boss;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("GameVolume")) {
			MusicVolume = 0.4f;//PlayerPrefs.GetFloat ("GameVolume");
		} else {
			MusicVolume = 0.4f;
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


	public void OvertimePlayAudio(){


		source.PlayOneShot (endingGameSounds [Random.Range(0,2)], EffectVolume);

	}

	public void PlayAudio(string track){

		if (EffectVolume > 0) {

			if (track == "reactions_monica") { 
				source.PlayOneShot (reactions [0],EffectVolume);
			}
			if (track == "reactions_cebolinha") { 
				source.PlayOneShot (reactions [1],EffectVolume);
			}

			if (track == "reactions_magali") { 
				source.PlayOneShot (reactions [2],EffectVolume);
			}

			if (track == "reactions_cascao") { 
				source.PlayOneShot (reactions [3],EffectVolume);
			}

			if (track == "cabrum") { 
				source.PlayOneShot (audios [0],EffectVolume);
			}
			if (track == "death") { //ok
				source.PlayOneShot (audios [1],EffectVolume);
			}
			if (track == "shot") { //ok
				source.PlayOneShot (audios [3],EffectVolume);
//				Debug.Log ("Pow "+ EffectVolume);
			}
			if (track == "spawn") { //ok 
				source.PlayOneShot (audios [4],EffectVolume);
			}
			if (track == "atack") { //ok
				source.PlayOneShot (audios [5],EffectVolume);
			}
			if (track == "tower") { //ok
				source.PlayOneShot (audios [6],EffectVolume);
			}
			if (track == "button") {
				int y = Random.Range (0, clickaudios.Length-1);
				source.PlayOneShot (clickaudios [y],EffectVolume);
			}
			if (track == "chuva2") {
				source.PlayOneShot (audios [11],EffectVolume);
			}
			if (track == "tadan") {
				source.PlayOneShot (audios [12],EffectVolume);
			}
			if (track == "terremoto") {
				source.PlayOneShot (audios [13],EffectVolume);
			}
			if (track == "nervosa") {
				source.PlayOneShot (audios [14],EffectVolume);
			}
			if (track == "nao") {
				source.PlayOneShot (audios [15],EffectVolume);
			}
			if (track == "alivio") {
				source.PlayOneShot (audios [16],EffectVolume);
			}
			if (track == "suspiro") {
				source.PlayOneShot (audios [17],EffectVolume);
			}
			if (track == "passos") {
				source.PlayOneShot (audios [26],EffectVolume);
			}
			if (track == "selecaocarta") {
				source.PlayOneShot (audios [24],EffectVolume);
			}
			if (track == "selecaofase") {
				source.PlayOneShot (audios [25],EffectVolume);
			}
			if (track == "cinematics0") {
				source.PlayOneShot (audios [27],EffectVolume);
			}
			if (track == "cinematics1") {
				source.PlayOneShot (audios [28],EffectVolume);
			}
			if (track == "cinematics2") {
				source.PlayOneShot (audios [28],EffectVolume);
			}
			if (track == "cinematics3") {
				source.PlayOneShot (audios [29],EffectVolume);
			}
			if (track == "openChest") {
				source.PlayOneShot (audios [30],EffectVolume);
			}


		}
		if (MusicVolume > 0) {
			if (track == "victory") { //ok
				source.clip = null;
				source.PlayOneShot (audios [7],MusicVolume-0.6f);
			}
			if (track == "defeat") { //ok
				source.clip = null;
				source.PlayOneShot (audios [2],MusicVolume-0.6f);
//				source.Stop();
//				source.loop = true;
//				source.clip = audios [2];
//				source.Play ();
			}
			if (track == "menu") {
				source.loop = true;
				source.volume = MusicVolume;
				source.clip = audios [9];
				source.Play ();

			}

			if (track == "ingame") {
				//StartCoroutine (LatePlayAudio ());
				source.loop = true;
				source.volume = MusicVolume;
				if (SceneManager.GetActiveScene ().name == "JogoOffline" || SceneManager.GetActiveScene ().name == "GamePlayReview" || SceneManager.GetActiveScene ().name == "TutorialScene" ) {
					if(StaticController.instance.GameController.GameOver == false){ // CENAS DE GAMEPLAY
						if (terraintype == "Forest") {
							if (boss) {
								source.clip = audios [21];
							} else {
								source.clip = audios [18];
							}
						} else if (terraintype == "Winter") {
							if (boss) {
								source.clip = audios [22];
							} else {
								source.clip = audios [19];
							}
						} else if (terraintype == "Dungeon") {
							if (boss) {
								source.clip = audios [23];
							} else {
								source.clip = audios [20];
							}
						} else {
							source.clip = audios [21];
						}
					}
				} else if(SceneManager.GetActiveScene ().name == "Cinematics"){

				}else{ 																// CENAS DE MENU
					source.clip = audios [9];
				}
				source.volume = MusicVolume - 0.2f;
				source.Play ();
			}
		}


	}

	IEnumerator LatePlayAudio(){
		yield return new WaitForSeconds (0.2f);
		source.loop = true;
		source.volume = MusicVolume;
		if (SceneManager.GetActiveScene ().name == "JogoOffline" || SceneManager.GetActiveScene ().name == "GamePlayReview" || SceneManager.GetActiveScene ().name == "TutorialScene" ) {
			if(StaticController.instance.GameController.GameOver == false){ // CENAS DE GAMEPLAY
				if (terraintype == "Forest") {
					if (boss) {
						source.clip = audios [21];
					} else {
						source.clip = audios [18];
					}
				} else if (terraintype == "Winter") {
					if (boss) {
						source.clip = audios [22];
					} else {
						source.clip = audios [19];
					}
				} else if (terraintype == "Dungeon") {
					if (boss) {
						source.clip = audios [23];
					} else {
						source.clip = audios [20];
					}
				} else {
					source.clip = audios [21];
				}
			}
		} else if(SceneManager.GetActiveScene ().name == "Cinematics"){

		}else{ 																// CENAS DE MENU
			source.clip = audios [9];
		}
		source.volume = MusicVolume - 0.2f;
		source.Play ();
	}

}
