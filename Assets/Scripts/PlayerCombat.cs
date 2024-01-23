using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	private Animator anim;                  // Reference to the player's animator component.
	public Transform attackPoint;           // The point from which the attack will be launched
	public float attackRange = 0.5f;        // The range of the attack
	public LayerMask enemyLayers;           // The layers that will be affected by the attack
	public float attackRate = 2f;           // The rate of attack
	private float nextAttackTime = 0f;      // The time of the next attack

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time >= nextAttackTime)
		{
			// Use the short range attack
			if (Input.GetKeyDown(KeyCode.Z))
			{
				Attack();
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
	}

	void Attack()
	{
		// Play attack animation
		anim.SetTrigger("attack");

		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		// Damage them
		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<EnemyController>().TakeDamage(1);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
