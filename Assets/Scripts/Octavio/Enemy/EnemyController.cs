using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyMovement movement;
    public EnemyUI enemyUI;
    public Collider col;
    public EnemyFX enemyFX;
    public EnemyMesh enemyMesh;

    public FDirection direction;

    public bool IsDead;

    public void TakeDamage()
    {
        if (IsDead) return;

        IsDead = true;

        movement.Stop();

        if (col != null)
            col.enabled = false;

        enemyFX.StartDieSequence();
    }

    public void Update()
    {
        enemyMesh.UpdateDirection(direction);
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

    public void Flip()
    {
        if (direction == FDirection.Left)
            direction = FDirection.Right;
        else
            direction = FDirection.Left;
    }

}