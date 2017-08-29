using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour {
    protected const int SpeedFactor = 10;
    protected Rigidbody2D rb;
    protected Vector2 target;
    protected Vector2 previousPos;

    protected Coroutine stopAfterCollision;
    public Vector2 PreviousVelocity;    

    // Use this for initialization
    protected void Start () {        
        rb = GetComponent<Rigidbody2D>();
        target = transform.localPosition;
        previousPos = transform.localPosition;
    }

    protected void FixedUpdate() {
        /*var distance = Vector2.Distance(target, transform.localPosition);
        if (distance < 0.05f) {
            rb.velocity = Vector2.zero;
        }*/

        if (target != (Vector2)transform.localPosition) {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, 0.01f * SpeedFactor);
        }
        if ((Vector2)transform.localPosition == target && (Vector2)transform.localPosition != previousPos) {
            previousPos = transform.localPosition;
        }
    }

    public void MoveTo(Vector2 position) {
        /*if (stopAfterCollision != null) {
            StopCoroutine(stopAfterCollision);
        }*/
        previousPos = transform.localPosition;
        target = position;        

        /*var direction = position - (Vector2)transform.localPosition;
        rb.velocity = direction.normalized;
        PreviousVelocity = rb.velocity;*/
    }

    protected void OnCollisionEnter2D(Collision2D collision) {      
        stopAfterCollision = StartCoroutine(StopAfterCollision());
    }

    protected IEnumerator StopAfterCollision() {
        yield return new WaitForSeconds(1);

        rb.velocity = Vector2.zero;
    }
}
