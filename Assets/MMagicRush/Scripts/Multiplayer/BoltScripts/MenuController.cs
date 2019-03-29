using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using UdpKit;
using Bolt;
using YupiPlay.MMB;
using Bolt.Utils;

namespace YupiPlay.MMB.Multiplayer {
    public class MenuController : Bolt.GlobalEventListener {
        public int ClientWaitTimeout = 10;
        public int MatchmakingTimeout = 300;
        public int LaunchDelay = 3;         
        public string MPScene = "GamePlayReviewMulti";
        public UnityEvent OnConnection;
        public UnityEvent OnMatchmakingTimeout;

        public event Action<string, int> OnOpponentInfoEvent;

        int limit = 0;
        Coroutine clientwait = null;
        Coroutine matchmakingTimeout = null;
        
        string matchName;
        RoomInfo roomInfo;
        
        // Client envia
        PlayerToken playerToken;

        // Server recebe
        PlayerToken clientToken;

        string myUsername;
        int myHero;
        int mySkill = 0;

        void Awake() {
            limit = BoltRuntimeSettings.instance.GetConfigCopy().serverConnectionLimit;
        }

        public void StartServer() {
            MatchData.Instance.Reset();

            myUsername = PlayerInfo.Instance.Username;
            myHero = PlayerPrefs.GetInt("SelectedCharacter", 0);
            BoltLauncher.StartServer();
        }

        public void StartClient() {
            MatchData.Instance.Reset();

            myUsername = PlayerInfo.Instance.Username;
            myHero = PlayerPrefs.GetInt("SelectedCharacter", 0);
            BoltLauncher.StartClient();
        }

        public void StartMatchmaking() {
            myUsername = PlayerInfo.Instance.Username;
            BoltLauncher.StartClient();

            matchmakingTimeout = StartCoroutine(WaitForTimeout());
        }

        public override void BoltStartDone() {
            if (BoltNetwork.IsServer) {
                matchName = ServerCallbacks.Instance.MatchName = Guid.NewGuid().ToString();
                ServerCallbacks.Instance.Username = myUsername;
                ServerCallbacks.Instance.Hero = myHero;
                ServerCallbacks.Instance.Skill = mySkill;

                // Adiciona detalhes do servidor para os clientes filtrarem
                roomInfo = new RoomInfo(myUsername, myHero, mySkill, true);
                BoltNetwork.SetServerInfo(matchName, roomInfo);
            }

            //if (BoltNetwork.IsClient) {
                //clientwait = StartCoroutine(WaitForServers());
            //}
        }

        // Só carrega a cena após um oponente conectar
        public override void Connected(BoltConnection connection) {
            OnConnection.Invoke();

            if (BoltNetwork.IsServer) {
                if (string.IsNullOrEmpty(myUsername)) {
                    myUsername = "Zezinho";
                }

                clientToken = connection.ConnectToken as PlayerToken;

                MatchData.Instance.SetServer(myUsername, myHero, mySkill)
                    .SetClient(clientToken.Username, clientToken.Hero, clientToken.Skill);

                CallOpponentInfoEvent(clientToken.Username, clientToken.Hero);
                StartCoroutine(WaitAndLaunchGame());
            }

            if (BoltNetwork.IsClient) {
                if (string.IsNullOrEmpty(myUsername)) {
                    myUsername = "Juninho";
                }
            }
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList) {
            // Se não há serivodres para procurar inicie como servidor
            // if (BoltNetwork.IsClient && sessionList.Count == 0) {
            //     BoltLauncher.Shutdown();
            //     BoltLauncher.StartServer();
            //     return;
            // }

            foreach (var session in sessionList) {
                UdpSession photonSession = session.Value as UdpSession;

                var connectionsNum = photonSession.ConnectionsCurrent;
                var roomInfo = photonSession.GetProtocolToken() as RoomInfo;

                // Conecta somente em salas abertas com menos de 2 conexões
                if (photonSession.Source == UdpSessionSource.Photon &&
                    connectionsNum < limit && roomInfo.Open) {
                    CallOpponentInfoEvent(roomInfo.Username, roomInfo.Hero);

                    playerToken = new PlayerToken(myUsername, myHero, mySkill);
                    
                    MatchData.Instance.SetClient(myUsername, myHero, mySkill)
                        .SetServer(roomInfo.Username, roomInfo.Hero, roomInfo.Skill);
                    
                    BoltNetwork.Connect(photonSession, playerToken);
                }
            }
        }

        void CallOpponentInfoEvent(string displayName, int hero) {
            if (OnOpponentInfoEvent != null) {
                OnOpponentInfoEvent(displayName, hero);
            }
        }

        public override void BoltShutdownBegin(AddCallback registerDoneCallback) {
            registerDoneCallback(StartServer);
        }

        IEnumerator WaitAndLaunchGame() {
            yield return new WaitForSeconds(LaunchDelay);

            BoltNetwork.LoadScene("GamePlayReviewMulti");
        }

        IEnumerator WaitForServers() {
            yield return new WaitForSeconds(10);

            //BoltNetwork.ShutdownImmediate();
            BoltLauncher.Shutdown();
            //BoltLauncher.StartServer();
        }

        IEnumerator WaitForTimeout() {
            yield return new WaitForSeconds(MatchmakingTimeout);

            OnMatchmakingTimeout.Invoke();
        }

        public void ShutdownMultiplayer() {
            BoltNetwork.Shutdown();
        }
    }

    
}

