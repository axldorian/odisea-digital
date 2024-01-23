using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
	private Animator anim;                  // Reference to the player's animator component.
	public Transform attackPoint;           // The point from which the attack will be launched
	public int attackDamage = 1;            // The damage of the attack
	public float attackRange = 0.5f;        // The range of the attack
	public float attackAreaRange = 0.5f;    // The range of the attack area
	public LayerMask playerLayer;           // The player layer that will be affected by the attack
	private float nextAttackTime = 0f;      // The time of the next attack

	private Transform target;            // The player position

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time >= nextAttackTime)
		{
			// Attack if the player is in range 
			if (Vector2.Distance(transform.position, target.position) < attackAreaRange)
			{
				Attack();

				// Set the time of the next attack
				nextAttackTime = Time.time + 2f;
			}
		}
	}

	void Attack()
	{
		// Play attack animation
		anim.SetTrigger("attack");

		// Detect player in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

		// Damage player
		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<PlayerController>().TakeDamage(attackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
