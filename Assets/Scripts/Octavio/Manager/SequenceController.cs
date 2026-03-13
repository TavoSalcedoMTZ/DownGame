using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using System;

public class SequenceController : MonoBehaviour
{
    public static SequenceController Instance;

    public CinemachineCamera cinemachine;
    public bool IsPlayingSequence;
    void Awake()
    {
        Instance = this;
    }



    public void PlayEnemyDefeatSequence(EnemyController enemy, PlayerAttack attack)
    {
        if (IsPlayingSequence) return;

        IsPlayingSequence = true;

        attack.CanAttack = false;

        StartCoroutine(EnemyDefeatSequence(enemy, () =>
        {
            attack.CanAttack = true;
            attack.IsAttacking = false;
            IsPlayingSequence = false;
        }));
    }

    IEnumerator EnemyDefeatSequence(EnemyController enemy, Action onComplete)
    {
        yield return ZoomInAndOut(enemy.TakeDamage);

        onComplete?.Invoke();
        enemy.gameObject.SetActive(false);
    }

    IEnumerator ZoomInAndOut(Action action = null)
    {
        float baseSize = cinemachine.Lens.OrthographicSize;

        float zoomOut = baseSize * 1.2f;
        float zoomIn = baseSize * 0.5f;

        yield return Zoom(baseSize, zoomOut, 0.22f);

        StartCoroutine(SlowWorld(1f, 0.15f, 0.08f));

        yield return Zoom(zoomOut, zoomIn, 0.07f);

        action?.Invoke();

        yield return new WaitForSecondsRealtime(0.15f);

        StartCoroutine(SlowWorld(WorldSettings.movementScale, 1f, 0.12f));

        yield return Zoom(zoomIn, baseSize, 0.18f);
    }

    IEnumerator Zoom(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            float alpha = t / duration;
            float value = Mathf.Lerp(from, to, alpha);

            cinemachine.Lens.OrthographicSize = value;

            yield return null;
        }

        cinemachine.Lens.OrthographicSize = to;
    }

    IEnumerator SlowWorld(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            float alpha = t / duration;
            WorldSettings.movementScale = Mathf.Lerp(from, to, alpha);

            yield return null;
        }

        WorldSettings.movementScale = to;
    }
}