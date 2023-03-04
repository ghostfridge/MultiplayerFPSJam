using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour {
    public static PlayerManager Singelton { get; private set; }
    public NetworkList<RoomPlayer> ConnectedPlayers;

    private void Awake() {
        Singelton = this;
        ConnectedPlayers = new NetworkList<RoomPlayer>();
    }

    public override void OnNetworkSpawn() {
        if (IsServer) {
            ConnectedPlayers.Clear();
            foreach (KeyValuePair<ulong, NetworkClient> client in NetworkManager.Singleton.ConnectedClients) {
                RoomPlayer roomPlayer = new RoomPlayer(client.Key);
                roomPlayer.isLeader = true;
                ConnectedPlayers.Add(roomPlayer);
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
        ConnectedPlayers.Add(new RoomPlayer(clientId));
    }

    private void OnClientDisconnect(ulong clientId) {
        ConnectedPlayers.Remove(new RoomPlayer(clientId));
    }

    public RoomPlayer GetConnectedPlayer(ulong clientId) {
        return ConnectedPlayers.ToArray().ToList().Find((RoomPlayer roomPlayer) => roomPlayer.clientId == clientId);
    }
}
