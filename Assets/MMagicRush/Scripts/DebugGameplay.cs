using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugGameplay : MonoBehaviour {

	//HERO

	public InputField HeroHP;

	public InputField HeroMaxHP;

	public InputField HeroEnergy;

	public InputField HeroMaxEnergy;

	public InputField HeroDMG;

	public InputField HeroSpeed;

	//ENEMY

	public InputField EnemyHP;

	public InputField EnemyMaxHP;

	public InputField EnemyEnergy;

	public InputField EnemyMaxEnergy;

	public InputField EnemyDMG;

	public InputField EnemySpeed;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitDebug(){
		Time.timeScale = 1;
		this.gameObject.SetActive (false);
	}

	public void SetValues(){
		SoldierControler Hero = GameObject.Find ("Hero").GetComponent<SoldierControler>();
		SoldierControler Enemy = GameObject.Find ("HeroEnemy").GetComponent<SoldierControler>();
		Time.timeScale = 1;

		//HERO

		Hero.vida = int.Parse (HeroHP.text);
		Hero.UpdateLife ();

		Hero.vidaMax = int.Parse (HeroMaxHP.text);
		Hero.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Hero.UpdateLife ();

		Hero.energy = int.Parse (HeroEnergy.text);
		Hero.UpdateEnergy ();

		Hero.energyMax = int.Parse (HeroMaxEnergy.text);
		Hero.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Hero.UpdateEnergy ();

		Hero.damage = int.Parse (HeroDMG.text);

		Hero.speed = float.Parse (HeroSpeed.text);

		//ENEMY

		Enemy.vida = int.Parse (EnemyHP.text);
		Enemy.UpdateLife ();

		Enemy.vidaMax = int.Parse (EnemyMaxHP.text);
		Enemy.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Enemy.UpdateLife ();

		Enemy.energy = int.Parse (EnemyEnergy.text);
		Enemy.UpdateEnergy ();

		Enemy.energyMax = int.Parse (EnemyMaxEnergy.text);
		Enemy.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Enemy.UpdateEnergy ();

		Enemy.damage = int.Parse (EnemyDMG.text);

		Enemy.speed = float.Parse (EnemySpeed.text);


		Hero.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Hero.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Enemy.healtbarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		Enemy.energybarSoldier.GetComponent<HealtBar> ().RefreshMaxLIfe ();
		this.gameObject.SetActive (false);

	}

	public void GetValues(){
		SoldierControler Hero = GameObject.Find ("Hero").GetComponent<SoldierControler>();
		SoldierControler Enemy = GameObject.Find ("HeroEnemy").GetComponent<SoldierControler>();
		Time.timeScale = 0;

		HeroHP.text = Hero.vida.ToString ();
		HeroMaxHP.text = Hero.vidaMax.ToString ();
		HeroEnergy.text = Hero.energy.ToString ();
		HeroMaxEnergy.text = Hero.energyMax.ToString ();
		HeroDMG.text = Hero.damage.ToString ();
		HeroSpeed.text = Hero.speed.ToString ();

		EnemyHP.text = Enemy.vida.ToString ();
		EnemyMaxHP.text = Enemy.vidaMax.ToString ();
		EnemyEnergy.text = Enemy.energy.ToString ();
		EnemyMaxEnergy.text = Enemy.energyMax.ToString ();
		EnemyDMG.text = Enemy.damage.ToString ();
		EnemySpeed.text = Enemy.speed.ToString ();

	}



}
