using UnityEngine;

public class DisparadorTrampa : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject prefabProyectil; // Arrastra aquí tu prefab del proyectil
    public Transform puntoDeDisparo;   // Arrastra aquí el GameObject vacío que creaste

    [Header("Ritmo de Fuego")]
    public float tiempoEntreDisparos = 2f; // Dispara cada 2 segundos
    private float tiempoSiguienteDisparo;

    void Update()
    {
        // Verificamos si ya es tiempo de disparar de nuevo
        if (Time.time >= tiempoSiguienteDisparo)
        {
            Disparar();
            // Calculamos cuándo será el próximo disparo
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        // Instanciamos (creamos) el proyectil en la posición y rotación del PuntoDeDisparo
        Instantiate(prefabProyectil, puntoDeDisparo.position, puntoDeDisparo.rotation);
    }
}