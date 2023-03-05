using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using TMPro;

public class ChatSystem : NetworkBehaviour {
    [SerializeField] private Transform messageContainer;
    [SerializeField] private GameObject chatMessage;

    [ContextMenu("Send test message")]
    public void SendGlobalMessage() {
        SendGlobalMessageServerRpc("Hello world!");
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendGlobalMessageServerRpc(string messageContent, NetworkConnection conn = null) {
        ChatMessage message = new ChatMessage(messageContent, conn.ClientId.ToString());
        SendGlobalMessageObserverRpc(message);
    }

    [ObserversRpc]
    public void SendGlobalMessageObserverRpc(ChatMessage msg) {
        Debug.Log($"{msg.sender}: {msg.content}");
        TMP_Text messageText = Instantiate(chatMessage, messageContainer).GetComponentInChildren<TMP_Text>();
        messageText.text = $"<b>{msg.sender}:</b> {msg.content}";
    }
}
