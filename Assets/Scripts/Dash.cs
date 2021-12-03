using System;
using Unity.Netcode;
using UnityEngine;

public class Dash : NetworkBehaviour
{
    public CharacterController2D controller;
    public Player player;
    private bool _isDashing;
    [SerializeField]
    private int force;

    private const float Cost = 0.04f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.Stamina.value > 0.2) 
        {
            _isDashing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) || player.Stamina.value < 0.2)
        {
            _isDashing = false;
        }

        Move();
    }

    private void Move()
    {
        if (!IsLocalPlayer) return;
        if (!_isDashing) return;
        var facingRight = controller.FacingRight;
        transform.position += new Vector3(force * (-1 + Convert.ToInt32(facingRight) * +2) * Time.deltaTime, 0f, 0f);
        player.Stamina.value -= Cost;
    }
}
