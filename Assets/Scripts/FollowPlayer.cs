using System;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class FollowPlayer : NetworkBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject Player;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        
    }

    private void Update()
    {
        if (Player != null) return;
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in playerObjects)
        {
            if (player.GetComponent<NetworkObject>().IsLocalPlayer)
            {
                Player = player;
                _virtualCamera.Follow = Player.transform;
            }
        }
    }
}
