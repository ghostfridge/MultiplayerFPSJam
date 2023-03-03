using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class ClientStateManager : NetworkBehaviour {
    [SerializeField] private SpawnHandler spawnHandler;
    [SerializeField] private Canvas lobbyCanvas;
    [SerializeField] private Transform lobbyPlayerList;
    [SerializeField] private GameObject lobbyPlayerCard;

    private void Start() {
        CloseLobbyUI();

        NetworkManager.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId) {
        UpdateLobbyPlayerListServerRpc();
        OpenLobbyUI(clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateLobbyPlayerListServerRpc(ServerRpcParams serverRpcParams = default) {
        ulong myClientId = serverRpcParams.Receive.SenderClientId;
        ClientRpcParams clientRpcParams = new ClientRpcParams() {
            Send = new ClientRpcSendParams {
                TargetClientIds = new ulong[] { myClientId }
            }
        };

        LobbyPlayerData[] lobbyPlayers = NetworkManager.Singleton.ConnectedClients.Select(delegate (KeyValuePair<ulong, NetworkClient> client) {
            ulong clientId = client.Key;
            LobbyPlayerData lobbyPlayer = new LobbyPlayerData() {
                clientId = clientId,
                prettyName = $"Player {clientId + 1}{(clientId == myClientId ? " (Me)" : "")}"
            };
            return lobbyPlayer;
        }).ToArray();

        UpdateLobbyPlayerListClientRpc(lobbyPlayers, clientRpcParams);
    }

    [ClientRpc]
    private void UpdateLobbyPlayerListClientRpc(LobbyPlayerData[] lobbyPlayers, ClientRpcParams clientRpcParams) {
        foreach (LobbyPlayerData lobbyPlayer in lobbyPlayers) {
            Debug.Log($"{lobbyPlayer.clientId} joined!");

            GameObject playerCard = Instantiate(lobbyPlayerCard, lobbyPlayerList);
            playerCard.GetComponentInChildren<TMP_Text>().text = lobbyPlayer.prettyName;
        }
    }

    private void OpenLobbyUI(ulong clientId) {
        lobbyCanvas.gameObject.SetActive(true);
    }

    private void CloseLobbyUI() {
        lobbyCanvas.gameObject.SetActive(false);
    }

    public struct LobbyPlayerData : INetworkSerializeByMemcpy {
        public ulong clientId;
        public string prettyName;
    }
}
