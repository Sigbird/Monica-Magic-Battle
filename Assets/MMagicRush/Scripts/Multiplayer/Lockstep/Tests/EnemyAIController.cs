using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;

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

    private List<NetCommand> outputBuffer;

    private Stopwatch watch;

    public static EnemyAIController Instance;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {        
        watch = new Stopwatch();
        outputBuffer = new List<NetCommand>();

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
            var inputList = outputBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == Turn; });
            CommandBuffer.Instance.InsertListToInput(inputList);

            if (Turn > 2) {                
                outputBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == Turn - 1; });
            }

            Turn++;
        }        
    }

    private IEnumerator RunMoveCommands() {
        while (NetClock.Instance.IsRunning()) {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            var target = positions[cmdIndex];
                        
            outputBuffer.Add(new MoveCommand(Turn, target));
            InputFeedback.SetActive(true);
            InputFeedback.transform.position = target;

            watch.Start();

            cmdIndex++;
            if (cmdIndex > 3) cmdIndex = 0;
        }        
    }

    public void GetEnemyInputLatency() {
        watch.Stop();
        UnityEngine.Debug.Log("EIL: " + watch.ElapsedMilliseconds);
        watch.Reset();
    }
}
