using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using YupiPlay;

public class NetGameController : MonoBehaviour {
    public GameObject PlayerHero;
    public GameObject ClickFeeback;
    public PlayerController playerController;
    public EnemyAIController enemyController;

    private Vector2 targetPosition;    

    public static NetGameController Instance { get { return instance; } set { } }

    private static NetGameController instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        NetworkSessionManager.Instance.SendReady();
	}
	
	// Update is called once per frame
    // Update de testes
	void Update () {		
	}     

    public void PlayerCommandListener(NetCommand cmd) {
        Selector(false, cmd);
    }    

    public void EnemyCommandListener(NetCommand cmd) {
        Selector(true, cmd);
    }

    private void Selector(bool IsInput, NetCommand cmd) {
        if (cmd.GetCommand() == NetCommand.MOVE) {            
            var position = (cmd as MoveCommand).GetPosition();

            if (IsInput) {
                enemyController.MoveTo(position);
                enemyController.GetEnemyInputLatency();
            } else {
                playerController.MoveTo(position);
                NetClock.Instance.GetInputLatency();
            }            
        }
    }

    public void StartGame() {
        
    }

    public void EndGame() {

    }
}
