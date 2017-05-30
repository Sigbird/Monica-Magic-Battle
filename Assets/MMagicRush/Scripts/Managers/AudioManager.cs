using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioClip[] audios;
	public AudioSource source;
	// Use this for initialization
	void Start () {
		source = this.GetComponent<AudioSource> ();
		PlayAudio ("ingame");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StopAudio(){
		source.Stop ();
	}

	public void SetVolume(float x){
		source.volume = x;
	}

	public void PlayAudio(string track){

		if (track == "magic") { 
			source.PlayOneShot (audios[0]);
		}
		if (track == "death") { //ok
			source.PlayOneShot (audios[1]);
		}
		if (track == "defeat") { //ok
			source.PlayOneShot (audios[2]);
		}
		if (track == "shot") { //ok
			source.PlayOneShot (audios[3]);
		}
		if (track == "spawn") { //ok 
			source.PlayOneShot (audios[4]);
		}
		if (track == "atack") { //ok
			source.PlayOneShot (audios[5]);
		}
		if (track == "tower") { //ok
			source.PlayOneShot (audios[6]);
		}
		if (track == "victory") { //ok
			source.PlayOneShot (audios[7]);
		}
		if (track == "button") {
			source.PlayOneShot (audios[8]);
		}
		if (track == "menu") {
			source.loop = true;
			source.clip = audios [9];
			source.Play ();
		}
		if (track == "ingame") {
			source.loop = true;
			source.clip = audios [10];
			source.volume = 0.08f;
			source.Play ();
		}


	}

}
