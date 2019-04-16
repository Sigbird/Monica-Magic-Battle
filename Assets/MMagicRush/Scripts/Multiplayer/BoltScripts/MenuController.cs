using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using UdpKit;
using Bolt;
using YupiPlay.MMB;
using Bolt.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YupiPlay.MMB.Multiplayer {
    public class MenuController : Bolt.GlobalEventListener {
        public int ClientWaitTimeout = 10;
        public int MatchmakingTimeout = 300;
        public int LaunchDelay = 3;         
        public string MPScene = "GamePlayReviewMulti";
        public UnityEvent OnConnection;
        public UnityEvent OnMatchmakingTimeout;
		public GameObject LoadingCanvas;
		public Scrollbar ProgressionBar;
		public bool MultiPlayerOnly;
		public bool connectingtoSomeone;

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
			connectingtoSomeone = false;
            limit = BoltRuntimeSettings.instance.GetConfigCopy().serverConnectionLimit;

        }

		void Update(){

			//Debug.Log ("CONECTED: " + BoltNetwork.);


		}



        public void StartServer() {

            MatchData.Instance.Reset();

            myUsername = PlayerInfo.Instance.Username;
            myHero = PlayerPrefs.GetInt("SelectedCharacter", 0);
            BoltLauncher.StartServer();
        }

		public void StartServer2() {
//			Shutdown = false;
//
//			MatchData.Instance.Reset();
//
//			myUsername = PlayerInfo.Instance.Username;
//			myHero = PlayerPrefs.GetInt("SelectedCharacter", 0);
//			BoltLauncher.StartServer();
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
			connectingtoSomeone = true;
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
            registerDoneCallback(StartServer2);
        }

		public void StartOffilne(){
			if (MultiPlayerOnly == true) {
				StartCoroutine (TrySearchingOpponents ());
			} else {
				Debug.Log ("STARTING OFFLINE");
				PlayerPrefs.SetString ("Multiplayer", "False");
				PlayerPrefs.SetInt ("round",1);
				PlayerPrefs.SetInt ("playerCharges",0);
				PlayerPrefs.SetInt ("enemyCharges",0);
				StartCoroutine (AsynchronousLoad ("GamePlayReview"));
			}
		}

		public void PlayOffline(){
			Debug.Log ("STARTING OFFLINE");
			PlayerPrefs.SetString ("Multiplayer", "False");
			PlayerPrefs.SetInt ("round",1);
			PlayerPrefs.SetInt ("playerCharges",0);
			PlayerPrefs.SetInt ("enemyCharges",0);
			StartCoroutine (AsynchronousLoad ("GamePlayReview"));
		}

		public void UniquePlayButton(){
			StartCoroutine (TrySearchingOpponents ());

		}
			
		IEnumerator AsynchronousLoad (string scene)
		{
			yield return null;

			AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
			ao.allowSceneActivation = false;

			while (! ao.isDone)
			{
				LoadingCanvas.SetActive (true);
				// [0, 0.9] > [0, 1]
				float progress = Mathf.Clamp01(ao.progress / 0.9f);
				Debug.Log("Loading progress: " + (progress * 100) + "%");
				ProgressionBar.size = progress;
				// Loading completed
				if (ao.progress == 0.9f)
				{
					yield return new WaitForSeconds(1.0f);
					Debug.Log("Press a key to start");
					//if (Input.GetKeyDown(KeyCode.A))
					//				if (LoadingCanvas != null) {
					//					Destroy (LoadingCanvas.gameObject);
					//				}
					ao.allowSceneActivation = true;
					//ao.isDone = true;
				}

				yield return null;
			}
		}

        IEnumerator WaitAndLaunchGame() {
            yield return new WaitForSeconds(LaunchDelay);

            BoltNetwork.LoadScene("GamePlayReviewMulti");
        }

        IEnumerator WaitForServers() {
            yield return new WaitForSeconds(10);

           // BoltNetwork.ShutdownImmediate();
            BoltLauncher.Shutdown();
            //BoltLauncher.StartServer();
        }

		IEnumerator TrySearchingOpponents() {
			Debug.Log ("STARTING SERVER");
			StartServer ();
			yield return new WaitForSeconds(30);
			if (connectingtoSomeone == false) {
				
				BoltLauncher.Shutdown ();
				yield return new WaitForSeconds (2);
				Debug.Log ("STARTING CLIENT");
				StartClient ();
				yield return new WaitForSeconds (30);
				if (connectingtoSomeone == false) {
					BoltLauncher.Shutdown ();

					yield return new WaitForSeconds (2);
					StartOffilne ();
				}
			}
			// BoltNetwork.ShutdownImmediate();

			//BoltLauncher.StartServer();
		}

        IEnumerator WaitForTimeout() {
            yield return new WaitForSeconds(MatchmakingTimeout);

            OnMatchmakingTimeout.Invoke();
        }

        public void ShutdownMultiplayer() {
			BoltLauncher.Shutdown();
			//Shutdown = true;
			//BoltNetwork.Server.Disconnect();
        }
    }

    
}

