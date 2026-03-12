using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public EnemyMovement movement;
    public EnemyUI enemyUI;
    public Collider col;
    public EnemyFX enemyFX;

    public FDirection direction;

    public void TakeDamage()
    {
        movement.Stop();
        enemyFX.StartDieSequence();

        
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


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }
    }
}