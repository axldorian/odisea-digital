using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCommon2 : MonoBehaviour
{
	private Enemy enemyComponent;
	private Animator animator;
	[SerializeField] private int damageMade;
	private Rigidbody2D rb;

	// For movement
	[SerializeField] private float speed;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float distance;
	private bool rightMove;

	// For attack
	private PlayerStatus playerStatus;
	private Transform playerPosition;
	[SerializeField] private Transform attackControllerRadius;
	[SerializeField] private float attackRadius;
	[SerializeField] private float attackDelay;
	private float attackDelayTimer = 0f;


	private bool canMove = true;
	private bool isFollowingPlayer = false;

	// Start is called before the first frame update
	void Start()
	{
		enemyComponent = GetComponent<Enemy>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		// Find the player data
		playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
		playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}

	void FixedUpdate()
	{
		if (canMove)
		{
			RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
			rb.velocity = new Vector2(speed, rb.velocity.y);

			if (isFollowingPlayer && groundInfo != false)
			{
				// If the player is on the oposite side of movement of the enemy
				if (playerPosition.position.x < transform.position.x && rightMove)
				{
					Flip();
				}
				else if (playerPosition.position.x > transform.position.x && !rightMove)
				{
					Flip();
				}
			}
			else
			{
				// Move enemy normally
				if (groundInfo == false)
				{
					Flip();
				}
			}

		}
	}

	private void Flip()
	{
		rightMove = !rightMove;

		// Flip the enemy
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;

		// velocity
		speed *= -1;
	}

	// Update is called once per frame
	void Update()
	{
		// If the enemy's health is less than or equal to 0
		if (enemyComponent.health <= 0)
		{
			// Can't move
			canMove = false;
		}

		// Check if the enemy is close to the player
		Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackControllerRadius.position, attackRadius);

		var playerFound = false;
		foreach (Collider2D obj in hitObjects)
		{
			if (obj.CompareTag("Player"))
			{
				// Set the playerFound to true
				playerFound = true;

				// Attack if the enemy is close to the player
				if (Vector2.Distance(playerPosition.position, transform.position) < 2f)
				{
					// If the attack delay timer is greater than 0
					// reduce the timer
					if (attackDelayTimer > 0)
					{
						attackDelayTimer -= Time.deltaTime;
					}
					else
					{
						// Reset the attack delay timer
						attackDelayTimer = attackDelay;

						// Show the attack animation
						animator.SetTrigger("attack");

						// Make the player take damage
						playerStatus.TakeDamage(damageMade);
					}
				}
			}
		}

		// If the player is found
		if (playerFound)
		{
			// Set the isFollowingPlayer to true
			isFollowingPlayer = true;
		}
		else
		{
			// Set the isFollowingPlayer to false
			isFollowingPlayer = false;
		}
	}
}
