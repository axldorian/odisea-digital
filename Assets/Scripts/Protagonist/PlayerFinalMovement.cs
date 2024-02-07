using UnityEngine;

public class PlayerFinalMovement : PlayerBase
{
	private float lastY = 0f;
	private PlayerStatus playerStatus;
	private bool showedDeathAnimation = false;

	private void Start()
	{
		// find the dialogue manager by DialogManager.cs
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

		// find the player status by PlayerStatus.cs
		playerStatus = GetComponent<PlayerStatus>();

		// Set the lastY to the current position of the player
		lastY = transform.position.y;

		// Set overlapRadius to 0.5f
		overlapRadius = 0.5f;
	}

	private void Update()
	{
		// If there is a cutscene playing, we don't want the player to move
		if (dialogueManager.isCutscenePlaying)
		{
			horizontal = 0;
			return;
		}

		// If the player's health is less than or equal to 0
		// then the player is dead
		if (playerStatus.currentHealth <= 0)
		{
			// If has not showed the death animation
			if (!showedDeathAnimation)
			{
				// Show the death animation
				animator.Play("PlayerFinal_die");

				// Set showedDeathAnimation to true
				showedDeathAnimation = true;

				// Disable the player's movement
				horizontal = 0;

				// Disable the player's jump
				jumpingPower = 0;
			}

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

		// If the player is grounded
		if (IsGrounded())
		{

			// If there is horizontal input, fire animation
			if (horizontal != 0)
			{
				animator.SetBool("run", true);
			}
			else
			{
				animator.SetBool("run", false);
			}

			// Set the jump and fall animations to false
			animator.SetBool("jump", false);
			animator.SetBool("fall", false);
		}

		// If is not grounded then is jumping or falling
		else
		{
			// If the player is jumping, fire animation
			if (gameObject.transform.position.y > lastY)
			{
				animator.SetBool("jump", true);
				animator.SetBool("fall", false);
			}

			// If the player is falling, fire animation
			if (gameObject.transform.position.y < lastY)
			{
				animator.SetBool("fall", true);
				animator.SetBool("jump", false);
			}
		}

		// Set the lastY to the current position of the player
		lastY = transform.position.y;
	}
}
