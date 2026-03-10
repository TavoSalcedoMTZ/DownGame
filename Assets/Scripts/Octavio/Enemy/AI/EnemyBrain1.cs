using UnityEngine;

public class EnemyBrain1 : AIBrain
{
    public ForwardObstacleDetector detector;

    public float minDistance = 1.2f;

    public float randomInterval = 5f;
    public float randomProbability = 0.35f;

    float lastRandomChange;

    protected override void Think()
    {
        AvoidObstacle();
        RandomDirection();
    }

    void AvoidObstacle()
    {
        DistanceInfo info = detector.nextObstacle;

        if (info.typeTarget == TypeTarget.Wall ||
            info.typeTarget == TypeTarget.Enemy || 
            info.typeTarget==TypeTarget.ExclusiveEnemyWall)
        {
            if (info.distance <= minDistance)
            {
                ChangeDirection();
            }
        }
    }

    void RandomDirection()
    {
        if (Time.time - lastRandomChange < randomInterval)
            return;

        lastRandomChange = Time.time;

        if (Random.value <= randomProbability)
        {
            ChangeDirection();
        }
    }
}