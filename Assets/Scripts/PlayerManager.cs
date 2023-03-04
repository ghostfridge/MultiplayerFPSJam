using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;

public class PlayerManager : NetworkBehaviour {
    public static PlayerManager Singelton { get; private set; }
    [SyncObject]
    public readonly SyncList<RoomPlayer> ConnectedPlayers = new SyncList<RoomPlayer>();

    private void Awake() {
        Singelton = this;
    }

    [Server]
    public override void OnStartClient() {
        base.OnStartClient();

        // DEDSER: Change leadership detection
        ConnectedPlayers.Clear();
        foreach (KeyValuePair<int, NetworkConnection> client in NetworkManager.ServerManager.Clients) {
            RoomPlayer roomPlayer = new RoomPlayer(client.Key);
            roomPlayer.isLeader = true;
            ConnectedPlayers.Add(roomPlayer);
        }

        NetworkManager.ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;
    }

    [Server]
    public override void OnStopClient() {
        base.OnStopClient();

        NetworkManager.ServerManager.OnRemoteConnectionState -= OnRemoteConnectionState;

        ConnectedPlayers.Clear();
    }

    private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args) {
        // ConnectedPlayers.Add(new RoomPlayer(clientId));
    }

    public RoomPlayer GetConnectedPlayer(int clientId) {
        return ConnectedPlayers.Find((RoomPlayer roomPlayer) => roomPlayer.clientId == clientId);
    }

    public RoomPlayer GetCurrentPlayer() {
        return ConnectedPlayers.ToList().Find((RoomPlayer roomPlayer) => roomPlayer.clientId == NetworkManager.ClientManager.Connection.ClientId);
    }
}
