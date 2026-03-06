using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BindingAction
{
    public int tapsRequired;
    public UnityEvent action;
}