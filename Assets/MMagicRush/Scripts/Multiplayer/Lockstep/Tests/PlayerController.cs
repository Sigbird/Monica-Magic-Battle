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
        if (NetGameController.Instance.HasGameStarted()) {
            if (Input.GetMouseButtonUp(0)) {
                moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CommandController.Move(moveTo);

                ClickFeedback.SetActive(true);
                ClickFeedback.transform.position = moveTo;

                NetClock.Instance.RegisterInputTime();
            }
        }              
    }

    
}
