using System.Collections;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using YupiPlay;

public class EnemyAIController : MonoBehaviour {       
    private CommandController input;

    void Awake() {
        
    }

    // Use this for initialization
    void Start () {                                
        if (NetworkSessionManager.Instance.Match != null 
            && NetworkSessionManager.Instance.Match.AgainstAI) {
            input = GetComponent<CommandController>();
            StartCoroutine(Play());
        }        
    }
	
    private IEnumerator Play() {
        while (true) {
            yield return new WaitForSecondsRealtime(1);

            if (NetGameController.Instance.HasGameStarted() && !NetClock.Instance.IsDelayed()) {                
                var pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-15f, 15f));
                input.Move(pos);                
            }            
        }
    }    
}
