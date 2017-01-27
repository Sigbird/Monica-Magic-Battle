using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsScript : MonoBehaviour {
	public Sprite[] spriteImages;
	public int skillID;
	public bool skillActivated;

	// Use this for initialization
	void Start () {
		UpdateSkill ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Enable(){
		this.GetComponent<Animator> ().SetTrigger ("Enable");
	}

	public void UpdateSkill(){
		if (this.GetComponent<SpriteRenderer> () != null) {
			this.GetComponent<SpriteRenderer> ().sprite = spriteImages [skillID];
		}
	}

	public void ActivateSkill(){
		skillActivated = true;
	}
}
