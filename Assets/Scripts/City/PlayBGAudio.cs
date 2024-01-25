using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGAudio : MonoBehaviour
{
	private AudioSource aud;

	// Start is called before the first frame update
	void Start()
	{
		aud = GetComponent<AudioSource>();

		// esperar 3.82 segundos para empezar a escuchar
		Invoke(nameof(PlayAudio), 2f);
	}

	void PlayAudio()
	{
		aud.Play();
	}

	public void StopAudio()
	{
		aud.Stop();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
