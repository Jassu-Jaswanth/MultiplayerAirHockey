using UnityEngine;
using Unity.Netcode;

public class NetworkUpdate : NetworkBehaviour {

    Vector2 opponentTransform;
    [SerializeField] GameObject opponent;
    
    private void Update() {
        if (IsClient && !IsHost) {
            TestServerRpc(GetComponent<RectTransform>().anchoredPosition);
        }
        if (IsHost) {
            TestClientRpc(GetComponent<RectTransform>().anchoredPosition);
        }

        opponent.GetComponent<RectTransform>().anchoredPosition = opponentTransform;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TestServerRpc(Vector2 pos) {
        if (IsHost) {
            //only runs on server
            opponentTransform = -1 * pos;
        }
    }

    [ClientRpc]
    public void TestClientRpc(Vector2 pos) {
        if(IsClient && !IsHost) {
            // only runs on client
            opponentTransform = -1 * pos;
        }
    }
}