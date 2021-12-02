using System;
using Unity.Netcode;
using UnityEngine;

public class Dash : NetworkBehaviour
{
    public CharacterController2D controller;
    public Player player;
    [SerializeField]
    private int force;

    private float _maxStamina;

    private void Start()
    {
        _maxStamina = player.Stamina;
    }

    private const float Cost = 0.04f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (player.Stamina> 0.2)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        if (!IsLocalPlayer) return;
        bool facingRight = controller.FacingRight;
        transform.position += new Vector3(force * (-1 + Convert.ToInt32(facingRight) * +2) * Time.deltaTime, 0f, 0f);
        player.Stamina -= Cost;
    }
}
