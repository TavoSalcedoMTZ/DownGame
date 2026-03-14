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

    EnemyController currentEnemy;

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

        DistanceInfo info = distanceDetector.nextObstacle;

        EnemyController detectedEnemy = null;

        if (info.typeTarget == TypeTarget.Enemy && info.GO_Ref != null)
        {
            info.GO_Ref.TryGetComponent(out detectedEnemy);
        }

        if (detectedEnemy != null && !detectedEnemy.IsDead)
        {
            if (currentEnemy != detectedEnemy)
            {
                ClearEnemy();

                currentEnemy = detectedEnemy;

                attack.SetEnemy(currentEnemy);
                attack.CanAttack = true;

                currentEnemy.enemyUI.ShowParryIndicator();
            }

            return;
        }

        ClearEnemy();
    }

    void ClearEnemy()
    {
        if (currentEnemy != null)
        {
            currentEnemy.enemyUI.HideParryIndicator();
            attack.SetEnemy(null);
            currentEnemy = null;
        }
    }

    public void PlayerDie()
    {
        if (IsDead) return;

        IsDead = true;

        ClearEnemy();
        attack.ResetAttack();

        Debug.Log("Player died");

        GameManager.Instance.Lost();
        AudioManager.Play("die", transform.position);
    }

    public void MovePlayer(Vector3 positionToMove)
    {
        transform.position = positionToMove;
        IsDead = false;
        AudioManager.Play("respawn", transform.position);
    }
}