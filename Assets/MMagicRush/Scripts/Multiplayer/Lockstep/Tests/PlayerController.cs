using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System;

public class PlayerController : BasePlayerController {
    public GameObject ClickFeedback;
    
	// Update is called once per frame
	void Update () {
        if (NetGameController.Instance.HasGameStarted()) {
            if (Input.GetMouseButtonUp(0)) {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CommandController.Move(target);

                ClickFeedback.SetActive(true);
                ClickFeedback.transform.position = target;

                NetClock.Instance.RegisterInputTime();
            }
        }              
    }

    
}
