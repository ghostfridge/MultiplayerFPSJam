using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ClientStateManager : NetworkBehaviour {
    [SerializeField] private SpawnHandler spawnHandler;
    [SerializeField] private Canvas lobbyCanvas;
    [SerializeField] private Transform lobbyPlayerList;
    [SerializeField] private GameObject lobbyPlayerCard;

    public NetworkList<LobbyPlayerData> lobbyPlayers;

    private void Start() {
        lobbyPlayers = new NetworkList<LobbyPlayerData>();

        CloseLobbyUI();
    }

    public override void OnNetworkSpawn() {
        lobbyPlayers.OnListChanged += UpdateLobbyPlayerList;


        if (IsServer) {
            lobbyPlayers.Clear();
            foreach (KeyValuePair<ulong, NetworkClient> client in NetworkManager.Singleton.ConnectedClients) {
                lobbyPlayers.Add(new LobbyPlayerData(client.Key));
            }

            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }

        OpenLobbyUI();
    }

    public override void OnNetworkDespawn() {
        lobbyPlayers.OnListChanged -= UpdateLobbyPlayerList;

        if (IsServer) {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnect;

            lobbyPlayers.Clear();
        }

        ClearLobbyPlayerList();
        CloseLobbyUI();
    }

    private void OnClientConnected(ulong clientId) {
        lobbyPlayers.Add(new LobbyPlayerData(clientId));
    }

    private void OnClientDisconnect(ulong clientId) {
        lobbyPlayers.Remove(new LobbyPlayerData(clientId));
    }

    private void UpdateLobbyPlayerList(NetworkListEvent<LobbyPlayerData> changeEvent) {
        ClearLobbyPlayerList();

        for (int i = 0; i < lobbyPlayers.Count; i++) {
            LobbyPlayerData lobbyPlayer = lobbyPlayers[i];
            GameObject playerCard = Instantiate(lobbyPlayerCard, lobbyPlayerList);
            playerCard.GetComponentInChildren<TMP_Text>().text = lobbyPlayer.prettyName;
        }
    }

    private void ClearLobbyPlayerList() {
        int childCount = lobbyPlayerList.childCount;
        for (int i = 0; i < childCount; i++) {
            Destroy(lobbyPlayerList.GetChild(i).gameObject);
        }
    }

    private void OpenLobbyUI() {
        lobbyCanvas.gameObject.SetActive(true);
    }

    private void CloseLobbyUI() {
        lobbyCanvas.gameObject.SetActive(false);
    }

    public struct LobbyPlayerData : INetworkSerializeByMemcpy, IEquatable<LobbyPlayerData> {
        public ulong clientId;
        public string prettyName {
            get {
                return $"Player {clientId + 1}";
            }
        }

        public LobbyPlayerData(ulong clientId) {
            this.clientId = clientId;
        }

        public bool Equals(LobbyPlayerData other) {
            return this.clientId == other.clientId;
        }
    }
}
