using UnityEngine;
using UnityEngine.Playables;

public class SecondChest : MonoBehaviour
{
	[SerializeField] private PreventAdvance preventAdvance;
	[SerializeField] private PlayableDirector director;

	private bool isTriggered = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// If the player is colliding with this object and hasn't triggered the cutscene
		if (collision.CompareTag("Player") && !isTriggered)
		{
			// Set the cutscene as triggered
			isTriggered = true;

			// Add one to the number of guns
			preventAdvance.numberOfGuns++;

			// Play the cutscene
			director.Play();
		} 
	}
}
