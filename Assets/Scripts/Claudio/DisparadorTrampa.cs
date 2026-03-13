using UnityEngine;

public class DisparadorTrampa : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject prefabProyectil;
    public Transform puntoDeDisparo;

    [Header("Ritmo de Fuego")]
    public float tiempoEntreDisparos = 2f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime * WorldSettings.movementScale;

        if (timer >= tiempoEntreDisparos)
        {
            Disparar();
            timer = 0f;
        }
    }

    void Disparar()
    {
        Instantiate(prefabProyectil, puntoDeDisparo.position, puntoDeDisparo.rotation);
    }
}