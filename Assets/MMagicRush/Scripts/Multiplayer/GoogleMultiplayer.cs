#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

namespace YupiPlay {
	public class GoogleMultiplayer :  MonoBehaviour, RealTimeMultiplayerListener {

        const int MinOpponents = 1, MaxOpponents = 1;
        const int GameVariant = 0;

        private static GoogleMultiplayer instance = null;

        public enum Statuses {
            CANCREATEGAME, ROOMCONNECTED, WAITING
        }

        public static Statuses Status {
            get {
                return status;
            }
        }

        private static Statuses status = Statuses.CANCREATEGAME;

        public delegate void RoomConnection();
        public static event RoomConnection OnRoomConnectedSuccess;
        public static event RoomConnection OnRoomConnectedFailure;
        public static event RoomConnection OnLeftGame;
        public static event RoomConnection OnWaitingForGame;

        public delegate void RoomSetupProgress(float progress);
        public static event RoomSetupProgress OnGameSetupProgress;

        public delegate void ParticipantLeft(Participant paticipant);
        public static event ParticipantLeft OnParticipantLeftGame;

        public delegate void PeerConnection(string[] participantIds);
        public static event PeerConnection OnPeersConnectedGame;
        public static event PeerConnection OnPeersDisconnectedGame;

        public delegate void MessageReceived(bool isReliable, string senderId, byte[] data);
        public static event MessageReceived OnMessageReceived;

        public static void QuickGame() {			
            
                status = Statuses.WAITING;
				NetworkStateManager.Instance.Reset();
                instance = new GoogleMultiplayer();
                PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents, GameVariant, instance);

                if (OnWaitingForGame != null) {
                    OnWaitingForGame();
                }
            
        }

        public static void InviteToGame() {
            if (status == Statuses.CANCREATEGAME) {         
                status = Statuses.WAITING;
				NetworkStateManager.Instance.Reset();
                instance = new GoogleMultiplayer();
                PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents, GameVariant, instance);

                if (OnWaitingForGame != null) {
                    OnWaitingForGame();
                }
            }
        }

        public static void AcceptFromInbox() {
            if (status == Statuses.CANCREATEGAME) {         
                status = Statuses.WAITING;
				NetworkStateManager.Instance.Reset();
                instance = new GoogleMultiplayer();
                PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(instance);

                if (OnWaitingForGame != null) {
                    OnWaitingForGame();
                }   
            }

        }

        public static void AcceptInvitation(string invitationId) {
            if (status == Statuses.CANCREATEGAME) {
                status = Statuses.WAITING;
				NetworkStateManager.Instance.Reset();
                instance = new GoogleMultiplayer();
                PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, instance);

                if (OnWaitingForGame != null) {
                    OnWaitingForGame();
                }   
            }

        }

        public void OnRoomSetupProgress(float progress) {
            if (OnGameSetupProgress != null) {
                OnGameSetupProgress(progress);
            }
        }

        public void OnRoomConnected(bool success) {
            if (success) {
                status = Statuses.ROOMCONNECTED;
                if (OnRoomConnectedSuccess != null) {
                    OnRoomConnectedSuccess();
                }
            } else {
				status = Statuses.CANCREATEGAME;
                if (OnRoomConnectedFailure != null) {
                    OnRoomConnectedFailure();
                }
            }
        }

        public void OnLeftRoom() {
            status = Statuses.CANCREATEGAME;
            if (OnLeftGame != null) {
                OnLeftGame();
            }
        }

        public void OnParticipantLeft(Participant participant) {
            if (OnParticipantLeftGame != null) {
                OnParticipantLeftGame(participant);
            }
        }

        public void OnPeersConnected(string[] participantIds) {
            if (OnPeersConnectedGame != null) {
                OnPeersConnectedGame(participantIds);
            }
        }

        public void OnPeersDisconnected(string[] participantIds) {
            if (OnPeersDisconnectedGame != null) {
                OnPeersDisconnectedGame(participantIds);
            }
        }

        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
            if (OnMessageReceived != null) {
                OnMessageReceived(isReliable, senderId, data);
            }
        }
    }
}
#endif