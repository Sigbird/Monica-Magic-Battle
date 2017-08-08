using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public int Lives = 3;
    private int livesCounter;
    public ProtoGameUI GameUI;
	// Use this for initialization
	void Start () {
        livesCounter = Lives;
        GameUI.SetLives(livesCounter);
	}		

    private void OnCollisionEnter2D(Collision2D collision) {
        livesCounter--;
        GameUI.SetLives(livesCounter);

        if (livesCounter == 0) {
            GameUI.ShowEnd();
            NetGameController.Instance.EndGame();

            StartCoroutine(BackToMenuOnEnd());
        }
    }

    private IEnumerator BackToMenuOnEnd() {
        yield return new WaitForSeconds(3);

        SceneTestHelper.LoadMenu();
    }
}
