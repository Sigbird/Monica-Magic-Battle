using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System;

public class PlayerController : MonoBehaviour {
    public GameObject ClickFeedback;

    private Rigidbody2D rb;
    private Vector2 target;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (NetGameController.Instance.IsGameRunning()) {
            if (Input.GetMouseButtonUp(0)) {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CommandController.Move(target);

                ClickFeedback.SetActive(true);
                ClickFeedback.transform.position = target;

                NetClock.Instance.RegisterInputTime();
            }
        }              
    }

    private void FixedUpdate() {
        var distance = Vector2.Distance(target, transform.position);
        if (distance < 0.01f) {
            rb.GetComponent<Rigidbody2D>().velocity = new Vector2();
        }
    }

    public void MoveTo(Vector2 position) {
        var direction = position - (Vector2)transform.position;
        rb.velocity = direction.normalized;
    }
}
