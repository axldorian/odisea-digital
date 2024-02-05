using UnityEngine;

public class PreventAdvance : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[HideInInspector] public int numberOfGuns = 0;
	private bool isCleared = false;

	private void Update()
	{
		// If the player has the two guns
		if (numberOfGuns >= 2 && !isCleared)
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
