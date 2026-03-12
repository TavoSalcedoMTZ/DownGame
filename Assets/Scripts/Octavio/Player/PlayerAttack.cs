using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : PlayerComp
{
    public UnityEvent OnAttack;

    private EnemyController enemyNear;

    public bool CanAttack;
    public bool IsAttacking;

    public void Attack()
    {
        if (enemyNear == null) return;
        if (!CanAttack) return;
        if (IsAttacking) return;

        IsAttacking = true;
        CanAttack = false;

        SequenceController.Instance.PlayEnemyDefeatSequence(enemyNear, this);

        enemyNear = null;
        enemyNear.gameObject.GetComponent<Rigidbody>().useGravity = false;
        enemyNear.gameObject.GetComponent<Collider>().enabled = false;
    }

    public void SetEnemy(EnemyController enemyNear)
    {
        if (IsAttacking) return;

        this.enemyNear = enemyNear;
    }
}