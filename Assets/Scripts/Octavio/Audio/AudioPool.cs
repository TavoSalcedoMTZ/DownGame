using UnityEngine;
using System.Collections.Generic;

public class AudioPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    private List<AudioSource> sources = new List<AudioSource>();

    void Awake()
    {
        CreateAudioSource(poolSize);
    }

    public void Start()
    {
        AudioManager.Pool = this;

    }

    public void PlayClipAt(AudioClip clip, Vector3 position, float volume, AudioSpace space)
    {
        AudioSource source = GetAvailableSource();

        source.transform.position = position;
        source.clip = clip;
        source.volume = volume;

        source.spatialBlend = space == AudioSpace.ThreeD ? 1f : 0f;

        source.Play();
    }

    public void CreateAudioSource(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject obj = new GameObject("PooledAudio_" + sources.Count);
            obj.transform.parent = transform;

            AudioSource source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 1f;

            sources.Add(source);
        }
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var s in sources)
        {
            if (!s.isPlaying)
                return s;
        }

        CreateAudioSource(1);
        return sources[sources.Count - 1];
    }
}