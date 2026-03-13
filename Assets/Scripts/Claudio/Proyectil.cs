using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 10f;
    public float tiempoDeVida = 5f;

    Rigidbody rb;
    float vida;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = transform.right * velocidad * WorldSettings.movementScale;

        vida += Time.deltaTime * WorldSettings.movementScale;

        if (vida >= tiempoDeVida)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision choque)
    {
        Destroy(gameObject);
    }
}