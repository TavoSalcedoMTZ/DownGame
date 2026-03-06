using UnityEngine;

[System.Serializable]
public struct ActionRule
{
    public TypeTarget targetType;
    public float triggerDistance;
    public PlayerActionType action;
}