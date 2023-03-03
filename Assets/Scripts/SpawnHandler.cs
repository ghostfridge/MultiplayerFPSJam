using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnHandler : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default) {
        ulong clientId = serverRpcParams.Receive.SenderClientId;

        NetworkObject player = GameObject.Instantiate(playerPrefab, GetSpawnPoint(), Quaternion.identity).GetComponent<NetworkObject>();
        player.SpawnAsPlayerObject(clientId, true);
    }

    private Vector3 GetSpawnPoint() {
        PlayerSpawnpoint[] spawnpoints = FindObjectsByType<PlayerSpawnpoint>(FindObjectsSortMode.None);
        foreach (PlayerSpawnpoint spawnpoint in spawnpoints) {
            if (!spawnpoint.isOccupied.Value) {
                spawnpoint.isOccupied.Value = true;
                return spawnpoint.transform.position;
            }
        }

        Debug.LogWarning("No unoccupied spawnpoint found, falling back to zero. (Missing a spawnpoint?)");
        return Vector3.zero;
    }
}
