using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : NetworkBehaviour
{
	[SerializeField] 
	private float jumpForce = 400f;							
	[Range(0, 1)] [SerializeField] 
	private float crouchSpeed = .36f;		
	[Range(0, .3f)] [SerializeField] 
	private float movementSmoothing = .05f;	
	[SerializeField] 
	private bool airControl;
	[SerializeField] 
	private LayerMask whatIsGround;
	[SerializeField] 
	private Transform groundCheck;
	[SerializeField] 
	private Transform ceilingCheck;
	[SerializeField] 
	private Collider2D crouchDisableCollider;
	private const float GroundedRadius = .2f;
	public bool IsGrounded;
	private const float CeilingRadius = .2f;
	public Rigidbody2D CurrentRigidbody;
	private Vector3 _velocity = Vector3.zero;
	[Header("Events")]
	[Space]
	
	[SerializeField]
	private UnityEvent onLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	[SerializeField]
	private BoolEvent onCrouchEvent;
	private bool _wasCrouching = false;
	public bool FacingRight { get; set; } = true;
	public bool IsCrouching { get; set; }
	public bool IsJumping { get; set; }

	private void Update()
	{
		var playerObjects = GameObject.FindGameObjectsWithTag("Player");
		foreach (var player in playerObjects)
		{
			if (player.GetComponent<NetworkObject>().IsLocalPlayer)
			{
				CurrentRigidbody = GetComponent<Rigidbody2D>();
			}
		}
	}

	private void Awake()
	{
		if (onLandEvent == null)
		{
			onLandEvent = new UnityEvent();
		}

		if (onCrouchEvent == null)
		{
			onCrouchEvent = new BoolEvent();
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = IsGrounded;
		IsGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				IsGrounded = true;
				if (!wasGrounded)
				{
					onLandEvent.Invoke();
				}
			}
		}
	}

	private float CrouchingMove()
	{
		if (IsCrouching)
		{
			if (!_wasCrouching)
			{
				_wasCrouching = true;
				onCrouchEvent.Invoke(true);
			}

			if (crouchDisableCollider != null)
			{
				crouchDisableCollider.enabled = false;
			}
			return crouchSpeed;
		} else
		{
			if (crouchDisableCollider != null)
			{
				crouchDisableCollider.enabled = true;
			}

			if (_wasCrouching)
			{
				_wasCrouching = false;
				onCrouchEvent.Invoke(false);
			}
			return 1;
		}

		
	}
	
	public void Move(Vector2 targetVelocity)
	{
		if (!IsGrounded && !airControl) return;
		var horizontalVelocity = targetVelocity.x;
		var velocity = CurrentRigidbody.velocity;
		horizontalVelocity *= CrouchingMove();
		CurrentRigidbody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);

		if (horizontalVelocity > 0 && !FacingRight)
		{
			Flip();
		}
		else if (horizontalVelocity < 0 && FacingRight)
		{
			Flip();
		}
		if (IsGrounded && IsJumping)
		{
			Jump();
		}
	}

	private void Jump()
	{
		IsGrounded = false;
		CurrentRigidbody.AddForce(Vector2.up * jumpForce);
	}

	private void Flip()
	{
		FacingRight = !FacingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}