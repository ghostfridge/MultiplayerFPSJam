using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour {
    public static PlayerManager Singelton { get; private set; }
    public NetworkList<LobbyPlayer> ConnectedPlayers;

    private void Awake() {
        Singelton = this;
        ConnectedPlayers = new NetworkList<LobbyPlayer>();
    }

    public override void OnNetworkSpawn() {
        if (IsServer) {
            ConnectedPlayers.Clear();
            foreach (KeyValuePair<ulong, NetworkClient> client in NetworkManager.Singleton.ConnectedClients) {
                ConnectedPlayers.Add(new LobbyPlayer(client.Key));
            }

            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }
    }

    public override void OnNetworkDespawn() {
        if (IsServer) {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnect;

            ConnectedPlayers.Clear();
        }
    }

    private void OnClientConnected(ulong clientId) {
        ConnectedPlayers.Add(new LobbyPlayer(clientId));
    }

    private void OnClientDisconnect(ulong clientId) {
        ConnectedPlayers.Remove(new LobbyPlayer(clientId));
    }
}
