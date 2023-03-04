public struct RoomPlayer {
    public int clientId;
    public string prettyName {
        get {
            return $"Player {clientId + 1}{(isLeader ? " (Leader)" : "")}";
        }
    }
    public bool isLeader;

    public RoomPlayer(int clientId) {
        this.clientId = clientId;
        this.isLeader = false;
    }

    public bool Equals(RoomPlayer other) {
        return this.clientId == other.clientId;
    }
}
