using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	private Transform target;            // The position that that camera will be following.
	public float moveSpeed;            // The speed that the enemy will move at.

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
	}
}
