using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventGunAccess : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private PreventAdvance preventAdvance;

	private bool isCleared = false;


	void Update()
	{
		// If the player has the first gun
		if (preventAdvance.numberOfGuns >= 1 && !isCleared)
		{
			// Set the object as cleared
			isCleared = true;

			// Destroy the child of this object
			Destroy(transform.GetChild(0).gameObject);

			// Disable the collider and rigidbody of this object
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Rigidbody2D>().simulated = false;
		}
	}
}
