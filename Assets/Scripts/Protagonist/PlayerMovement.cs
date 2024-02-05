using UnityEngine;

public class PlayerMovement : PlayerBase
{
	private void Start()
	{
		// find the dialogue manager by DialogManager.cs
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}

	private void Update()
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
}