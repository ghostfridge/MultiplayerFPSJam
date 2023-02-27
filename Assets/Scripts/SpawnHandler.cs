using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnHandler : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefab;

    public override void OnNetworkSpawn() {
        if (!IsOwner) return;

        foreach (NetworkClient client in NetworkManager.ConnectedClientsList) {
            if (client.PlayerObject == null || !client.PlayerObject.IsSpawned) {
                SpawnPlayerServerRpc(client.ClientId);
            }
        }

        NetworkManager.OnClientConnectedCallback += SpawnPlayerServerRpc;
        // asdasd
    }

    public override void OnNetworkDespawn() {
        if (!IsOwner) return;

        NetworkManager.OnClientConnectedCallback -= SpawnPlayerServerRpc;
    }

    [ServerRpc]
    private void SpawnPlayerServerRpc(ulong clientId) {
        NetworkObject player = GameObject.Instantiate(playerPrefab, GetSpawnPoint(), Quaternion.identity).GetComponent<NetworkObject>();
        player.SpawnAsPlayerObject(clientId, true);
    }

    private Vector3 GetSpawnPoint() {
        PlayerSpawnpoint[] spawnpoints = FindObjectsByType<PlayerSpawnpoint>(FindObjectsSortMode.None);
        foreach (var spawnpoint in spawnpoints) {
            if (spawnpoint.isOccupied.Value) continue;

            spawnpoint.isOccupied.Value = true;

            return spawnpoint.transform.position;
        }

        return Vector3.zero;
    }
}
