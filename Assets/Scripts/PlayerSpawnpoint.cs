using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class PlayerSpawnpoint : NetworkBehaviour {
    [SyncVar]
    [HideInInspector] public bool isOccupied;
}
