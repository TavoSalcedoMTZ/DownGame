using UnityEngine;

public class EnemyFloorDetect : MonoBehaviour
{
    public float offsetX = 0.5f;
    public float range = 2f;

    public EnemyController controller;

    void FixedUpdate()
    {
        DetectEdge();
    }

    void DetectEdge()
    {
        Vector3 dir = controller.GetDirectionVector();

        Vector3 origin = transform.position + dir * offsetX;

        RaycastHit hit;

        bool hitSomething = Physics.Raycast(origin, Vector3.down, out hit, range);

      //  Debug.DrawRay(origin, Vector3.down * range, Color.red);

        if (!hitSomething)
        {
            controller.Flip();
            return;
        }

        Floor floor;

        if (!hit.collider.TryGetComponent(out floor))
        {
            controller.Flip();
        }
    }

}