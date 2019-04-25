using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using YupiPlay;
using UnityEngine.UI;


public class SplashAnim : MonoBehaviour {
	public bool noLoading;
	public string firstscene;
	public GameObject LoadingCanvas;
	public Scrollbar ProgressionBar;
	public GameObject VidIntro;
	public GameObject Logos;
	public GameObject SkipPannel;
	public AudioClip IntroAudio;
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey ("VideoIntro");

		PlayerPrefs.SetFloat ("GameVolume", 1);
		PlayerPrefs.SetFloat ("GameVolumeEffects", 1);

		PlayerPrefs.SetInt ("Lesson", 1);
		PlayerPrefs.SetString ("Tutorial", "False");
	}

	void Awake(){
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAnimAudio(){
		GetComponent<AudioSource> ().PlayOneShot (IntroAudio);
	}

	public void VideoIntro(){


		if (PlayerPrefs.HasKey ("VideoIntro")) {
			SplashEnd ();
		}else{
			Logos.SetActive (false);
			VidIntro.SetActive (true);
			SkipPannel.SetActive (true);
			PlayerPrefs.SetInt("VideoIntro",1);
		}


	}

	public void SplashEnd(){
		SkipPannel.SetActive (false);
		if (noLoading) {
			SceneManager.LoadScene (firstscene);
		} else {
			GetComponent<AudioSource> ().clip = null;
			GetComponent<AudioSource> ().Stop ();
			StartCoroutine (AsynchronousLoad (firstscene));
			//SceneLoadingManager.LoadScene (firstscene);
		}
	}


	IEnumerator AsynchronousLoad (string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (! ao.isDone)
		{
			LoadingCanvas.SetActive (true);
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");
			ProgressionBar.size = progress;
			// Loading completed
			if (ao.progress == 0.9f)
			{
				Debug.Log("Press a key to start");
				//if (Input.GetKeyDown(KeyCode.A))
				if (LoadingCanvas != null) {
					//Destroy (LoadingCanvas.gameObject);
				}
					ao.allowSceneActivation = true;
				//ao.isDone = true;
			}

			yield return null;
		}
	}

}
