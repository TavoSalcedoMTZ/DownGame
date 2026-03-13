using UnityEngine;

public class PenduloDI : MonoBehaviour
{
    [Header("Configuración del Péndulo")]
    public float velocidad = 2.0f;
    public float anguloMaximo = 60.0f;

    float tiempo;

    void Update()
    {
        tiempo += Time.deltaTime * WorldSettings.movementScale;

        float angulo = anguloMaximo * Mathf.Sin(tiempo * velocidad);

        transform.localRotation = Quaternion.Euler(0f, 0f, angulo);
    }
}