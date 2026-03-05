using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public FDirection direction;
    public float speed;




    public void Move()
    {
        rb.linearVelocity = new Vector3((direction == FDirection.Left ? -1 : 1) * speed, rb.linearVelocity.y, rb.linearVelocity.z);

    }
    public void Update()
    {
        Move();
    }

    public void ChangeDirection()
    {
        direction = direction == FDirection.Left ? FDirection.Right : FDirection.Left;
    }


}
