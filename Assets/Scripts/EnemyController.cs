using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public int maxHealth = 2;
	private int currentHealth;

	private Animator anim;                  // Reference to the animator component.

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		currentHealth = maxHealth;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damage)
	{
		// Reduce the enemy's health by the amount of damage taken.
		currentHealth -= damage;

		// Play hurt animation
		anim.SetTrigger("hurt");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		// Die animation
		anim.SetBool("isDead", true);

		// Destroy the enemy after 1 second
		Destroy(gameObject, 1f);
	}
}
