using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;

public class RoomManager : NetworkBehaviour {
    public static RoomManager Singelton { get; private set; }
    [SyncObject]
    public readonly SyncList<RoomPlayer> ConnectedPlayers = new SyncList<RoomPlayer>();

    private void Awake() {
        Singelton = this;
    }

    public override void OnStartClient() {
        base.OnStartClient();

        if (IsServer) {
            // DEDSER: Change leadership detection
            ConnectedPlayers.Clear();
            foreach (KeyValuePair<int, NetworkConnection> client in NetworkManager.ServerManager.Clients) {
                RoomPlayer roomPlayer = new RoomPlayer(client.Key);
                roomPlayer.isLeader = true;
                ConnectedPlayers.Add(roomPlayer);
            }

            NetworkManager.ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;
        }
    }

    public override void OnStopClient() {
        base.OnStopClient();

        if (IsServer) {
            NetworkManager.ServerManager.OnRemoteConnectionState -= OnRemoteConnectionState;

            ConnectedPlayers.Clear();
        }
    }

    private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args) {
        if (args.ConnectionState == RemoteConnectionState.Started) {
            ConnectedPlayers.Add(new RoomPlayer(conn.ClientId));
        } else if (args.ConnectionState == RemoteConnectionState.Stopped) {
            ConnectedPlayers.RemoveAt(ConnectedPlayers.FindIndex((RoomPlayer roomPlayer) => roomPlayer.clientId == conn.ClientId));
        }
    }

    public RoomPlayer GetConnectedPlayer(int clientId) {
        return ConnectedPlayers.Find((RoomPlayer roomPlayer) => roomPlayer.clientId == clientId);
    }

    public RoomPlayer GetCurrentPlayer() {
        return ConnectedPlayers.ToList().Find((RoomPlayer roomPlayer) => roomPlayer.clientId == NetworkManager.ClientManager.Connection.ClientId);
    }
}
