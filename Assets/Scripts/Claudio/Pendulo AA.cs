using UnityEngine;

public class PenduloAA : MonoBehaviour
{
    [Header("Configuración del Péndulo")]
    public float velocidad = 2.0f; // Qué tan rápido se balancea
    public float anguloMaximo = 60.0f; // El límite de grados hacia la izquierda/derecha

    void Update()
    {
        // Calculamos el ángulo usando la función Seno para un movimiento suave
        float angulo = anguloMaximo * Mathf.Sin(Time.time * velocidad);

        // Aplicamos la rotación (asumiendo que se balancea en el eje Z)
        transform.localRotation = Quaternion.Euler(angulo, 0, 0);
    }
}
