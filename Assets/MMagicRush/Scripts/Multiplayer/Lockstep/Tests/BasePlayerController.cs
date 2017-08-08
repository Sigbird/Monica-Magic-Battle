using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour {
    protected Rigidbody2D rb;
    protected Vector2 target;

    protected Coroutine stopAfterCollision;

    // Use this for initialization
    protected void Start () {        
        rb = GetComponent<Rigidbody2D>();
    }		

    protected void FixedUpdate() {
        var distance = Vector2.Distance(target, transform.position);
        if (distance < 0.01f) {
            rb.velocity = Vector2.zero;
        }
    }

    public void MoveTo(Vector2 position) {
        if (stopAfterCollision != null) {
            StopCoroutine(stopAfterCollision);
        }

        var direction = position - (Vector2)transform.position;
        rb.velocity = direction.normalized;
    }

    protected void OnCollisionEnter2D(Collision2D collision) {        
        if (target != Vector2.zero) {
            Vector2 invertedDirection = -target;
            rb.velocity = invertedDirection.normalized;
        }

        stopAfterCollision = StartCoroutine(StopAfterCollision());
    }

    protected IEnumerator StopAfterCollision() {
        yield return new WaitForSeconds(1);

        rb.velocity = Vector2.zero;
    }
}
