using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpeningSequence : MonoBehaviour {
	public GameObject[] RewardPrefab;
	public GameObject ChestImage;
	public ChestManagement manager;
	public GameObject ReturnPanel;
	public Button chestOpener;
	public SceneHelper Helper;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		chestOpener.interactable = true;
	}


	IEnumerator DeliverRewards(){
		yield return new WaitForSeconds (1f);
		RewardPrefab [0].GetComponent<RewardPrefabScript> ().Controller = Helper;
		RewardPrefab[0].GetComponent<RewardPrefabScript> ().IdPos = 0;
		RewardPrefab[3] = Instantiate (RewardPrefab[0], new Vector3(0,-2,0), Quaternion.identity);
		yield return new WaitForSeconds (0.5f);
		RewardPrefab [1].GetComponent<RewardPrefabScript> ().Controller = Helper;
		RewardPrefab[1].GetComponent<RewardPrefabScript> ().IdPos = 1;
		RewardPrefab[4] = Instantiate (RewardPrefab[1], new Vector3(0,-2,0), Quaternion.identity);
		yield return new WaitForSeconds (0.5f);
		RewardPrefab[2].GetComponent<RewardPrefabScript> ().IdPos = 2;
		RewardPrefab[5] =(GameObject) Instantiate (RewardPrefab[2], new Vector3(0,-2,0), Quaternion.identity);
	}

	public void OpenChest(){
		StartCoroutine (DeliverRewards ());
		ReturnPanel.SetActive (true);
//		this.GetComponent<Button> ().interactable = false;
	}

	public void ReturnToMenu(){
		Destroy (RewardPrefab [3].gameObject);
		Destroy (RewardPrefab [4].gameObject);
		Destroy (RewardPrefab [5].gameObject);

		int x = PlayerPrefs.GetInt ("ChestsNumber");


		PlayerPrefs.SetInt ("ChestsNumber", x -= 1);

		manager.UpdateChests ();
		this.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
