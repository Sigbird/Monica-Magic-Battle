using UnityEngine;
using UnityEngine.UI;

public class OpponentInfo : MonoBehaviour {
    public Text opponentInfo;    

    public void SetOpponentInfo(string displayName, int hero) {
        var heroName = "Mônica";

        if (hero == 1) {
            heroName = "Cebolinha";
        }

        opponentInfo.text = displayName + "\n" + heroName;
        opponentInfo.gameObject.SetActive(true);
    }
}
