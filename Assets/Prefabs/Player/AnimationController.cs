using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator animator;
    public PlayerFloorDetector floorDetector;

    public string walkParam = "isWalking";
    public string descendParam = "isDescending";

    void Update()
    {
        if (floorDetector == null) return;

        bool grounded = floorDetector.isGrounded;

        animator.SetBool(walkParam, grounded);
        animator.SetBool(descendParam, !grounded);
    }
}


