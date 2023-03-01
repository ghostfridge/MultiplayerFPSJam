using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnHandler : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefab;

    [ServerRpc]
    public void SpawnPlayerServerRpc(ulong clientId, int team) {
        NetworkObject player = GameObject.Instantiate(playerPrefab, GetSpawnPoint(team), Quaternion.identity).GetComponent<NetworkObject>();
        player.SpawnAsPlayerObject(clientId, true);
    }

    private Vector3 GetSpawnPoint(int point) {
        PlayerSpawnpoint[] spawnpoints = FindObjectsByType<PlayerSpawnpoint>(FindObjectsSortMode.None);

        return spawnpoints[point].transform.position;
    }
}
