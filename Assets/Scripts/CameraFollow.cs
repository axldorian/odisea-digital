using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public Vector3 offset;                     // The offset at which the Health Bar follows the player.
	public float damping;            // The speed with which the camera will be following.

	private Vector3 velocity = Vector3.zero;    // The velocity at which the camera will be following.

	// Update is called once per frame
	void FixedUpdate()
	{
		Vector3 movePosition = target.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
	}
}
