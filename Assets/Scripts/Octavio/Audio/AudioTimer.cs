using UnityEngine;
using System.Collections;

public class AudioTimer : MonoBehaviour
{
    public float delay = 1f;
    public int index;

    AudioSource source;
    AudioDataResult? audioData;

    void Start()
    {
        audioData = AudioManager.Library.GetAudio(index);

        if (audioData == null)
            return;

        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = audioData.Value.Space == AudioSpace.ThreeD ? 1f : 0f;
        source.clip = audioData.Value.Clip;
        source.volume = audioData.Value.Volume;

        StartCoroutine(AudioSequence());
    }

    IEnumerator AudioSequence()
    {
        while (true)
        {
            source.Play();

            yield return new WaitForSeconds(delay);
        }
    }
}