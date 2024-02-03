using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public Transform cameraTransform;

	// Update is called once per frame
	void Update()
	{
		// Make the dialogue UI follow the camera
		transform.LookAt(cameraTransform);
	}
}
