using UnityEngine;

public class FireAndAttack : MonoBehaviour
{
	// The player status component
	private PlayerStatus playerStatus;

	// For the sword attack
	[SerializeField] private Transform swordAttackRadius;
	[SerializeField] private float attackRadius = 0.5f;
	[SerializeField] private int attackDamage = 10;

	// The delay between sword attacks
	[SerializeField] private float attackDelay = 0.5f;
	[SerializeField] private float attackDelayTimer = 0f;


	// For the gun attack
	[SerializeField] private Transform gunFirePoint;
	[SerializeField] private GameObject bulletPrefab;

	// The delay between gun attacks
	[SerializeField] private float fireDelay = 0.3f;
	[SerializeField] private float fireDelayTimer = 0f;


	// The animator component
	private Animator animator;


	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		playerStatus = GetComponent<PlayerStatus>();
	}

	/*
	 * The Z key is used to attack with sword
	 * The X key is used to fire the gun
	*/

	// Update is called once per frame
	void Update()
	{
		// If the player's health is less than or equal to 0
		// then the player is dead
		if (playerStatus.currentHealth <= 0)
		{
			// Do not allow the player to attack
			return;
		}

		// If the attack delay timer is greater than 0
		// reduce the timer
		if (attackDelayTimer > 0)
		{
			attackDelayTimer -= Time.deltaTime;
		}

		// If the fire delay timer is greater than 0
		// reduce the timer
		if (fireDelayTimer > 0)
		{
			fireDelayTimer -= Time.deltaTime;
		}

		// Sword attack
		if (Input.GetKeyDown(KeyCode.Z) && attackDelayTimer <= 0)
		{
			// Show the attack animation
			animator.SetTrigger("attack");

			// Call the attack method
			Attack();

			// Set the attack delay timer to the attack delay
			attackDelayTimer = attackDelay;
		}

		// Gun attack
		if (Input.GetKeyDown(KeyCode.X) && fireDelayTimer <= 0)
		{
			// Show the fire animation
			animator.SetTrigger("fire");

			// Fire the gun
			Fire();

			// Set the fire delay timer to the fire delay
			fireDelayTimer = fireDelay;
		}
	}

	private void Attack()
	{
		var hitObjects = Physics2D.OverlapCircleAll(swordAttackRadius.position, attackRadius);

		foreach (var hitObject in hitObjects)
		{
			if (hitObject.CompareTag("Enemy"))
			{
				hitObject.GetComponent<Enemy>().TakeDamage(attackDamage);
			}
		}
	}

	private void Fire()
	{
		// Create the bullet
		var bullet = Instantiate(bulletPrefab, gunFirePoint.position, gunFirePoint.rotation);

		// Get the bullet component
		Bullet bulletComponent = bullet.GetComponent<Bullet>();

		// Set the bullet direction
		bulletComponent.SetDirection(transform.localScale.x);
	}

	// private void OnDrawGizmos()
	// {
	// 	Gizmos.color = Color.red;
	// 	Gizmos.DrawWireSphere(swordAttackRadius.position, attackRadius);
	// }
}
