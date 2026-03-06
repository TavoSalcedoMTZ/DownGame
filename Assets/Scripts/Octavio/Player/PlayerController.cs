using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerDistanceDetector distanceDetector;
    public PlayerActionController actionController;
    public PlayerFloorDetector floorDetector;

    public FDirection direction;
    

    public Vector3 GetDirectionVector()
    {
        return direction == FDirection.Left ? Vector3.left : Vector3.right;
    }

    public void ChangeDirection()
    {
        direction = direction == FDirection.Left ? FDirection.Right : FDirection.Left;
    }
}