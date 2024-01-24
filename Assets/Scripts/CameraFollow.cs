using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public bool shouldFollow = true;                    // Should the camera follow the player?
	public Transform target;                            // The position that that camera will be following.
	private Vector3 offset = new(2f, 2.5f, -10f);       // The offset at which the camera follows the player.
	private readonly float smoothTime = 0.25f;          // The speed with which the camera will be following.

	private Vector3 velocity = Vector3.zero;            // The velocity at which the camera will be following.

	// Update is called once per frame
	void FixedUpdate()
	{
		// if the camera should follow the player
		if (shouldFollow)
		{
			// Create a position the camera is aiming for based on the offset from the target.
			Vector3 movePosition = target.position + offset;

			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smoothTime);
		}
	}
}
