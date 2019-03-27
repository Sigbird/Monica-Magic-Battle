using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject playerServer;
    private GameObject playerClient;
    

    public override void SceneLoadLocalDone(string map) {       
        
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
    }

    public override void EntityAttached(BoltEntity entity) { 
        Debug.Log(entity.name);
        // Começa a countdown de Início partida após o Player der spawn
        if (entity.tag == "enemysoldier1") {
            gameController.HeroGameObject = entity;        
            wpScript.Hero = entity.transform;
            roundStart.Hero = entity;    

            bindRiverPass(entity);
        }
        if (entity.tag == "enemysoldier2") {
            gameController.EnemyGameObject = entity;
            wpScript.Enemy = entity.transform;
            roundStart.Enemy = entity;

            bindRiverPass(entity);
        }        
    }

    private void bindRiverPass(GameObject entity) {        
        var entityController = entity.GetComponent<WPSoldierControler>();
        entityController.RiverPassLeft = RiverPassLeft;
        entityController.RiverPassRight = RiverPassRight;   
    }
}
