using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;
	public GameObject[] Maps;
	public GameObject[] WaypointsMapForest;
	public GameObject[] WaypointsMapIce;
	public GameObject[] WaypointsMapCastle;
	public GameObject[] WaypointsAll;
	public int actualMap;
	public GameObject[] NextButton;
	public GameObject[] FirstPins;
	public GameObject[] SecondPins;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	public void Start ()
	{
		actualMap = 0;
		//PlayerPrefs.SetInt ("ClearedLevels", 0);
		// Pass a ref and default the player Starting Pin
		UpdateLevels();
		Character.Initialise(this, StartPin);
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (Input.GetMouseButtonUp (0)) {
			if (Character.NextPin () != null) {
				if (Character.NextPin ().ActualStatus != PinStatus.Locked) {
					if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), Character.NextPin ().gameObject.transform.position) < 1) {
						Character.TrySetDirection (Direction.Up);
						GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayAudio ("passos");
					}
				}
			}

//			if (Character.PreviousPin () != null) {
//				if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), Character.PreviousPin ().gameObject.transform.position) < 1) {
//					Character.TrySetDirection (Direction.Down);
//				}
//			}
		}

//		if (Input.GetKeyUp(KeyCode.UpArrow))
//		{
//			Character.TrySetDirection(Direction.Up);
//		}
//		else if(Input.GetKeyUp(KeyCode.DownArrow))
//		{
//			Character.TrySetDirection(Direction.Down);
//		}
//		else if(Input.GetKeyUp(KeyCode.LeftArrow))
//		{
//			Character.TrySetDirection(Direction.Left);
//		}
//		else if(Input.GetKeyUp(KeyCode.RightArrow))
//		{
//			Character.TrySetDirection(Direction.Right);
//		}
	}

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}

	public void UpdateLevels()
	{
		int cleared = PlayerPrefs.GetInt ("ClearedLevels");
		for(int x = 0 ; x<=11 ; x++){
			if (x < cleared) {//Cleareds
				WaypointsAll[x].GetComponent<Pin>().ActualStatus = PinStatus.Cleared;
			} else if (x == cleared) {//Unlocked
				WaypointsAll[x].GetComponent<Pin>().ActualStatus = PinStatus.Unlocked;
			} else {//Locked
				WaypointsAll[x].GetComponent<Pin>().ActualStatus = PinStatus.Locked;
			}
		}
	}

	public void ChangeMap(string x){
	
		if (x == "Next") {
			if (actualMap < 2) {
				Maps [actualMap].SetActive (false);
				actualMap++;
				Maps [actualMap].SetActive (true);
				Character.transform.position = FirstPins [actualMap].transform.position; //new Vector3 (FirstPins [x].transform.position.x, FirstPins [x].transform.position.y, FirstPins [x].transform.position.z);
				StartPin = FirstPins [actualMap].GetComponent<Pin>();
				Character.SetCurrentPin(FirstPins[actualMap].GetComponent<Pin>());
			} 
			if (actualMap == 2) {
				NextButton [0].SetActive (false);
			}
			if (actualMap > 0) {
				NextButton [1].SetActive (true);
			}
		}

		if (x == "Previous") {
			if(actualMap>0){
				Maps [actualMap].SetActive (false);
				actualMap--;
				Maps [actualMap].SetActive (true);
				Character.transform.position = FirstPins [actualMap].transform.position;
				StartPin = FirstPins [actualMap].GetComponent<Pin>();
				Character.SetCurrentPin(FirstPins[actualMap].GetComponent<Pin>());
			}

			if (actualMap == 0) {
				NextButton [1].SetActive (false);
			}
			if (actualMap < 2) {
				NextButton [0].SetActive (true);
			}
		}

	}

	public void CallScene(string scene){
	
		SceneManager.LoadScene (scene);
	
	}
}
