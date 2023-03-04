using System;
using Unity.Netcode;

public struct RoomPlayer : INetworkSerializeByMemcpy, IEquatable<RoomPlayer> {
    public ulong clientId;
    public string prettyName {
        get {
            return $"Player {clientId + 1}";
        }
    }

    public RoomPlayer(ulong clientId) {
        this.clientId = clientId;
    }

    public bool Equals(RoomPlayer other) {
        return this.clientId == other.clientId;
    }
}
