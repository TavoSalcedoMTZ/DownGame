using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyMovement movement;
    public EnemyUI enemyUI;
    public Collider col;

    public FDirection direction;

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    public Vector3 GetDirectionVector()
    {
        switch (direction)
        {
            case FDirection.Left:
                return -transform.right;

            case FDirection.Right:
                return transform.right;

            default:
                return Vector3.zero;
        }
    }
}