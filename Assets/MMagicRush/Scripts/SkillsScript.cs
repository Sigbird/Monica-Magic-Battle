using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsScript : MonoBehaviour {
	public Sprite[] spriteImages;
	public int skillID;
	public int skillLevel;
	public bool skillActivated;
	public SpriteRenderer lockObject;
	public Sprite lockLvl1;
	public Sprite lockLvl2;
	public Sprite lockLvl3;


	// Use this for initialization
	void Start () {
		UpdateSkill ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Enable(){
//		this.GetComponent<Animator> ().SetTrigger ("Enable");
	}

	public void Disable(){
		this.GetComponent<Animator> ().SetTrigger ("Disable");
	}

	public void UpdateSkill(){
		if (this.GetComponent<SpriteRenderer> () != null) {
			this.GetComponent<SpriteRenderer> ().sprite = spriteImages [skillID];
			if (this.skillLevel == 2) {
				lockObject.sprite = lockLvl2;
			} else if (this.skillLevel == 3) {
				lockObject.sprite = lockLvl3;
			} else {
				lockObject.sprite = lockLvl1;
			}
		}
	}

	public void ActivateSkill(){
		skillActivated = true;
	}
}
