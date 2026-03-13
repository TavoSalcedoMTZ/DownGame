using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : PlayerComp
{
    public string TagEnemy = "Damageable";

    public UnityEvent OnAttack;

    private EnemyController enemyNear;

    public bool CanAttack;
    public bool IsAttacking;

    public void Attack()
    {
        if (controller.IsDead) return;
        if (enemyNear == null) return;
        if (!CanAttack) return;
        if (IsAttacking) return;
        if (enemyNear.IsDead) return;

        IsAttacking = true;
        CanAttack = false;

        SequenceController.Instance.PlayEnemyDefeatSequence(enemyNear, this);

        enemyNear = null;
    }
    public void SetEnemy(EnemyController enemy)
    {
        if (IsAttacking) return;

        enemyNear = enemy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagEnemy))
        {
            controller.PlayerDie();
        }
    }

    public void ResetAttack()
    {
        enemyNear = null;
        CanAttack = false;
        IsAttacking = false;
    }
}