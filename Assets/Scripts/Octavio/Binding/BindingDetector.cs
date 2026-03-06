using UnityEngine;

public class BindingDetector : MonoBehaviour
{
    public BindingAction[] tapActions;
    public BindingAction holdAction;

    public float holdTime = 0.4f;
    public float comboResetTime = 0.35f;

    float pressTime;
    float lastTapTime;

    int tapCount;

    bool pressed;
    bool holding;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;
            holding = false;
            pressTime = Time.time;
        }

        if (pressed)
        {
            if (!holding && Time.time - pressTime >= holdTime)
            {
                holding = true;
                tapCount = 0;
                holdAction?.action?.Invoke();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;

            if (holding)
                return;

            if (Time.time - lastTapTime > comboResetTime)
                tapCount = 0;

            tapCount++;
            lastTapTime = Time.time;

            TriggerTapAction(tapCount);
        }
    }

    void TriggerTapAction(int count)
    {
        BindingAction best = null;
        int bestTap = -1;

        foreach (var action in tapActions)
        {
            if (action.tapsRequired <= count && action.tapsRequired > bestTap)
            {
                bestTap = action.tapsRequired;
                best = action;
            }
        }

        if (best != null)
            best.action?.Invoke();
    }
}