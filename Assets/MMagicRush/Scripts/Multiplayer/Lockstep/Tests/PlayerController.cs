using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System;

public class PlayerController : BasePlayerController {
    public GameObject ClickFeedback;

    private Vector2 moveTo;

	// Update is called once per frame
	void Update () {
        if (NetGameController.Instance.HasGameStarted() && !NetClock.Instance.IsDelayed()) {
            if (Input.GetMouseButtonUp(0)) {
                moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CommandController.Move(moveTo);

                ClickFeedback.transform.position = moveTo;
                ClickFeedback.SetActive(true);                

                NetClock.Instance.RegisterInputTime();
            }
        }              
    }

    new protected void FixedUpdate() {
        var distance = Vector2.Distance(target, transform.position);        
        if (distance < 0.01f) {
            rb.velocity = Vector2.zero;            
        }

        if (moveTo != Vector2.zero) {
            var distanceToGo = Vector2.Distance(moveTo, transform.position);

            if (distanceToGo < 0.01f) {
                ClickFeedback.SetActive(false);
            }
        }        
    }


}
