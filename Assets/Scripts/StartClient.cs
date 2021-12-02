using Unity.Netcode;
using UnityEngine;

public class StartClient : MonoBehaviour
{
    void Start()
    {
        var networkManager = GetComponent<NetworkManager>();
        networkManager.StartHost();
    }
}
