using UnityEngine;
using Unity.Netcode;

public class NetworkUpdate : NetworkBehaviour {

    // Vector2 opponentTransform;
    // Vector2 ballTransform;
    [SerializeField] GameObject opponent;
    [SerializeField] GameObject ball;

    NetworkVariable<Vector2> NV_clientTransform = new NetworkVariable<Vector2>(new Vector2(0f,0f),NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    NetworkVariable<Vector2> NV_hostTransform = new NetworkVariable<Vector2>(new Vector2(0f,0f),NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    NetworkVariable<Vector2> NV_ballTransform = new NetworkVariable<Vector2>(new Vector2(0f,0f),NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        
        if (IsClient && !IsHost) {
            // TestServerRpc(GetComponent<RectTransform>().anchoredPosition);
            // NV_clientTransform.Value = player.GetComponent<RectTransform>().anchoredPosition;
            clientTransformUpdateServerRpc(GetComponent<RectTransform>().anchoredPosition);
        }
        if (IsHost) {
            // TestClientRpc(GetComponent<RectTransform>().anchoredPosition);
            // ballClientRpc(ball.GetComponent<RectTransform>().anchoredPosition);
            NV_hostTransform.Value = -1 * GetComponent<RectTransform>().anchoredPosition;
            NV_ballTransform.Value = -1 * ball.GetComponent<RectTransform>().anchoredPosition;
        }
    }
    
    private void Update() {
        if (IsClient && !IsHost) {
            // TestServerRpc(GetComponent<RectTransform>().anchoredPosition);
            // NV_clientTransform.Value = player.GetComponent<RectTransform>().anchoredPosition;
            clientTransformUpdateServerRpc(GetComponent<RectTransform>().anchoredPosition);
            opponent.GetComponent<RectTransform>().anchoredPosition = NV_hostTransform.Value;
            ball.GetComponent<RectTransform>().anchoredPosition = NV_ballTransform.Value;
        }
        if (IsHost) {
            // TestClientRpc(GetComponent<RectTransform>().anchoredPosition);
            // ballClientRpc(ball.GetComponent<RectTransform>().anchoredPosition);
            NV_hostTransform.Value = -1 * GetComponent<RectTransform>().anchoredPosition;
            NV_ballTransform.Value = -1 * ball.GetComponent<RectTransform>().anchoredPosition;
            opponent.GetComponent<RectTransform>().anchoredPosition = NV_clientTransform.Value;
        }
    }

    // [ServerRpc(RequireOwnership = false)]
    // public void TestServerRpc(Vector2 pos) {
    //     if (IsHost) {
    //         //only runs only on host
    //         opponentTransform = -1 * pos;
    //     }
    // }

    // [ClientRpc]
    // public void TestClientRpc(Vector2 pos) {
    //     if(IsClient && !IsHost) {
    //         // only runs only on client
    //         opponentTransform = -1 * pos;
    //     }
    // }

    // [ClientRpc]
    // public void ballClientRpc(Vector2 pos) {
    //     if (IsClient && !IsHost) {
    //         // only runs only on client
    //         ballTransform = -1 * pos;
    //     }
    // }

    [ServerRpc(RequireOwnership = false)]
    public void clientTransformUpdateServerRpc(Vector2 pos){
        NV_clientTransform.Value = -1 * pos;
    }
}