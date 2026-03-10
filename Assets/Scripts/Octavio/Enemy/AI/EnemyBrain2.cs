using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyBrain2 : AIBrain
{
    public ForwardObstacleDetector detector;


    public float minDistance = 1.2f;

    public float randomInterval = 5f;
    public float randomProbability = 0.35f;

    

    float lastRandomChange;

    protected override void Think()
    {
        AvoidObstacle();
        RandomPhantom();
    }

    void AvoidObstacle()
    {
        DistanceInfo info = detector.nextObstacle;

        if (info.typeTarget == TypeTarget.Wall ||
            info.typeTarget == TypeTarget.Enemy||
            info.typeTarget == TypeTarget.ExclusiveEnemyWall)
        {
            if (info.distance <= minDistance)
            {
                ChangeDirection();
            }
        }
    }

    void RandomPhantom()
    {
        if (Time.time - lastRandomChange < randomInterval)
            return;

        lastRandomChange = Time.time;

        if (Random.value <= randomProbability)
        {
           StartCoroutine(ActivePhantomMode());
        }
    }


    private IEnumerator ActivePhantomMode()
    {
        float randomDuration= Random.Range(1f, 3f);

        controller.movement.Stop();
        controller.col.isTrigger = true;
        controller.movement.rb.useGravity = false;
        yield return new WaitForSeconds(randomDuration);

        controller.movement.rb.useGravity = true;
        controller.col.isTrigger = false;
        controller.movement.Resume();


    }

}