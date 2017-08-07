using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;

public class EnemyRemoteController : MonoBehaviour {
    public GameObject InputFeedback;
            
    private Stopwatch watch;    

    private Vector2 targetPosition;
    private Rigidbody2D rb;

    void Awake() {
        
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        watch = new Stopwatch();                       
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate() {
        var enemyDistance = Vector2.Distance(targetPosition, transform.position);
        if (enemyDistance < 0.001f) {
            rb.velocity = new Vector2();
        }
    }   

    public void GetEnemyInputLatency() {
        watch.Stop();
        UnityEngine.Debug.Log("EIL: " + watch.ElapsedMilliseconds);
        watch.Reset();
    }

    public void MoveTo(Vector2 position) {
        var direction = position - (Vector2) transform.position;
        rb.velocity = direction.normalized;
    }    
}
