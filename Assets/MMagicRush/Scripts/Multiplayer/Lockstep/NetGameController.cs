using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;
using YupiPlay;

public class NetGameController : MonoBehaviour, INetGameController {
    public PlayerController PlayerController;
    public EnemyRemoteController EnemyController;    
    public ProtoGameUI GameUI;

    private Vector2 targetPosition;    

    public static NetGameController Instance { get { return instance; } set { } }

    private static NetGameController instance;

    private bool hasGameStarted = false;

    private CommandController input;

    void Awake() {
        if (instance == null) {
            instance = this;
            NetClock.Instance.SetNetGameControllerInstance(this);
        } else {
            Destroy(this.gameObject);
        }
    }
    
    void Start () {
#if UNITY_EDITOR
        StartClock();
#else
          NetworkSessionManager.Instance.SendReady();
#endif

        input = PlayerController.GetComponent<CommandController>();
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

    public void StartClock() {
        NetClock.Instance.StartClock();        
    }

    public void StartGame() {
        StartCoroutine(StartUITimer());
    }

    private IEnumerator StartUITimer() {                
        GameUI.ShowStart();
        yield return new WaitForSeconds(2);
        GameUI.HideStart();
        hasGameStarted = true;
    }

    public void EndGame() {
        hasGameStarted = false;  
        input.End();        
    }

    public bool HasGameStarted() {
        return hasGameStarted;
    }

    private Vector2 MirrorPosition(Vector2 position) {
        return new Vector2(-position.x, -position.y);
    }
}
