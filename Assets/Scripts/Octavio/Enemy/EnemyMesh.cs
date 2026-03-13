using UnityEngine;

public class EnemyMesh : MonoBehaviour
{
    public Transform mesh;

    public void UpdateDirection(FDirection direction)
    {
        if (mesh == null) return;

        if (direction == FDirection.Right)
        {
            mesh.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            mesh.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }

        
    }


}