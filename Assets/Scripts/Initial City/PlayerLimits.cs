using UnityEngine;

public class PlayerLimits : MonoBehaviour
{
	// The player
	[SerializeField] private GameObject player;

	void Update()
	{
		// If the player is out of the left limit
		if (player.transform.position.x < 4)
		{
			// Reset the player position
			var reseted = player.transform.position;
			reseted.x = 4;

			player.transform.position = reseted;
		}
	}
}
