using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float horizontal;
	private readonly float speed = 8f;
	private float jumpingPower = 10f;
	private bool isFacingRight = true;

	private DialogueManager dialogueManager;

	[SerializeField] private Animator animator;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

	private void Start()
	{
		// find the dialogue manager by DialogManager.cs
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}

	void Update()
	{
		// If there is a cutscene playing, we don't want the player to move
		if (dialogueManager.isCutscenePlaying)
		{
			horizontal = 0;
			return;
		}

		// Get horizontal input
		horizontal = Input.GetAxisRaw("Horizontal");

		// If there is vertical input, fire animation
		// only if the player is grounded
		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
		}

		// If the player is jumping and releases the jump button
		// reduce the jump height
		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}

		Flip();
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

		// If there is horizontal input, fire animation
		if (horizontal != 0)
		{
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
		}
	}

	private bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}

	private void Flip()
	{
		if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
		{
			isFacingRight = !isFacingRight;
			Vector3 localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}
	}
}