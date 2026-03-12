using UnityEngine;
using UnityEngine.Events;

public class Finalepico : MonoBehaviour
{
    public UnityEvent onTouch;

    void OnTriggerEnter(Collider other)
    {
        onTouch.Invoke();
    }
}
