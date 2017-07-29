using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;

public class EnemyAIController : MonoBehaviour {
    public GameObject InputFeedback;

    private ulong Turn = 1;

    private Vector2[] positions = {
        new Vector2(1.69f, 1.33f),
        new Vector2(-0.06f, 0.58f),
        new Vector2(-1.58f, 1.72f),
        new Vector2(0.09f, 3.24f)
    };

    private int cmdIndex = 0;
	// Use this for initialization
	void Start () {
        NetClock.Instance.StartClock();
        StartCoroutine(RunEnemyClock());
        StartCoroutine(RunMoveCommands());
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private IEnumerator RunEnemyClock() {
        while (NetClock.Instance.IsRunning()) {
            yield return new WaitForSecondsRealtime(NetClock.TurnTime);

            CommandBuffer.Instance.InsertToInput(new NetCommand(Turn));
            Turn++;
        }        
    }

    private IEnumerator RunMoveCommands() {
        while (NetClock.Instance.IsRunning()) {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            var target = positions[cmdIndex];
            CommandBuffer.Instance.InsertToInput(new MoveCommand(Turn, target));
            InputFeedback.SetActive(true);
            InputFeedback.transform.position = target;

            cmdIndex++;
            if (cmdIndex > 3) cmdIndex = 0;
        }        
    }
}
