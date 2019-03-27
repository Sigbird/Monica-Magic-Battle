using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntity : Bolt.EntityEventListener<ITowerState> {
	
	ChargesScriptTowers chargesScriptTowers = null;
	ChargesScript chargesScript = null;

	public override void Attached() {
		chargesScript = GetComponent<ChargesScript>();

		if (chargesScript != null) {
			state.Vida = chargesScript.vidaMax;
		}

		chargesScriptTowers = GetComponent<ChargesScriptTowers>();

		if (chargesScriptTowers != null) {
			state.Vida = chargesScriptTowers.vidaMax;
		}

		state.AddCallback("Vida", OnVidaChange);
	}	

	private void OnVidaChange() {
		if (chargesScript != null) {
			chargesScript.vida = state.Vida;
			return;
		}

		chargesScriptTowers.vida = state.Vida;
	}

	public void SetVida(float vida) {
		state.Vida = vida;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
