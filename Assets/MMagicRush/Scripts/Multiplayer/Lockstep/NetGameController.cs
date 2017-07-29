using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;

public class NetGameController : MonoBehaviour {
    public GameObject PlayerHero;
    public GameObject ClickFeeback;
    public GameObject EnemyHero;

    private Vector2 targetPosition;
    private Vector2 enemyTargetPosition;

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
		
	}
	
	// Update is called once per frame
    // Update de testes
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Vector2 moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            CommandController.Move(moveTo);

            ClickFeeback.SetActive(true);
            ClickFeeback.transform.position = moveTo;

            NetClock.Instance.RegisterInputTime();
        }

        var distance = Vector2.Distance(targetPosition, PlayerHero.transform.position);
        if (distance < 0.01f) {
            PlayerHero.GetComponent<Rigidbody2D>().velocity = new Vector2();
        }

        var enemyDistance = Vector2.Distance(enemyTargetPosition, EnemyHero.transform.position);
        if (enemyDistance < 0.01f) {
            EnemyHero.GetComponent<Rigidbody2D>().velocity = new Vector2();
        }
	}

    private void MovePlayer(Vector2 pos) {
        targetPosition = pos;
        MoveObject(PlayerHero, targetPosition);
    }

    private void MoveEnemy(Vector2 pos) {
        enemyTargetPosition = pos;
        MoveObject(EnemyHero, enemyTargetPosition);
    }

    private void MoveObject(GameObject unit, Vector2 pos) {
        var direction = pos - (Vector2) unit.transform.position;
        unit.GetComponent<Rigidbody2D>().velocity = direction.normalized;
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
                MoveEnemy(position);
            } else {
                MovePlayer(position);
                NetClock.Instance.GetInputLatency();
            }            
        }
    }

    public void StartGame() {
        
    }

    public void EndGame() {

    }
}
