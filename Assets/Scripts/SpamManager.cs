using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamManager : MonoBehaviour
{
	public GameObject[] enemies;            // Array of enemy prefabs.
	private Transform target;            // The position of the player.
	private readonly float startDelay = 1.5f;
	private readonly float repeatRate = 5f;
	private bool firstTime = true;

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		InvokeRepeating(nameof(SpamEnemies), startDelay, repeatRate);
	}

	void SpamEnemies()
	{
		var offset = 50f;
		if (firstTime)
		{
			offset = 10f;
			firstTime = false;
		}

		// Get a random enemy
		GameObject enemy = enemies[Random.Range(0, enemies.Length)];

		// Get a position that is 50 units away from the player in x axis
		Vector2 spawnPosition = new(target.position.x + offset, target.position.y);

		// Spawn the enemy
		Instantiate(enemy, spawnPosition, Quaternion.identity);
	}
}
