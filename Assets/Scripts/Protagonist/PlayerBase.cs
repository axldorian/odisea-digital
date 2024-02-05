using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	protected float horizontal;
	protected readonly float speed = 8f;
	protected float jumpingPower = 12f;
	protected bool isFacingRight = true;
	protected float overlapRadius = 0.2f;

	protected DialogueManager dialogueManager;

	[SerializeField] protected Animator animator;
	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected Transform groundCheck;
	[SerializeField] protected LayerMask groundLayer;

	protected bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, overlapRadius, groundLayer);
	}

	protected void Flip()
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