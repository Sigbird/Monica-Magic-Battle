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

	public InputField HeroRecInfluencia;

	public InputField HeroSpeed;

	public InputField HeroRange;

	public InputField HeroVisao;

	public InputField HeroCAT;

	public InputField HeroLVL1XP;

	public InputField HeroLVL2XP;

	public InputField HeroLVL3XP;

	public InputField HeroPrefTop;

	public InputField HeroPrefMid;

	public InputField HeroPrefBot;

	//ENEMY

	public InputField EnemyHP;

	public InputField EnemyMaxHP;

	public InputField EnemyEnergy;

	public InputField EnemyMaxEnergy;

	public InputField EnemyDMG;

	public InputField EnemyRecInfluencia;

	public InputField EnemySpeed;

	public InputField EnemyRange;

	public InputField EnemyVisao;

	public InputField EnemyCAT;

	public InputField EnemyLVL1XP;

	public InputField EnemyLVL2XP;

	public InputField EnemyLVL3XP;

	public InputField EnemyPrefTop;

	public InputField EnemyPrefMid;

	public InputField EnemyPrefBot;



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

		Hero.damageSpeed = float.Parse (HeroRecInfluencia.text);

		Hero.velocity = float.Parse (HeroSpeed.text);

		Hero.range = float.Parse (HeroRange.text);

		Hero.reach = float.Parse (HeroVisao.text);

		Hero.xpLvl1 = float.Parse (HeroLVL1XP.text);

		Hero.xplvl2 = float.Parse (HeroLVL2XP.text);

		Hero.xplvl3 = float.Parse (HeroLVL3XP.text);

		Hero.topPreference = float.Parse (HeroPrefTop.text);

		Hero.midPreference = float.Parse (HeroPrefMid.text);

		Hero.botPreference = float.Parse (HeroPrefBot.text);


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

		Enemy.damageSpeed = float.Parse (EnemyRecInfluencia.text);

		Enemy.velocity = float.Parse (EnemySpeed.text);

		Enemy.range = float.Parse (EnemyRange.text);

		Enemy.reach = float.Parse (EnemyVisao.text);

		Enemy.xpLvl1 = float.Parse (EnemyLVL1XP.text);

		Enemy.xplvl2 = float.Parse (EnemyLVL2XP.text);

		Enemy.xplvl3 = float.Parse (EnemyLVL3XP.text);

		Enemy.topPreference = float.Parse (EnemyPrefTop.text);

		Enemy.midPreference = float.Parse (EnemyPrefMid.text);

		Enemy.botPreference = float.Parse (EnemyPrefBot.text);

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
		HeroRecInfluencia.text = Hero.damageSpeed.ToString ();
		HeroSpeed.text = Hero.velocity.ToString ();
		HeroVisao.text = Hero.reach.ToString ();
		HeroRange.text = Hero.range.ToString ();
		HeroLVL1XP.text = Hero.xpLvl1.ToString ();
		HeroLVL2XP.text = Hero.xplvl2.ToString ();
		HeroLVL3XP.text = Hero.xplvl3.ToString ();
		HeroPrefTop.text = Hero.topPreference.ToString ();
		HeroPrefMid.text = Hero.midPreference.ToString ();
		HeroPrefBot.text = Hero.botPreference.ToString ();
		HeroCAT.text = Hero.TipoSoldado.ToString ();

		EnemyHP.text = Enemy.vida.ToString ();
		EnemyMaxHP.text = Enemy.vidaMax.ToString ();
		EnemyEnergy.text = Enemy.energy.ToString ();
		EnemyMaxEnergy.text = Enemy.energyMax.ToString ();
		EnemyDMG.text = Enemy.damage.ToString ();
		EnemyRecInfluencia.text = Enemy.damageSpeed.ToString ();
		EnemySpeed.text = Enemy.velocity.ToString ();
		EnemyVisao.text = Enemy.reach.ToString ();
		EnemyRange.text = Enemy.range.ToString ();
		EnemyLVL1XP.text = Enemy.xpLvl1.ToString ();
		EnemyLVL2XP.text = Enemy.xplvl2.ToString ();
		EnemyLVL3XP.text = Enemy.xplvl3.ToString ();
		EnemyPrefTop.text = Enemy.topPreference.ToString ();
		EnemyPrefMid.text = Enemy.midPreference.ToString ();
		EnemyPrefBot.text = Enemy.botPreference.ToString ();
		EnemyCAT.text = Enemy.TipoSoldado.ToString();

	}

	public void RevertSoldado(int x){
		SoldierControler Hero = GameObject.Find ("Hero").GetComponent<SoldierControler>();
		SoldierControler Enemy = GameObject.Find ("HeroEnemy").GetComponent<SoldierControler>();

		if (x == 0) {
			switch (int.Parse(HeroCAT.text)) {
			case 0:
				this.HeroDMG.text = (Hero.damage + 1).ToString();
				this.HeroRecInfluencia.text = (Hero.damageSpeed + 1).ToString();
				this.HeroVisao.text = (Hero.reach + 2).ToString();
				break;
			case 1:
				this.HeroHP.text = (Hero.vida + 3).ToString();
				this.HeroMaxHP.text = (Hero.vidaMax + 3).ToString();
				this.HeroSpeed.text = (Hero.velocity - 2).ToString();
				break;
			case 2:
				this.HeroHP.text = (Hero.vida - 1).ToString();
				this.HeroMaxHP.text = (Hero.vidaMax - 1).ToString();
				this.HeroVisao.text = (Hero.reach + 4).ToString();
				break;
			}
		} else {
			switch (int.Parse(EnemyCAT.text)) {
			case 0:
				this.EnemyDMG.text = (Enemy.damage + 1).ToString();
				this.EnemyRecInfluencia.text = (Enemy.damageSpeed + 1).ToString();
				this.EnemyVisao.text = (Enemy.reach + 2).ToString();
				break;
			case 1:
				this.EnemyHP.text = (Enemy.vida + 3).ToString();
				this.EnemyMaxHP.text = (Enemy.vidaMax + 3).ToString();
				this.EnemySpeed.text = (Enemy.velocity - 2).ToString();
				break;
			case 2:
				this.EnemyHP.text = (Enemy.vida - 1).ToString();
				this.EnemyMaxHP.text = (Enemy.vidaMax - 1).ToString();
				this.EnemyVisao.text = (Enemy.reach + 4).ToString();
				break;
			}
		}

	}

}
