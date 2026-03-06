
using UnityEngine;
using UnityEngine.Events;
public class PlayerAttack : PlayerComp
{
    public UnityEvent OnAttack;
    private EnemyController enemyNear;
    public void Attack()
    {
        if (enemyNear != null)
        {
            enemyNear.TakeDamage();
            OnAttack?.Invoke();

        }
        
    }

    public void SetEnemy(EnemyController enemyNear) => this.enemyNear = enemyNear;

}
