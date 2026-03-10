using UnityEngine;

public class PlayerFloorDetector : PlayerComp
{
    public bool CheckingFloor;
    public bool isGrounded;
    public float rayDistance = 1f;

    Collider ignoredFloor;

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        RaycastHit hit;

        bool hitSomething = Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance);

        if (!hitSomething)
        {
            isGrounded = false;
            return;
        }

        if (hit.collider == ignoredFloor)
        {
            isGrounded = false;
            return;
        }

        isGrounded = true;

        if (!CheckingFloor)
            CheckFloor(hit);

        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.green);
    }

    void CheckFloor(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Floor target))
        {
            if (target.type == FloorType.Pass)
            {
                controller.movement.canDown = true;
            }
            else if (target.type == FloorType.Block)
            {
                controller.movement.canDown = false;
            }
        }
    }

    public void IgnoreFloor(Collider floor)
    {
        ignoredFloor = floor;
    }

    public void ResetIgnoredFloor()
    {
        ignoredFloor = null;
    }
}