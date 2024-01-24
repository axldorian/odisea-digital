using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestOffLimits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        		// Verify if the player is out of bounds
		if (transform.position.x < -5f)
		{
			transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
		}

		if (transform.position.x > 65f)
		{
			transform.position = new Vector3(65f, transform.position.y, transform.position.z);
		}
    }
}
