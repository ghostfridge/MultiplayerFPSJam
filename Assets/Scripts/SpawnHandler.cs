using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class SpawnHandler : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(NetworkConnection conn = null) {
        GameObject player = Instantiate(playerPrefab, GetSpawnPoint(), Quaternion.identity);
        Spawn(player, conn);
    }

    private Vector3 GetSpawnPoint() {
        PlayerSpawnpoint[] spawnpoints = FindObjectsByType<PlayerSpawnpoint>(FindObjectsSortMode.None);
        foreach (PlayerSpawnpoint spawnpoint in spawnpoints) {
            if (!spawnpoint.isOccupied) {
                spawnpoint.isOccupied = true;
                return spawnpoint.transform.position;
            }
        }

        Debug.LogWarning("No unoccupied spawnpoint found, falling back to zero. (Missing a spawnpoint?)");
        return Vector3.zero;
    }
}
