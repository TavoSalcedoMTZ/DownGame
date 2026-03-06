using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyUI enemyUI;
    public void TakeDamage()
    {
        Destroy(gameObject);
    }

  
}
