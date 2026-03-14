using UnityEngine;

public class PlayerActionController : PlayerComp
{
    public ActionRule[] rules;

    void Start()
    {
        controller.distanceDetector.OnObstacleUpdated += EvaluateRules;
    }

    void EvaluateRules(DistanceInfo info)
    {
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

            case PlayerActionType.DetectEnemy:
                controller.EnemyNear();
                break;
        }
    }
}