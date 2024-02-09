using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStartMenu : MonoBehaviour
{
	public void GoToStart()
	{
		SceneManager.LoadScene(0);
	}
}
