using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;

public class EnemyRemoteController : BasePlayerController {                
    private Stopwatch watch;                

    /*
    protected new void OnCollisionEnter2D(Collision2D collision) {
        if (rb.velocity == Vector2.zero) {
            var playerPreviousVelocity = collision.gameObject.GetComponent<PlayerController>().PreviousVelocity;
            rb.velocity = playerPreviousVelocity;            
        } else {
            rb.velocity = -PreviousVelocity;
        }        
        
        stopAfterCollision = StartCoroutine(StopAfterCollision());
    }
    */
}
