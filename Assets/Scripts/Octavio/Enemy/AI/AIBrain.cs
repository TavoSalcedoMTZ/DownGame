using UnityEngine;

public abstract class AIBrain : MonoBehaviour
{
    protected EnemyController controller;

    public float thinkInterval = 0.1f;

    float lastThink;

    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (Time.time - lastThink < thinkInterval)
            return;

        lastThink = Time.time;

        Think();
    }

    protected abstract void Think();

    protected void ChangeDirection()
    {
        if (controller.direction == FDirection.Left)
            controller.direction = FDirection.Right;
        else
            controller.direction = FDirection.Left;
    }
}