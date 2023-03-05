using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChatMessage {
    public string content;
    public string sender;

    public ChatMessage(string content, string sender) {
        this.content = content;
        this.sender = sender;
    }
}
