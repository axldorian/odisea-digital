using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void BotonJugar()
    {
        // Al momento de empaquetar el juego pasaria a la otra escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BotonSalir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
