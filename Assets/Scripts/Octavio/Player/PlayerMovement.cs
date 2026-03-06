using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerComp
{
    public Rigidbody rb;
    public Collider playerCollider;

    public float speed = 5f;

    public bool canMove = true;
    public bool canDown = true;

    void Update()
    {
        if (canMove)
            Move();
    }

    public void Move()
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
        rb.linearVelocity = Vector3.zero;
        canMove = false;
    }

    public void Resume()
    {
        canMove = true;
    }

    public void Down()
    {
        if (canDown)
        {
            StartCoroutine(DownRoutine());
        }
    }

    IEnumerator DownRoutine()
    {
        Stop();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            controller.floorDetector.IgnoreFloor(hit.collider);
        }

        canDown = false;
        playerCollider.isTrigger = true;

        yield return new WaitUntil(() => controller.floorDetector.isGrounded == false);
        yield return new WaitUntil(() => controller.floorDetector.isGrounded == true);

        playerCollider.isTrigger = false;

        controller.floorDetector.ResetIgnoredFloor();

        canDown = true;

        Resume();
    }
}