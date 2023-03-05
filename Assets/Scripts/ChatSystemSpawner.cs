using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class ChatSystemSpawner : NetworkBehaviour {
    public ChatSystem chatSystem;

    private void Start() {
        Debug.Log("yo ");
        GameObject go = Instantiate(chatSystem.gameObject);
        Spawn(go);
    }
}
