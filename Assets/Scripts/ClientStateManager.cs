using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;

public class ClientStateManager : NetworkBehaviour {
    [SerializeField] private SpawnHandler spawnHandler;
    [SerializeField] private Canvas lobbyCanvas;
    [SerializeField] private Transform lobbyPlayerList;
    [SerializeField] private GameObject lobbyPlayerCard;

    private void Start() {
        CloseLobbyUI();
    }

    public void StartGame() {
        StartGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc(NetworkConnection conn = null) {
        // Check if leader, all clients are connected, map selected, etc

        if (RoomManager.Singelton.GetConnectedPlayer(conn.ClientId).isLeader) {
            SpawnCharacterObserverRpc();
        } else {
            Debug.LogWarning("Only the owner can start the game!");
        }
    }

    [ObserversRpc]
    public void SpawnCharacterObserverRpc() {
        spawnHandler.SpawnPlayerServerRpc();
        CloseLobbyUI();
    }

    public override void OnStartClient() {
        base.OnStartClient();

        RoomManager.Singelton.ConnectedPlayers.OnChange += ConnectedPlayersListChanged;
        UpdateLobbyPlayerList();

        OpenLobbyUI();
    }

    public override void OnStopClient() {
        base.OnStopClient();

        RoomManager.Singelton.ConnectedPlayers.OnChange -= ConnectedPlayersListChanged;

        ClearLobbyPlayerList();
        CloseLobbyUI();
    }

    private void ConnectedPlayersListChanged(SyncListOperation op, int index, RoomPlayer oldItem, RoomPlayer newItem, bool asServer) {
        UpdateLobbyPlayerList();
    }

    private void UpdateLobbyPlayerList() {
        ClearLobbyPlayerList();

        for (int i = 0; i < RoomManager.Singelton.ConnectedPlayers.Count; i++) {
            RoomPlayer lobbyPlayer = RoomManager.Singelton.ConnectedPlayers[i];
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
}
