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
	public int actualMap = 0;
	public int nextMap = 1;
	public GameObject[] NextButton;
	public GameObject[] FirstPins;
	public GameObject[] SecondPins;
	public GameObject ScrollController;
	public int atualPin;




	public Text MapNames;
	public float t = 0;
	public float u = 0;
	public int idxChanged;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	public void Start ()
	{
		idxChanged = 100;
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

		int index = Camera.main.GetComponent<ScrollSanp> ().idx;
		//Debug.Log ("UPDATE INDEX "+index);
		if (index != idxChanged) {
			switch (index) {
			case 0:
				MapNames.text = "Reino da Floresta";
				ScrollController.transform.localPosition = new Vector3 (0, -2, 0);
				ChangeMap ("Forest");
				break;
			case 1:
				MapNames.text = "Reino Congelado";
				ScrollController.transform.localPosition = new Vector3(-607,-2,0);
				ChangeMap ("Ice");
				break;
			case 2:
				MapNames.text = "Castelo da Escuridão";
				ScrollController.transform.localPosition = new Vector3(-1214,-2,0);
				ChangeMap ("Dungeon");
				break;
			default:
				break;
			}
			idxChanged = index;
		}

		//Maps [actualMap].transform.FindChild ("MapSea").GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, Mathf.Lerp(1, 0, t));
		//Maps [nextMap].transform.FindChild ("MapSea").GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, Mathf.Lerp(1, 0, t));


		if (t <= 1) {
			t += u * Time.deltaTime;
		} else {
			t = 0;
			u = 0;
		}


		if (index<= 0) {
			NextButton [1].SetActive (false);
		} else {
			NextButton [1].SetActive (true);
		}

		if (index >= 2) {
			NextButton [0].SetActive (false);
		} else {
			NextButton [0].SetActive (true);
		}

//		if (t <= 1 && t >= 0) {
//			t += u * Time.deltaTime;
//		} else {
//			if(t >= 1)
//				t = 0.99f;
//
//			if(t <= 0)
//				t = 0.01f;
//		}
			
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (Input.GetMouseButtonUp (0)) {
			if (Character.NextPin () != null) {
				if (Character.NextPin ().ActualStatus != PinStatus.Locked) {
//					if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), Character.NextPin ().gameObject.transform.position) < 1) {
//						Character.TrySetDirection (Direction.Up);
//						GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayAudio ("passos");
//					}
//					if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), Character.NextPin ().gameObject.transform.position) < 1) {
//						Character.targetpin = 
//						GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayAudio ("passos");
//					}
				}
			}

//			if (Character.PreviousPin () != null) {
//				if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), Character.PreviousPin ().gameObject.transform.position) < 1) {
//					Character.TrySetDirection (Direction.Down);
//				}
//			}
		}

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(hit.collider != null)
		{
			if (hit.collider.transform.tag == "PinWaypoint") {
				if (hit.transform.gameObject.GetComponent<Pin> ().ActualStatus != PinStatus.Locked) {
					if (hit.transform.gameObject.GetComponent<Pin> ().PinID != atualPin) {
						atualPin = hit.transform.gameObject.GetComponent<Pin> ().PinID;
						Character.targetpin = hit.transform.GetComponent<Pin> ();
						GameObject.Find ("Main Camera").GetComponent<AudioManager> ().PlayAudio ("passos");
					} else {
						
					}
				}
			}

//			Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
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
//		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}

	public void UpdateLevels()
	{
		int cleared = PlayerPrefs.GetInt ("ClearedLevels");
		Debug.Log ("Cleared Levels: "+cleared);
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

	public void SwitchMap(string x){
		int cleared = PlayerPrefs.GetInt ("ClearedLevels");
		 
		if (x == "Next") {
			if (actualMap < 2) {
				actualMap++;
				Camera.main.GetComponent<ScrollSanp> ().idx = actualMap;
			}
		}

		if (x == "Previous") {
			if (actualMap > 0) {
				actualMap--;
				Camera.main.GetComponent<ScrollSanp> ().idx = actualMap;
			}
		}

			if (actualMap == 1 && cleared > 3){
				Character.gameObject.SetActive (true);
			} else {
				Character.gameObject.SetActive (false);
			}

			if (actualMap == 2 && cleared > 7) {
				Character.gameObject.SetActive (true);
			} else {
				Character.gameObject.SetActive (false);
			}

			if (actualMap == 0)
				Character.gameObject.SetActive (true);
		

	}



	public void ChangeMap(string x){
	
		if (x == "Next") {
			if (actualMap < 2) {
				Maps [actualMap].SetActive (false);
				actualMap++;
				nextMap = actualMap + 1;
				Maps [actualMap].SetActive (true);
				Character.transform.position = FirstPins [actualMap].transform.position; //new Vector3 (FirstPins [x].transform.position.x, FirstPins [x].transform.position.y, FirstPins [x].transform.position.z);
				StartPin = FirstPins [actualMap].GetComponent<Pin>();
				Character.SetCurrentPin(FirstPins[actualMap].GetComponent<Pin>());
			} 
			if (actualMap == 2) {
				NextButton [0].SetActive (false);
				nextMap = actualMap - 1;
			}
			if (actualMap > 0) {
				NextButton [1].SetActive (true);
			}
		}

		if (x == "Previous") {
			if(actualMap>0){
				Maps [actualMap].SetActive (false);
				actualMap--;
				nextMap = actualMap - 1;
				Maps [actualMap].SetActive (true);
				Character.transform.position = FirstPins [actualMap].transform.position;
				StartPin = FirstPins [actualMap].GetComponent<Pin>();
				Character.SetCurrentPin(FirstPins[actualMap].GetComponent<Pin>());
			}

			if (actualMap == 0) {
				NextButton [1].SetActive (false);
				nextMap = actualMap + 1;
			}
			if (actualMap < 2) {
				NextButton [0].SetActive (true);
			}
		}
		Character.atualpin = 0;
		if (x == "Forest") {
			Character.transform.position = FirstPins [0].transform.position;
			StartPin = FirstPins [0].GetComponent<Pin>();
			Character.SetCurrentPin(FirstPins[0].GetComponent<Pin>());
			Maps [0].SetActive (true);
			Maps [1].SetActive (false);
			Maps [2].SetActive (false);
			Character.GenericPins = Character.ForestPins;

		}
		if (x == "Ice") {
			Character.transform.position = FirstPins [1].transform.position;
			StartPin = FirstPins [1].GetComponent<Pin>();
			Character.SetCurrentPin(FirstPins[1].GetComponent<Pin>());
			Maps [0].SetActive (false);
			Maps [1].SetActive (true);
			Maps [2].SetActive (false);
			Character.GenericPins = Character.FrozenPins;
		}
		if (x == "Dungeon") {
			Character.transform.position = FirstPins [2].transform.position;
			StartPin = FirstPins [2].GetComponent<Pin>();
			Character.SetCurrentPin(FirstPins[2].GetComponent<Pin>());
			Maps [0].SetActive (false);
			Maps [1].SetActive (false);
			Maps [2].SetActive (true);
			Character.GenericPins = Character.DungeonPins;
		}

	}

	public void CallScene(string scene){
	
		SceneManager.LoadScene (scene);
	
	}
}
