using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using YupiPlay;

public class NetGameController : MonoBehaviour {
    public GameObject PlayerHero;
    public GameObject ClickFeeback;
    public PlayerController PlayerController;
    public EnemyRemoteController EnemyController;
    public ProtoGameUI GameUI;

    private Vector2 targetPosition;    

    public static NetGameController Instance { get { return instance; } set { } }

    private static NetGameController instance;

    private bool hasGameStarted = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    
    void Start () {
        NetworkSessionManager.Instance.SendReady();
#if UNITY_EDITOR
        StartGame();
#endif
    }
		
	void Update () {		
	}     

    public void PlayerCommandListener(NetCommand cmd) {
        Selector(false, cmd);
    }    

    public void EnemyCommandListener(NetCommand cmd) {
        Selector(true, cmd);
    }

    private void Selector(bool isInput, NetCommand cmd) {
        if (cmd.GetCommand() == NetCommand.MOVE) {            
            var position = (cmd as MoveCommand).GetPosition();

            if (isInput) {
                EnemyController.MoveTo(MirrorPosition(position));
                //EnemyController.GetEnemyInputLatency();
            } else {
                PlayerController.MoveTo(position);
               // NetClock.Instance.GetInputLatency();
            }            
        }

        if (isInput && cmd.GetCommand() == NetCommand.END) {
            NetClock.Instance.StopClock();
        }
    }

    public void StartGame() {
        NetClock.Instance.StartClock();
        StartCoroutine(StartUITimer());
    }

    private IEnumerator StartUITimer() {        
        yield return new WaitForSeconds(2);
        GameUI.ShowStart();
        yield return new WaitForSeconds(2);
        GameUI.HideStart();
        hasGameStarted = true;
    }

    public void EndGame() {
        hasGameStarted = false;  
        CommandController.End();        
    }

    public bool HasGameStarted() {
        return hasGameStarted;
    }

    private Vector2 MirrorPosition(Vector2 position) {
        return new Vector2(-position.x, -position.y);
    }
}
