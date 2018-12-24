using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchClock : Bolt.EntityEventListener<IMatchClock> {
    public Text signalText;
    
    public static bool AllowInputs = false;

    int countdown = 3;    

	public override void Attached() {
        if (entity.isOwner && BoltNetwork.IsServer) {
            state.MatchStarted = false;
        }

        state.AddCallback("MatchStarted", MatchStartedChanged);
        state.AddCallback("StartCountdown", StartCountDownChanged);
    }

    void MatchStartedChanged() {
        AllowInputs = state.MatchStarted;
    }

    void StartCountDownChanged() {
        signalText.text = state.StartCountdown.ToString();

        if (state.StartCountdown == 0) {
            signalText.text = "Go!";
            StartCoroutine(StartGame());
        }
    }

    public void StartCountDown() {
        if (BoltNetwork.IsServer && entity.isOwner) {
            StartCoroutine(Countdown());
        }
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(1);
        signalText.gameObject.SetActive(false);

        if (entity.isOwner) {
            state.MatchStarted = true;
        }
    }

    IEnumerator Countdown() {
        while (countdown > 0) {
            Debug.Log(countdown);
            state.StartCountdown = countdown;
            yield return new WaitForSeconds(1);

            countdown--;
            state.StartCountdown = countdown;
        }                                
    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
