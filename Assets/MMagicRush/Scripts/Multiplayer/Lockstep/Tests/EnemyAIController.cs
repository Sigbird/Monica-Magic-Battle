using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using System.Diagnostics;

public class EnemyAIController : MonoBehaviour {
    public GameObject InputFeedback;

    private long Turn = 1;

    private Vector2[] positions = {
        new Vector2(1.69f, 1.33f),
        new Vector2(-0.06f, 0.58f),
        new Vector2(-1.58f, 1.72f),
        new Vector2(0.09f, 3.24f)
    };

    private int cmdIndex = 0;

    private List<NetCommand> outputBuffer;

    private Stopwatch watch;    

    private Vector2 targetPosition;
    private Rigidbody2D rb;

    void Awake() {
        
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        watch = new Stopwatch();
        outputBuffer = new List<NetCommand>();
               
        StartCoroutine(RunEnemyClock());
        StartCoroutine(RunMoveCommands());
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

    private IEnumerator RunEnemyClock() {
        while (true) {
            yield return new WaitForSecondsRealtime(NetClock.Instance.TurnTime);

            CommandBuffer.Instance.InsertToInput(new NetCommand(Turn));
            var inputList = outputBuffer.FindAll((NetCommand cmd) => { return cmd.GetTurn() == Turn; });
            CommandBuffer.Instance.InsertListToInput(inputList);

            if (Turn > 2) {                
                outputBuffer.RemoveAll((NetCommand cmd) => { return cmd.GetTurn() == Turn - 2; });
            }

            Turn++;            
        }        
    }

    private IEnumerator RunMoveCommands() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(2f, 3f));

            targetPosition = positions[cmdIndex];
                        
            outputBuffer.Add(new MoveCommand(Turn, targetPosition));
            InputFeedback.SetActive(true);
            InputFeedback.transform.position = targetPosition;

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

    public void MoveTo(Vector2 position) {
        var direction = position - (Vector2) transform.position;
        rb.velocity = direction.normalized;
    }
}
