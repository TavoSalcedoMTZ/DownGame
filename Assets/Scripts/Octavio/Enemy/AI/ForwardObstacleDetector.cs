using UnityEngine;

public class ForwardObstacleDetector : MonoBehaviour
{
    public DistanceInfo nextObstacle;

    public EnemyController controller;

    public float rayDistance = 5f;

    void Update()
    {
        Detect();
    }

    void Detect()
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
                return;
            }
        }

        nextObstacle.typeTarget = TypeTarget.None;
        nextObstacle.distance = Mathf.Infinity;
        nextObstacle.GO_Ref = null;
    }
}