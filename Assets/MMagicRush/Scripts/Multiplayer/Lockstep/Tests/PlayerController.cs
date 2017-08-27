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
        /*
        if (NetGameController.Instance.HasGameStarted() && !NetClock.Instance.IsDelayed()) {
            if (Input.GetMouseButtonUp(0)) {
                //moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveTo = GridInputSnap.GetGridInput();
                CommandController.Move(moveTo);

                ClickFeedback.transform.localPosition = moveTo;
                ClickFeedback.SetActive(true);                

                NetClock.Instance.RegisterInputTime();
            }
        }     
        */
    }

    void OnPlayerMouseUp(Vector2 mousePos) {
        if (NetGameController.Instance.HasGameStarted() && !NetClock.Instance.IsDelayed()) {

            //moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTo = mousePos;
            CommandController.Move(moveTo);

            ClickFeedback.transform.localPosition = moveTo;
            ClickFeedback.SetActive(true);

            NetClock.Instance.RegisterInputTime();            
        }
    }

    new protected void FixedUpdate() {
        var distance = Vector2.Distance(target, transform.localPosition);        
        if (distance < 0.05f) {
            rb.velocity = Vector2.zero;            
        }

        if (moveTo != Vector2.zero) {
            var distanceToGo = Vector2.Distance(moveTo, transform.localPosition);

            if (distanceToGo < 0.05f) {
                ClickFeedback.SetActive(false);
            }
        }        
    }

    void OnEnable() {
        GridInputSnap.OnGridMouseUpEvent += OnPlayerMouseUp;
    }

    void OnDisable() {
        GridInputSnap.OnGridMouseUpEvent -= OnPlayerMouseUp;
    }

}
