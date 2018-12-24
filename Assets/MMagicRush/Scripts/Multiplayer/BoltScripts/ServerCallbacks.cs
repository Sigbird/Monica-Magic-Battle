﻿using UnityEngine;
using System.Collections;
using Bolt;
using UdpKit;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener {
    public string MatchName;

    public static ServerCallbacks Instance;

    bool allowNewConnections = true;

    void Awake() {
        Instance = this;    
    }

    // Não aceita mais conexões após receber 1 cliente (oponente)
    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token) {
        if (allowNewConnections) {
            allowNewConnections = false;

            BoltNetwork.SetServerInfo(MatchName, new RoomInfo(0, false));

            BoltNetwork.Accept(endpoint);
            
            return;
        }

        BoltNetwork.Refuse(endpoint);
    }

    public override void Connected(BoltConnection connection) {        
    }

    public override void Disconnected(BoltConnection connection) {        
    }
}