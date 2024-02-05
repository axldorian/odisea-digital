using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
	// The director of the cutscene
	[SerializeField] private PlayableDirector director;
	private bool isTriggered = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{

			// If the cutscene has not been triggered yet
			if (!isTriggered)
			{
				// Set the cutscene as triggered
				isTriggered = true;

				// Start the cutscene
				director.Play();
			}
		}
	}
}
