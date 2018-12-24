using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCallbacks : Bolt.GlobalEventListener {
    public Camera MainCamera;
    public Transform Player1Position;
    public Transform Player2Position;
    public MatchClock matchClock;    

    public override void SceneLoadLocalDone(string map) {       
        if (BoltNetwork.IsServer) {
            //BoltNetwork.Instantiate(BoltPrefabs.PlayerShip, Player1Position.position, Quaternion.identity);            
        }   

        if (BoltNetwork.IsClient) {
            //BoltNetwork.Instantiate(BoltPrefabs.Player2Ship, Player2Position.position, Player2Position.rotation);
            //MainCamera.transform.rotation = Player2Position.rotation;
        }
    }

    public override void EntityAttached(BoltEntity entity) {        
        // Começa a countdown de Início partida após o Player der spawn
        if (BoltNetwork.IsServer && entity.tag == "Player2") {            
            matchClock.StartCountDown();
        }
    }    
}
