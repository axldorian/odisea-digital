using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 30f;
	[SerializeField] private int bulletDamage = 5;

	private void Update()
	{
		// Move the bullet to the right
		transform.Translate(bulletSpeed * Time.deltaTime * Vector2.right);

		// Destroy the bullet after 3 seconds
		Destroy(gameObject, 3f);
	}


	/// <summary>
	/// Set the direction of the bullet based on player's
	/// transform.localScale.x
	/// </summary>
	/// <param name="x">
	/// The player's transform.localScale.x
	/// </param>
	public void SetDirection(float x)
	{
		// If the player is facing left
		if (x < 0)
		{
			// Reverse the bullet's direction
			bulletSpeed *= -1;

			// Flip the bullet
			Vector3 localScale = transform.localScale;
			localScale.x *= -1;
			transform.localScale = localScale;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// If the bullet collides with an enemy
		if (other.CompareTag("Enemy"))
		{
			// Make the enemy take damage
			other.GetComponent<Enemy>().TakeDamage(bulletDamage);

			// Destroy the bullet
			Destroy(gameObject);
		}
	}
}
