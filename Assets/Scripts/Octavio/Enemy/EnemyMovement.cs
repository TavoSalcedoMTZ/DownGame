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

        rb.linearVelocity = new Vector3(
            dir.x * speed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    public void Stop()
    { 
        CanMove = false;
    }

    public void Resume()
    {

        CanMove = true;
    }
}