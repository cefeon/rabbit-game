using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : NetworkBehaviour
{
	[SerializeField] 
	private float jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] 
	private float movementSmoothing = .05f;	
	[SerializeField] 
	private bool airControl;
	[SerializeField] 
	private LayerMask whatIsGround;
	[SerializeField] 
	private Transform groundCheck;
	private const float GroundedRadius = .2f;
	public bool IsGrounded;
	public Rigidbody2D CurrentRigidbody;
	private Vector3 _velocity = Vector3.zero;
	[Header("Events")]
	[Space]
	
	[SerializeField]
	private UnityEvent onLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	[SerializeField]
	public bool FacingRight { get; set; } = true;
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
		onLandEvent ??= new UnityEvent();
	}

	private void FixedUpdate()
	{
		var wasGrounded = IsGrounded;
		IsGrounded = false;
		var colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
		foreach (var t in colliders)
		{
			if (t.gameObject != gameObject)
			{
				IsGrounded = true;
				if (!wasGrounded)
				{
					onLandEvent.Invoke();
				}
			}
		}
	}

	public void Move(Vector2 targetVelocity)
	{
		if (!IsGrounded && !airControl) return;
		var horizontalVelocity = targetVelocity.x;
		var velocity = CurrentRigidbody.velocity;
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