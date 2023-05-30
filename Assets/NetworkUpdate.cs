using UnityEngine;
using Unity.Netcode;

public class NetworkUpdate : NetworkBehaviour {

    Vector2 opponentTransform;
    Vector2 ballTransform;
    [SerializeField] GameObject opponent;
    [SerializeField] GameObject ball;
    
    private void Update() {
        if (IsClient && !IsHost) {
            TestServerRpc(GetComponent<RectTransform>().anchoredPosition);
        }
        if (IsHost) {
            TestClientRpc(GetComponent<RectTransform>().anchoredPosition);
            ballClientRpc(ball.GetComponent<RectTransform>().anchoredPosition);
        }

        opponent.GetComponent<RectTransform>().anchoredPosition = opponentTransform;
        if (IsClient && !IsHost) ball.GetComponent<RectTransform>().anchoredPosition = ballTransform;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TestServerRpc(Vector2 pos) {
        if (IsHost) {
            //only runs only on host
            opponentTransform = -1 * pos;
        }
    }

    [ClientRpc]
    public void TestClientRpc(Vector2 pos) {
        if(IsClient && !IsHost) {
            // only runs only on client
            opponentTransform = -1 * pos;
        }
    }

    [ClientRpc]
    public void ballClientRpc(Vector2 pos) {
        if (IsClient && !IsHost) {
            // only runs only on client
            ballTransform = -1 * pos;
        }
    }
}