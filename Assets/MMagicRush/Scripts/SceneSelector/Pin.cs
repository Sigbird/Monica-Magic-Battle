using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public enum PinStatus
{
	Locked,
	Unlocked,
	Cleared
}

public class Pin : MonoBehaviour
{



	[Header("Options")] //
	public bool IsAutomatic;
	public bool HideIcon;
	public string SceneToLoad;
	public int Enemy;

	
	[Header("Pins")] //
	public Pin UpPin;
	public Pin DownPin;
	public Pin LeftPin;
	public Pin RightPin;
	public Pin UnhidenUp;
	public Pin UnhidenDown;

	private Dictionary<Direction, Pin> _pinDirections; 

	public PinStatus ActualStatus;
	public GameObject[] StatusImages;
	

	/// <summary>
	/// Use this for initialisation
	/// </summary>
	private void Start()
	{
		// Load the directions into a dictionary for easy access
		_pinDirections = new Dictionary<Direction, Pin>
		{
			{ Direction.Up, UpPin },
			{ Direction.Down, DownPin },
			{ Direction.Left, LeftPin },
			{ Direction.Right, RightPin }
		};
		
		// Hide the icon if needed
		if (HideIcon)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void Update(){
	
		if (Input.GetMouseButtonUp (0)) {
			if (Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), this.transform.position) <= 1){
				if (Vector2.Distance (GameObject.Find ("Character").transform.position, this.transform.position) <= 1) {
					if (this.ActualStatus != PinStatus.Locked) {
						PlayerPrefs.SetString ("TerrainType", SceneToLoad);
						PlayerPrefs.SetInt ("Enemy", Enemy);

						PlayerPrefs.SetInt ("SelectedCharacter", PlayerPrefs.GetInt ("Character"));
						PlayerPrefs.SetInt ("round", 1);
						PlayerPrefs.SetInt ("playerCharges", 0);
						PlayerPrefs.SetInt ("enemyCharges", 0);
						SceneManager.LoadScene ("JogoMulti");

						Debug.Log ("Chamando cena: " + SceneToLoad);
					}
				}
			}
		}

		if (IsAutomatic == false) {
			if (ActualStatus == PinStatus.Cleared) {
				StatusImages [0].SetActive (true);
				StatusImages [1].SetActive (false);
				StatusImages [2].SetActive (false);
			}
			if (ActualStatus == PinStatus.Locked) {
				StatusImages [0].SetActive (false);
				StatusImages [1].SetActive (true);
				StatusImages [2].SetActive (false);
			}
			if (ActualStatus == PinStatus.Unlocked) {
				StatusImages [0].SetActive (false);
				StatusImages [1].SetActive (false);
				StatusImages [2].SetActive (true);
			}
		}
	}
	
	
	/// <summary>
	/// Get the pin in a selected direction
	/// Using a switch statement rather than linq so this can run in the editor
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public Pin GetPinInDirectionUnhiden(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
			return UnhidenUp;
			case Direction.Down:
			return UnhidenDown;
			case Direction.Left:
				return LeftPin;
			case Direction.Right:
				return RightPin;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}

	public Pin GetPinInDirection(Direction direction)
	{
		switch (direction)
		{
		case Direction.Up:
			return UpPin;
		case Direction.Down:
			return DownPin;
		case Direction.Left:
			return LeftPin;
		case Direction.Right:
			return RightPin;
		default:
			throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}

	
	/// <summary>
	/// This gets the first pin thats not the one passed 
	/// </summary>
	/// <param name="pin"></param>
	/// <returns></returns>
	public Pin GetNextPin(Pin pin)
	{
		return _pinDirections.FirstOrDefault(x => x.Value != null && x.Value != pin).Value;
	}
	
	
	/// <summary>
	/// Draw lines between connected pins
	/// </summary>
	private void OnDrawGizmos()
	{
		if(UpPin != null) DrawLine(UpPin);
		if(RightPin != null) DrawLine(RightPin);
		if(DownPin != null) DrawLine(DownPin);
		if(LeftPin != null) DrawLine(LeftPin);
	}


	/// <summary>
	/// Draw one pin line
	/// </summary>
	/// <param name="pin"></param>
	protected void DrawLine(Pin pin)
	{   
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pin.transform.position);
	}

	public void TryToCallScene(){
		Debug.Log ("apertou");

		if (Vector2.Distance (GameObject.Find ("Character").transform.position, this.transform.position) <= 1) {
			SceneManager.LoadScene (SceneToLoad);
		}

	}
}
