using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float velocidad = 10f;
    public float tiempoDeVida = 5f; // Por si no choca con nada, que se destruya eventualmente

    void Start()
    {
        // Le damos impulso hacia adelante en el momento que nace
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.right * velocidad;

        // Destruye el proyectil despu�s de unos segundos como medida de seguridad
        Destroy(gameObject, tiempoDeVida);
    }

    // Esta funci�n se activa cuando el Collider choca con otro Collider
    void OnCollisionEnter(Collision choque)
    {
        // Opcional: Aqu� podr�as comprobar con qu� choc�. 
        // Ejemplo: if (choque.gameObject.CompareTag("Player")) { // hacer da�o }

        // Destruimos el proyectil al impactar
        Destroy(gameObject);
    }
}
