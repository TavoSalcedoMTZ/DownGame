using UnityEngine;

public class PlayerDistanceDetector : PlayerComp
{
    public DistanceInfo nextObstacle;
    public float rayDistance = 10f;

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        Vector3 dir = controller.GetDirectionVector();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, rayDistance))
        {
            TargetObject target;

            if (hit.collider.TryGetComponent(out target))
            {
                nextObstacle.typeTarget = target.typeTarget;
                nextObstacle.distance = hit.distance;
                nextObstacle.GO_Ref = target.gameObject;
            }
            else
            {
                nextObstacle.typeTarget = TypeTarget.None;
                nextObstacle.distance = Mathf.Infinity;
                nextObstacle.GO_Ref = null;
            }
        }
        else
        {
            nextObstacle.typeTarget = TypeTarget.None;
            nextObstacle.distance = Mathf.Infinity;
            nextObstacle.GO_Ref = null;
        }

        Debug.DrawRay(transform.position, dir * rayDistance, Color.red);
    }

    public EnemyController DetectEnemy()
    {
        if (nextObstacle.typeTarget == TypeTarget.Enemy)
        {
            if (nextObstacle.GO_Ref != null &&
                nextObstacle.GO_Ref.TryGetComponent(out EnemyController enemy))
            {
                if (enemy.IsDead)
                    return null;

                return enemy;
            }
        }

        return null;
    }
}