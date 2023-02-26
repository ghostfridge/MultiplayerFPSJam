using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ConnectionGUI : MonoBehaviour {
    private NetworkManager networkManager;
    private UnityTransport unityTransport;

    private string address = "127.0.0.1";
    private string port = "7777";

    private void Awake() {
        networkManager = GetComponent<NetworkManager>();
        unityTransport = GetComponent<UnityTransport>();
    }

    private void OnGUI() {
        GUIStyle boxStyle = new GUIStyle("Box");

        GUILayout.BeginVertical("Connection", boxStyle, GUILayout.MinWidth(120));
        GUILayout.Space(20);
        if (!networkManager.IsClient) {

            address = GUILayout.TextField(address);
            port = GUILayout.TextField(port);
            if (GUILayout.Button("Host")) {
                UpdateConnectionData();
                networkManager.StartHost();
            }
            if (GUILayout.Button("Join")) {
                UpdateConnectionData();
                networkManager.StartClient();
            }
        } else {
            if (networkManager.IsHost) {
                GUILayout.Label("Host");
            } else {
                GUILayout.Label("Client");
            }
            GUILayout.Label($"Address: {unityTransport.ConnectionData.Address}");
            GUILayout.Label($"Port: {unityTransport.ConnectionData.Port}");
            if (GUILayout.Button("Disconnect")) {
                networkManager.Shutdown();
            }
        }
        GUILayout.EndVertical();
    }

    private void UpdateConnectionData() {
        unityTransport.ConnectionData.Address = address;
        if (ushort.TryParse(port, out ushort ushPort)) {
            unityTransport.ConnectionData.Port = ushPort;
        }
    }
}
