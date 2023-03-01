using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ClientStateManager : NetworkBehaviour {
    [SerializeField] private SpawnHandler spawnHandler;
    [SerializeField] private Canvas pregameCanvas;
    private ulong clientId;

    private void Start() {
        CloseTeamPrompt();

        NetworkManager.OnClientConnectedCallback += OpenTeamPrompt;
    }

    private void JoinTeam(int team) {
        if (team == 0 || team == 1) {
            spawnHandler.SpawnPlayerServerRpc(clientId, team);
            CloseTeamPrompt();
        }
    }

    private void OpenTeamPrompt(ulong _clientId) {
        clientId = _clientId;
        pregameCanvas.gameObject.SetActive(true);
    }

    private void CloseTeamPrompt() {
        pregameCanvas.gameObject.SetActive(false);
    }

    private void OnEnable() {
        Button[] buttons = pregameCanvas.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++) {
            int team = i;
            buttons[i].onClick.AddListener(() => JoinTeam(team));
        }
    }
}
