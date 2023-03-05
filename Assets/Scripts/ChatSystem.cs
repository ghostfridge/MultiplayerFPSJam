using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FishNet.Connection;
using FishNet.Object;
using TMPro;

public class ChatSystem : NetworkBehaviour {
    public InputMain controls;

    [SerializeField] private GameObject container;
    [SerializeField] private Transform messageContainer;
    [SerializeField] private GameObject chatMessage;
    [SerializeField] private TMP_InputField chatInput;

    public bool isOpen;

    private void Awake() {
        controls = new InputMain();

        CloseChat();
    }

    public override void OnStartClient() {
        base.OnStartClient();

        if (controls != null) {
            controls.Enable();
            controls.Player.SendMessage.performed += SendGlobalMessage;
            controls.Player.OpenChat.performed += OpenChat;
        }
    }

    public override void OnStopClient() {
        base.OnStopClient();

        if (IsOwner && controls != null) {
            controls.Disable();
            controls.Player.SendMessage.performed -= SendGlobalMessage;
            controls.Player.OpenChat.performed -= OpenChat;
        }

        CloseChat();
    }

    public void OpenChat(InputAction.CallbackContext ctx) {
        isOpen = true;

        container.GetComponent<Image>().enabled = true;
        chatInput.gameObject.SetActive(true);

        chatInput.Select();
        chatInput.ActivateInputField();
    }

    public void CloseChat() {
        isOpen = false;

        container.GetComponent<Image>().enabled = false;
        chatInput.gameObject.SetActive(false);

        chatInput.DeactivateInputField();
    }

    public void SendGlobalMessage(InputAction.CallbackContext ctx) {
        if (!isOpen || chatInput.text == "") return;

        SendGlobalMessageServerRpc(chatInput.text);
        chatInput.text = "";
        CloseChat();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendGlobalMessageServerRpc(string messageContent, NetworkConnection conn = null) {
        ChatMessage message = new ChatMessage(messageContent, conn.ClientId.ToString());
        SendGlobalMessageObserverRpc(message);
    }

    [ObserversRpc]
    public void SendGlobalMessageObserverRpc(ChatMessage msg) {
        TMP_Text messageText = Instantiate(chatMessage, messageContainer).GetComponentInChildren<TMP_Text>();
        messageText.text = $"<b>{msg.sender}:</b> {msg.content}";

        Debug.Log($"New message ({msg.sender}: {msg.content})");
    }
}
