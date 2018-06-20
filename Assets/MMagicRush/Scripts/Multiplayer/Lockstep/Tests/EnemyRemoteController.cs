using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;

public class EnemyRemoteController : BasePlayerController {                
    private Stopwatch watch;

	public bool alive;

    public void GetEnemyInputLatency() {
        watch.Stop();
        UnityEngine.Debug.Log("EIL: " + watch.ElapsedMilliseconds);
        watch.Reset();
    }    
}
