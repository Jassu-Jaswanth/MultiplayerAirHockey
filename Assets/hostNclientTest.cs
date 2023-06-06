using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class hostNclientTest : MonoBehaviour{
    [SerializeField] TMPro.TMP_InputField ip;
    public void createHost(){
        NetworkManager.Singleton.StartHost();
    }

    public void createClient(){
        NetworkManager.Singleton.StartClient();
    }
}
