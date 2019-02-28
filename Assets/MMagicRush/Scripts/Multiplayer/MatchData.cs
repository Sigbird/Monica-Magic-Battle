using UnityEngine;

namespace YupiPlay.MMB.Multiplayer {
  public class MatchData : MonoBehaviour {
    public static MatchData Instance;

    public PlayerData Server;
    public PlayerData Client;
    void Awake() {
      if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        return;
      }

      Destroy(this.gameObject);
    }

    public PlayerData CreatePlayer(string username, int hero, int skill) {
      return new PlayerData(username, hero, skill);
    }

    public MatchData SetServer(string username, int hero, int skill) {
      Server = CreatePlayer(username, hero, skill);
      return this;
    }

    public MatchData SetClient(string username, int hero, int skill) {
      Client = CreatePlayer(username, hero, skill);
      return this;
    }

    public MatchData Reset() {
      Server = null;
      Client = null;
      return this;
    }
  }

  
}
