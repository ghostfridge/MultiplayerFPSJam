using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Transporting.Tugboat;

public class ConnectionGUI : MonoBehaviour {
    private NetworkManager networkManager;
    private Transport transport;

    private string address = "127.0.0.1";
    private string port = "7777";

    private void Awake() {
        networkManager = InstanceFinder.NetworkManager;
        transport = networkManager.TransportManager.GetTransport<Tugboat>();
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
                transport.StartConnection(true);
                transport.StartConnection(false);
            }
            if (GUILayout.Button("Join")) {
                UpdateConnectionData();
                transport.StartConnection(false);
            }
        } else {
            if (networkManager.IsHost) {
                GUILayout.Label("Host");
            } else {
                GUILayout.Label("Client");
            }
            GUILayout.Label($"Address: {transport.GetClientAddress()}");
            GUILayout.Label($"Port: {transport.GetPort()}");
            if (networkManager.IsHost) {
                if (GUILayout.Button("Shutdown")) {
                    transport.StopConnection(true);
                }
            } else {
                if (GUILayout.Button("Disconnect")) {
                    transport.StopConnection(false);
                }
            }
        }
        GUILayout.EndVertical();
    }

    private void UpdateConnectionData() {
        transport.SetClientAddress(address);
        if (ushort.TryParse(port, out ushort ushortPort)) {
            transport.SetPort(ushortPort);
        }
    }
}
