using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {

	public CharacterController2D controller;

	[SerializeField]
	private float runSpeed = 40f;
	private float _horizontalMove;
	public Animator animator;
	private void Update ()
	{
		if (!controller.IsLocalPlayer) return;
		_horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		
		animator.SetFloat("Speed", Math.Abs(_horizontalMove));
		
		if (Input.GetButtonDown("Jump"))
		{
			controller.IsJumping = true;
		}
	}

	private void FixedUpdate ()
	{
		if (!controller.IsLocalPlayer) return;
		var targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f,
			controller.currentRigidbody.velocity.y);
		controller.Move(targetVelocity);
		controller.IsJumping = false;
	}
}