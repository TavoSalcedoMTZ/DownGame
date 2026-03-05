using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public PlayerDistanceDetector distanceDetector;
    public PlayerMovement movement;
    public float distancetoChangeDirection = 1f;

    void Update()
    {
        CheckAction();
    }

    void CheckAction()
    {
        if (distanceDetector.nextObstacle.typeTarget == TypeTarget.Wall &&
            distanceDetector.nextObstacle.distance <= distancetoChangeDirection)
        {
            movement.ChangeDirection();
        }
    }
}