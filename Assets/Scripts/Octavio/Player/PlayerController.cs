using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerDistanceDetector distanceDetector;
    public PlayerActionController actionController;
    public PlayerFloorDetector floorDetector;
    public PlayerAttack attack;
    public PlayerMeshController meshController;

    public FDirection direction;
    public bool IsDead;

    public Vector3 GetDirectionVector()
    {
        return direction == FDirection.Left ? Vector3.left : Vector3.right;
    }

    public void ChangeDirection()
    {
        direction = direction == FDirection.Left ? FDirection.Right : FDirection.Left;

        meshController.UpdateDirection(direction);
    }

    public void EnemyNear()
    {
        if (IsDead) return;

        if (attack.IsAttacking)
            return;

        EnemyController enemy = distanceDetector.DetectEnemy();

        if (enemy != null)
        {
            attack.SetEnemy(enemy);
            enemy.enemyUI.ShowParryIndicator();
            attack.CanAttack = true;
        }
        else
        {
            attack.SetEnemy(null);
        }
    }

    public void PlayerDie()
    {
        if (IsDead) return;

        IsDead = true;

        attack.ResetAttack();

        Debug.Log("Player died");

        GameManager.Instance.Lost();
    }

    public void MovePlayer( Vector3 positionToMove)
    {
        gameObject.transform.position = positionToMove;
        IsDead = false;
    }
}