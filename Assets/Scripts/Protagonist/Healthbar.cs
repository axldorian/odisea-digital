using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private Gradient gradient;
	[SerializeField] private Image fill;

	// Set the max health of the player
	public void SetMaxHealth(int health)
	{
		// Set the max value of the slider
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	// Set the health of the player
	public void SetHealth(int health)
	{
		// Set the value of the slider
		slider.value = health;

		// Change the color of the fill
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
