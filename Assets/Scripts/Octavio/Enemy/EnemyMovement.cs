using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyController controller;
    public Rigidbody rb;

    public float speed = 3f;
    private bool CanMove = true;

    void FixedUpdate()
    {
        if (CanMove)
            Move();
    }

    void Move()
    {
        Vector3 dir = controller.GetDirectionVector();

        float scaledSpeed = speed * WorldSettings.movementScale;

        rb.linearVelocity = new Vector3(
            dir.x * scaledSpeed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    public void Stop()
    {
        CanMove = false;
        rb.linearVelocity = Vector3.zero;
    }

    public void Resume()
    {
        CanMove = true;
    }
}