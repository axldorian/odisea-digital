using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health;
	[SerializeField] private float timeToDestroy = 1f;
	[SerializeField] private Animator animator;
	[SerializeField] private string deathAnimationName;


	public void TakeDamage(int damage)
	{
		// Show the damage animation
		animator.SetTrigger("damage");

		// Reduce the enemy's health
		health -= damage;

		// If the enemy's health is less than or equal to 0
		if (health <= 0)
		{
			// Show the death animation
			animator.Play(deathAnimationName);

			// Destroy the enemy after a certain amount of time
			Destroy(gameObject, timeToDestroy);
		}
	}
}
