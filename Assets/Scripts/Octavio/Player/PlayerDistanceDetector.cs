using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDistanceDetector : MonoBehaviour
{
    public DistanceInfo nextObstacle;

    

    public void Update()
    {
 
        CheckDistance();

    }


    public void CheckDistance()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit))
        {
            if (hit.collider.gameObject.GetComponent<TargetObject>() != null)
            {
                nextObstacle.typeTarget = hit.collider.gameObject.GetComponent<TargetObject>().typeTarget;
                nextObstacle.distance = hit.distance;
            }
        }

    }

}
