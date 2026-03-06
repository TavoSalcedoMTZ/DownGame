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
            }
            else
            {
                nextObstacle.typeTarget = TypeTarget.None;
                nextObstacle.distance = Mathf.Infinity;
            }
        }
        else
        {
            nextObstacle.typeTarget = TypeTarget.None;
            nextObstacle.distance = Mathf.Infinity;
        }

        Debug.DrawRay(transform.position, dir * rayDistance, Color.red);
    }
}