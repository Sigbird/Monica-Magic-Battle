using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class GlobalCallbacks : Bolt.GlobalEventListener {

    public override void BoltStartBegin() {
        BoltNetwork.RegisterTokenClass<RoomInfo>();
        BoltNetwork.RegisterTokenClass<PlayerToken>();
    }
}
