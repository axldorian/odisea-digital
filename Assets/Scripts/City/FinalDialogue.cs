using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinalDialogue : MonoBehaviour
{
	[SerializeField] private PlayableDirector _director;
	public GameObject AudioBackground;
	private bool _firstTime = true;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		// Si esta en la posicion 30 en x y es la primera vez que se ejecuta, reproducir la cinematica
		if (transform.position.x > 30 && _firstTime)
		{
			_firstTime = false;
			_director.Play();
			AudioBackground.GetComponent<PlayBGAudio>().StopAudio();
			StartCoroutine(NextScene());
		}

	}

	IEnumerator NextScene()
	{
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
