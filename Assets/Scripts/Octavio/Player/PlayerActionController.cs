using UnityEngine;

public class PlayerActionController : PlayerComp
{
    public ActionRule[] rules;

    void Update()
    {
        EvaluateRules();
    }

    void EvaluateRules()
    {
        DistanceInfo info = controller.distanceDetector.nextObstacle;

        foreach (var rule in rules)
        {
            if (info.typeTarget == rule.targetType &&
                info.distance <= rule.triggerDistance)
            {
                ExecuteAction(rule.action);
                return;
            }
        }
    }

    void ExecuteAction(PlayerActionType action)
    {
        switch (action)
        {
            case PlayerActionType.ChangeDirection:
                controller.ChangeDirection();
                break;

            case PlayerActionType.Stop:
                controller.movement.Stop();
                break;
        }
    }
}