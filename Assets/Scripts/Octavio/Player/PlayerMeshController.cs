using UnityEngine;

public class PlayerMeshController : PlayerComp
{
    public Transform mesh;

    public void UpdateDirection(FDirection direction)
    {
        if (direction == FDirection.Right)
        {
            mesh.localRotation = Quaternion.identity;
        }
        else
        {
            mesh.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}