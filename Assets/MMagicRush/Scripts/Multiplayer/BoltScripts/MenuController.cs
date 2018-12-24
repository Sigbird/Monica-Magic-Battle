using UnityEngine;
using System.Collections;
using System;
using UdpKit;

public class MenuController : Bolt.GlobalEventListener {
    public int ClientWaitTimeout = 10;

    int limit = 0;
    Coroutine clientwait = null;
    

    private void Awake() {
        limit = BoltRuntimeSettings.instance.GetConfigCopy().serverConnectionLimit;        
    }

    public void StartServer() {
        BoltLauncher.StartServer();
    }

    public void StartClient() {
        BoltLauncher.StartClient();
    }

    public void StartMatchmaking() {        
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone() {
        if (BoltNetwork.IsServer) {            
            var MatchName = ServerCallbacks.Instance.MatchName = Guid.NewGuid().ToString();

            // Adiciona detalhes do servidor para os clientes filtrarem
            var roomInfo = new RoomInfo(0, true);
            BoltNetwork.SetServerInfo(MatchName, roomInfo);
        }

        if (BoltNetwork.IsClient) {
            clientwait = StartCoroutine(WaitForServers());
        }
    }

    IEnumerator WaitForServers() {
        yield return new WaitForSeconds(10);

        BoltLauncher.Shutdown();
        BoltLauncher.StartServer();
    }

    // Só carrega a cena após um oponente conectar
    public override void Connected(BoltConnection connection) {
        if (BoltNetwork.IsServer) {            
            BoltNetwork.LoadScene("JogoMulti");
        }
    }    

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList) {
        Debug.Log("SESSION LIST UPDATED");
        // Se não há serivodres para procurar inicie como servidor
        if (BoltNetwork.IsClient && sessionList.Count == 0) {
            BoltLauncher.Shutdown();
            BoltLauncher.StartServer();            
            return;
        }

        foreach (var session in sessionList) {
            UdpSession photonSession = session.Value as UdpSession;

            var connectionsNum = photonSession.ConnectionsCurrent;
            var roomInfo = photonSession.GetProtocolToken() as RoomInfo;            

            // Conecta somente em salas abertas com menos de 2 conexões
            if (photonSession.Source == UdpSessionSource.Photon && 
                connectionsNum < limit && roomInfo.Open) {
                StopCoroutine(clientwait);
                BoltNetwork.Connect(photonSession);                
            }           
        }
    }
}
