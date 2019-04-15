using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NetworkCallbacks : Bolt.GlobalEventListener {
    public Camera MainCamera;
    public Transform Player1Position;
    public Transform Player2Position;
    public MatchClock matchClock;   

    public GameController gameController; 
    public RoundStart roundStart;
    public WPScript wpScript;

    public GameObject RiverPassLeft;
    public GameObject RiverPassRight;

	public GameObject playerServer;
	public GameObject playerClient;

    private bool player1Ready;
    private bool player2Ready;
    public bool AllPlayersReady;

	public GameObject TerrainVisual;
	public GameObject SceneCamera;
	public GameObject CardSpawner;
	public GameObject WaitForPlayers;
	public GameObject Inicio;


    public event Action OnPlayersReady;

    public override void SceneLoadLocalDone(string map) {       
        
    }

	void Awake(){


		if (BoltNetwork.IsClient) {
			//gameController.enabled = true;
			//Flip de Camera e Terreno no Cliente
			CardSpawner.transform.position = new Vector3(3, 5.5f,0);
			SceneCamera.transform.rotation = Quaternion.Euler (0, 0, 180);
			SceneCamera.transform.position = new Vector3 (SceneCamera.transform.position.x, 1.05f, SceneCamera.transform.position.z);
			TerrainVisual.transform.rotation = Quaternion.Euler (0, 0, 180);
			TerrainVisual.transform.position = new Vector3 (TerrainVisual.transform.position.x, 1.05f, TerrainVisual.transform.position.z);    

			foreach (GameObject g in GameObject.FindGameObjectsWithTag("enemytower1")) {
				g.transform.rotation = Quaternion.Euler (0, 0, 180);
				//g.transform.position = new Vector3 (g.transform.position.x, g.transform.position.y - 1f, g.transform.position.z);    
			}
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("enemytower2")) {
				g.transform.rotation = Quaternion.Euler (0, 0, 180);
				//g.transform.position = new Vector3 (g.transform.position.x, g.transform.position.y - 2f, g.transform.position.z);    
			}
		}
	}

	void Update(){
		if (BoltNetwork.IsServer) {
			if(playerClient != null && playerServer != null){
				playerServer.transform.rotation = Quaternion.Euler (0, 0, 0);
				playerClient.transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		} else {
			if (playerClient != null && playerServer != null) {
				playerServer.transform.rotation = Quaternion.Euler (0, 0, 180);
				playerClient.transform.rotation = Quaternion.Euler (0, 0, 180);
			}
		}
	}

    public override void SceneLoadRemoteDone(BoltConnection connection) {
        if (BoltNetwork.IsServer) {
            var serverEntity = BoltNetwork.Instantiate(BoltPrefabs.HeroMPServer, Player1Position.position, Quaternion.identity);
            playerServer = serverEntity;
            serverEntity.name = playerServer.name = "HeroMPServer";     
            
            bindRiverPass(serverEntity);
            
            gameController.HeroGameObject = playerServer;        
            wpScript.Hero = playerServer.transform;
            roundStart.Hero = playerServer;      



		}   
    
        if (BoltNetwork.IsClient) {
            var clientEntity = BoltNetwork.Instantiate(BoltPrefabs.HeroMPClient, Player2Position.position, Quaternion.identity);
            playerClient = clientEntity;
            clientEntity.name = playerClient.name = "HeroMPClient"; 

            bindRiverPass(clientEntity);

            gameController.EnemyGameObject = playerClient;
            wpScript.Enemy = playerClient.transform;
            roundStart.Enemy = playerClient;


			}

		WaitForPlayers.SetActive (false);
		Inicio.SetActive (true);
     }        
    

    public override void EntityAttached(BoltEntity entity) { 
        Debug.Log(entity.name);
        // Começa a countdown de Início partida após o Player der spawn
		if (BoltNetwork.IsClient) {
			if (entity.tag == "enemysoldier1") {
				gameController.HeroGameObject = entity;
				entity.transform.rotation = Quaternion.Euler (0, 0, 180);
				wpScript.Hero = entity.transform;
				roundStart.Hero = entity;    
//				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
//				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
				bindRiverPass (entity);
				player1Ready = true;
			}
			if (entity.tag == "enemysoldier2") {
				gameController.EnemyGameObject = entity;
				entity.transform.rotation = Quaternion.Euler (0, 0, 180);
				wpScript.Enemy = entity.transform;
				roundStart.Enemy = entity;
//				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
//				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
				bindRiverPass (entity);
				player2Ready = true;
			}
		}
//		} else {
//			if (entity.tag == "enemysoldier1") {
//				gameController.HeroGameObject = entity;
//				entity.transform.rotation = Quaternion.Euler (0, 0, 0);
//				wpScript.Hero = entity.transform;
//				roundStart.Hero = entity;    
////				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
////				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
//				bindRiverPass (entity);
//				player1Ready = true;
//			}
//			if (entity.tag == "enemysoldier2") {
//				gameController.EnemyGameObject = entity;
//				entity.transform.rotation = Quaternion.Euler (0, 0, 0);
//				wpScript.Enemy = entity.transform;
//				roundStart.Enemy = entity;
////				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
////				entity.GetComponent<WPSoldierControler>().RiverPassLeft = RiverPassLeft;
//				bindRiverPass (entity);
//				player2Ready = true;
//			}
//		}

        if (player1Ready && player2Ready) {
            AllPlayersReady = true;
            if (OnPlayersReady != null) OnPlayersReady();
        }
    }

    private void bindRiverPass(GameObject entity) {        
        var entityController = entity.GetComponent<WPSoldierControler>();
        entityController.RiverPassLeft = RiverPassLeft;
        entityController.RiverPassRight = RiverPassRight;   
    }
    
    public void ShutdownMultiplayer() {
        BoltNetwork.Shutdown();
    }

	public void NextLevel(){

			Time.timeScale = 1;
			PlayerPrefs.DeleteKey ("TerrainType");
			SceneManager.LoadScene ("MainNewCoreloop");
		}

	public void InstanciarTropa(Vector3 pos, int id){
		if (BoltNetwork.IsClient) {

				BoltNetwork.Instantiate (BoltPrefabs.TroopMultiClient, pos, Quaternion.Euler (0, 0, 180)).GetComponent<SoldierControler>().troopId = id;


		} else {

				BoltNetwork.Instantiate (BoltPrefabs.TroopMultiServer, pos, Quaternion.Euler(0,0,0)).GetComponent<SoldierControler>().troopId = id;

		}
	}

}
