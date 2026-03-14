using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Puedes llamar a esta función desde un botón o un trigger
    public void CargarEscena(string nombreDeLaEscena)
    {
        SceneManager.LoadScene(nombreDeLaEscena);
    }


    // Función pública para que pueda ser llamada desde un botón
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        // Cierra la aplicación compilada
        Application.Quit();

        // Detiene el modo de reproducción si estás dentro del editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

