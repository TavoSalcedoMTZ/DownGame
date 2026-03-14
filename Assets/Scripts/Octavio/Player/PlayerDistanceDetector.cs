using UnityEngine;
using System;

public class PlayerDistanceDetector : PlayerComp
{
    public DistanceInfo nextObstacle;
    public float rayDistance = 10f;

    public Action<DistanceInfo> OnObstacleUpdated;

    GameObject lastObject;
    TypeTarget lastType;
    float lastDistance;

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        Vector3 dir = controller.GetDirectionVector();

        RaycastHit hit;

        TypeTarget type = TypeTarget.None;
        GameObject obj = null;
        float dist = Mathf.Infinity;

        if (Physics.Raycast(transform.position, dir, out hit, rayDistance))
        {
            TargetObject target;

            if (hit.collider.TryGetComponent(out target))
            {
                type = target.typeTarget;
                obj = target.gameObject;
                dist = hit.distance;
            }
        }

        nextObstacle.typeTarget = type;
        nextObstacle.GO_Ref = obj;
        nextObstacle.distance = dist;

        bool changed =
            obj != lastObject ||
            type != lastType ||
            Mathf.Abs(dist - lastDistance) > 0.05f;

        if (changed)
        {
            lastObject = obj;
            lastType = type;
            lastDistance = dist;

            OnObstacleUpdated?.Invoke(nextObstacle);
        }

        Debug.DrawRay(transform.position, dir * rayDistance, Color.red);
    }
}