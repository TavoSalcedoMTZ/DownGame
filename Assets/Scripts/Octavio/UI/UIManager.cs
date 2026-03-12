using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    public Volume volume;

    public float slowMotionScale = 0.2f;
    public float transitionTime = 2f;
    public BindingDetector BindingDetector;

    public GameObject Inactive;

    private ColorAdjustments colorAdjustments;
    public DepthOfField depthOfField;

    private Coroutine currentFlow;



    void Start()
    {
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out depthOfField);
    }

    private void OnEnable()
    {
       BindingDetector.ActiveChanged += ActiveController;
    }

    private void OnDisable()
    {
      BindingDetector.ActiveChanged -= ActiveController;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActiveController(false);
        }
    }

    public void ActiveController(bool active)
    {
        if (active)
        {
            if (currentFlow != null)
                StopCoroutine(currentFlow);

            currentFlow = StartCoroutine(RestoreSequence());
        }
        else
        {
            if (currentFlow != null)
                StopCoroutine(currentFlow);

            currentFlow = StartCoroutine(InactiveFlow());
        }
    }

    IEnumerator InactiveFlow()
    {
        yield return StartCoroutine(InactiveSequence());

        Inactive.SetActive(true);
    }

    IEnumerator InactiveSequence()
    {
        float t = 0f;

        float startScale = WorldSettings.movementScale;
        float startSaturation = colorAdjustments.saturation.value;
        float startCameraLeght = depthOfField.focalLength.value;

        while (t < transitionTime)
        {
            t += Time.unscaledDeltaTime;

            float lerp = t / transitionTime;

            WorldSettings.movementScale = Mathf.Lerp(startScale, slowMotionScale, lerp);
            colorAdjustments.saturation.value = Mathf.Lerp(startSaturation, -100f, lerp);
            depthOfField.focalLength.value = Mathf.Lerp(startCameraLeght, 100f , lerp);

            yield return null;
        }

        WorldSettings.movementScale = slowMotionScale;
        colorAdjustments.saturation.value = -100f;
    }

    IEnumerator RestoreSequence()
    {
        Inactive.SetActive(false);

        float t = 0f;

        float startScale = WorldSettings.movementScale;
        float startSaturation = colorAdjustments.saturation.value;
        float startCameraLeght = depthOfField.focalLength.value;

        while (t < transitionTime)
        {
            t += Time.unscaledDeltaTime;

            float lerp = t / transitionTime;

            WorldSettings.movementScale = Mathf.Lerp(startScale, 1f, lerp);
            colorAdjustments.saturation.value = Mathf.Lerp(startSaturation, 0f, lerp);
            depthOfField.focalLength.value = Mathf.Lerp(startCameraLeght, 0, lerp);

            yield return null;
        }

        WorldSettings.movementScale = 1f;
        colorAdjustments.saturation.value = 0f;
    }
}