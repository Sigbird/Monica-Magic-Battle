using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaticController : MonoBehaviour 
{
	public static StaticController instance;         //A reference to our game control script so we can access it statically.

	public GameController GameController;             //A reference to the object that displays the text which appears when the player dies.


	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

	void Update()
	{

	}
		
}