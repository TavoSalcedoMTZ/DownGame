using UnityEngine;

public abstract class PlayerComp : MonoBehaviour
{
    public PlayerController controller { get; private set; }

    protected virtual void Awake()
    {
        controller = GetComponent<PlayerController>();

        if (controller == null)
        {
            Debug.LogError($"{GetType().Name} requires PlayerController on the same GameObject");
        }
    }
}