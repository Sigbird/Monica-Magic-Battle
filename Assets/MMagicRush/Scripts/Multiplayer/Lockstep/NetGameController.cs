using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.MMB.Lockstep;

public class NetGameController : MonoBehaviour {

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
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Vector2 moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CommandController.Move(moveTo);
        }
	}

    public void StartGame() {
        
    }

    public void EndGame() {

    }
}
