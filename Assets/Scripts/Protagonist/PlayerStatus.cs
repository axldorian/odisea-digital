using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	// Animation
	private Animator animator;

	// The player's health
	[SerializeField] private int health = 100;
	[HideInInspector] public int currentHealth;

	// For the health bar
	[SerializeField] private Healthbar healthbar;


	void Start()
	{
		// Get the animator component
		animator = GetComponent<Animator>();

		// Set the player's health
		currentHealth = health;
		healthbar.SetMaxHealth(health);

		// Start the coroutine to regenerate the player's health
		StartCoroutine(RegenerateHealth());
	}

	/// <summary>
	/// Reduce the player's health
	/// </summary>
	/// <param name="damage">
	/// The amount of damage to be taken
	/// </param>
	public void TakeDamage(int damage)
	{
		// Reduce the player's health
		currentHealth -= damage;

		// If the player's health is more than 0
		if (currentHealth > 0)
		{
			// Play the damage animation
			animator.SetTrigger("damage");
		}

		// Update the health bar
		healthbar.SetHealth(currentHealth);
	}

	/// <summary>
	/// Increase the player's health
	/// </summary>
	/// <param name="health">
	/// The amount of health to be added
	/// </param>
	public void AddHealth(int health)
	{
		// If the player's health is already full
		if (currentHealth == this.health)
		{
			return;
		}

		// If the player's health plus the health to be added 
		// is greater than the max health
		if (currentHealth + health > this.health)
		{
			// Set the player's health to the max health
			currentHealth = this.health;
		}
		else
		{
			// Increase the player's health
			currentHealth += health;
		}

		// Update the health bar
		healthbar.SetHealth(currentHealth);
	}

	/// <summary>
	/// Increase the player's health every 15 seconds
	/// </summary>
	/// <returns>
	/// A coroutine to wait for 15 seconds before increasing the player's health
	/// </returns>
	private IEnumerator RegenerateHealth()
	{
		// Wait for 15 seconds
		yield return new WaitForSeconds(15);

		// If the player is dead, stop the coroutine
		if (currentHealth <= 0)
		{
			yield break;
		}

		// Increase the player's health by 10
		AddHealth(10);

		// Start the coroutine again
		StartCoroutine(RegenerateHealth());
	}
}
