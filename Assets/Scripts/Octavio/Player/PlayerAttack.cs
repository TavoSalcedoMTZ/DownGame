using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerAttack : PlayerComp
{
    public string TagEnemy = "Damageable";

    public UnityEvent OnAttack;

    public EnemyController enemyNear;

    public bool CanAttack;
    public bool IsAttacking;

    public float attackCooldown = 0.35f;

    bool onCooldown;

    Coroutine attackRoutine;

    public void Attack()
    {
        if (controller.IsDead) return;
        if (onCooldown) return;
        if (enemyNear == null) return;
        if (!CanAttack) return;
        if (IsAttacking) return;
        if (enemyNear.IsDead) return;

        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AttackFlow());
    }

    IEnumerator AttackFlow()
    {
        IsAttacking = true;
        CanAttack = false;
        onCooldown = true;

        yield return new WaitForSeconds(0.05f);

        if (controller.IsDead || enemyNear == null || enemyNear.IsDead)
        {
            ResetAttack();
            yield return Cooldown();
            yield break;
        }

        SequenceController.Instance.PlayEnemyDefeatSequence(enemyNear, this);

        enemyNear = null;
        attackRoutine = null;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }

    public void SetEnemy(EnemyController enemy)
    {
        if (IsAttacking) return;
        enemyNear = enemy;
    }

    public void ResetAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        enemyNear = null;
        CanAttack = false;
        IsAttacking = false;

        StartCoroutine(Cooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagEnemy))
        {
            controller.PlayerDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagEnemy))
        {
            controller.PlayerDie();
        }
    }
}