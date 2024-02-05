using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{

	private float length, startPostion;
	public GameObject cam;
	public float parallaxEffect;

	// Start is called before the first frame update
	void Start()
	{
		startPostion = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	// Update is called once per frame
	void Update()
	{
		float temp = cam.transform.position.x * (1 - parallaxEffect);
		float dist = cam.transform.position.x * parallaxEffect;
		transform.position = new Vector3(startPostion + dist, transform.position.y, transform.position.z);

		if (temp > startPostion + length) startPostion += length;
		else if (temp < startPostion - length) startPostion -= length;
	}
}
