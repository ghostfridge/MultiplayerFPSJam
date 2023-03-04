using System;
using Unity.Netcode;

public struct LobbyPlayer : INetworkSerializeByMemcpy, IEquatable<LobbyPlayer> {
    public ulong clientId;
    public string prettyName {
        get {
            return $"Player {clientId + 1}";
        }
    }

    public LobbyPlayer(ulong clientId) {
        this.clientId = clientId;
    }

    public bool Equals(LobbyPlayer other) {
        return this.clientId == other.clientId;
    }
}
