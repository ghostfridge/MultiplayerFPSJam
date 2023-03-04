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

    private void Start() {
        CloseLobbyUI();
    }

    public void StartGame() {
        StartGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc(ServerRpcParams serverRpcParams = default) {
        // Check if leader, all clients are connected, map selected, etc

        // Temporary check
        if (NetworkManager.ConnectedClientsIds[0] != serverRpcParams.Receive.SenderClientId) {
            Debug.LogWarning("Only the owner can start the game!");
            return;
        }

        SpawnCharacterClientRpc();
    }

    [ClientRpc]
    public void SpawnCharacterClientRpc() {
        spawnHandler.SpawnPlayerServerRpc();
        CloseLobbyUI();
    }

    public override void OnNetworkSpawn() {
        PlayerManager.Singelton.ConnectedPlayers.OnListChanged += ConnectedPlayersListChanged;
        UpdateLobbyPlayerList();

        OpenLobbyUI();
    }

    public override void OnNetworkDespawn() {
        PlayerManager.Singelton.ConnectedPlayers.OnListChanged -= ConnectedPlayersListChanged;

        ClearLobbyPlayerList();
        CloseLobbyUI();
    }

    private void ConnectedPlayersListChanged(NetworkListEvent<LobbyPlayer> changeEvent) {
        UpdateLobbyPlayerList();
    }

    private void UpdateLobbyPlayerList() {
        ClearLobbyPlayerList();

        for (int i = 0; i < PlayerManager.Singelton.ConnectedPlayers.Count; i++) {
            LobbyPlayer lobbyPlayer = PlayerManager.Singelton.ConnectedPlayers[i];
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
