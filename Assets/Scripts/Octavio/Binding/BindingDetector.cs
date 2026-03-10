using UnityEngine;
using UnityEngine.Events;

public class BindingDetector : MonoBehaviour
{
    public BindingAction[] tapActions;
    public BindingAction holdAction;

    public float holdTime = 0.4f;
    public float comboResetTime = 0.35f;

    public float timeTo30 = 30f;
    public float timeTo60 = 60f;

    float lastInputTime;

    float pressTime;
    float lastTapTime;

    int tapCount;

    bool pressed;
    bool holding;

    bool fired30;
    bool fired60;

    [Header("Events")]
    public UnityEvent On30s_Inactive;
    public UnityEvent On60s_Inactive;

    void Start()
    {
        lastInputTime = Time.time;
    }

    void Update()
    {
        CheckLastInput();

        if (Input.GetMouseButtonDown(0))
        {
            RegisterInput();

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
            RegisterInput();

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

    void RegisterInput()
    {
        lastInputTime = Time.time;

        fired30 = false;
        fired60 = false;
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

    void CheckLastInput()
    {
        float idleTime = Time.time - lastInputTime;

        if (!fired30 && idleTime >= timeTo30)
        {
            fired30 = true;
            On30s_Inactive?.Invoke();
        }

        if (!fired60 && idleTime >= timeTo60)
        {
            fired60 = true;
            On60s_Inactive?.Invoke();
        }
    }
}