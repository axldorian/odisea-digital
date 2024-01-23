using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int maxHealth = 500;            // The player's maximum health
	private int currentHealth;         // The player's current health
	public float moveSpeed;            // The speed that the player will move at.
	public float jumpForce;        // The force added to the player to jump.
	private Rigidbody2D rb2d;       // Store a reference to the Rigidbody2D component required to use 2D Physics.
	private Animator anim;                  // Reference to the player's animator component.

	// Start is called before the first frame update
	void Awake()
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(rb2d.velocity.y) < 0.001f)
		{
			anim.SetTrigger("jump");
			rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		float moveInput = Input.GetAxis("Horizontal");
		transform.position += moveSpeed * Time.deltaTime * new Vector3(moveInput, 0, 0);

		// Configura el parámetro de la animación de movimiento
		anim.SetFloat("walk", Mathf.Abs(moveInput));
	}

	public void TakeDamage(int damage)
	{
		// Reduce the player's health by the amount of damage taken.
		currentHealth -= damage;

		// Play hurt animation
		anim.SetTrigger("hurt");

		if (currentHealth <= 0)
		{
			// TODO: Implementar
			// Die();
		}
	}
}
