using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;
using YupiPlay;

public class EnemyAIController : MonoBehaviour {   
    private Stopwatch watch;

    private CommandController input;

    void Awake() {
        
    }

    // Use this for initialization
    void Start () {        
        watch = new Stopwatch();        
                
        if (NetworkSessionManager.Instance.Match != null 
            && NetworkSessionManager.Instance.Match.AgainstAI) {
            input = GetComponent<CommandController>();
            StartCoroutine(Play());
        }
    }
	
    private IEnumerator Play() {
        while (true) {
            yield return new WaitForSeconds(1);

            if (NetGameController.Instance.HasGameStarted() && !NetClock.Instance.IsDelayed()) {                
                var pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-15f, 15f));
                input.Move(pos);                
            }            
        }
    }

    public void GetEnemyInputLatency() {
        watch.Stop();
        UnityEngine.Debug.Log("EIL: " + watch.ElapsedMilliseconds);
        watch.Reset();
    }    
}
