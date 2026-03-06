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

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            if (hit.collider == ignoredFloor)
                return;

            isGrounded = true;
            Checkfloor(hit);
        }
        else
        {
            isGrounded = false;
        }

        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.green);
    }

    void Checkfloor(RaycastHit hit)
    {
        if (CheckingFloor) return;

        if (hit.collider.TryGetComponent(out Floor target))
        {
            switch (target.type)
            {
                case FloorType.Pass:
                    controller.movement.canDown = true;
                    break;

                case FloorType.Block:
                    controller.movement.canDown = false;
                    break;
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