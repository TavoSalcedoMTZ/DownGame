using UnityEngine;
using System.Collections.Generic;

public static class AudioManager
{
    public static AudioPool Pool;
    public static AudioLibrary Library;

    static Dictionary<int, AudioSource> persistentSources = new Dictionary<int, AudioSource>();


    public static void Play(int id, Vector3 position)
    {
        if (Pool == null || Library == null)
            return;

        AudioDataResult? result = Library.GetAudio(id);

        if (result == null)
            return;

        Pool.PlayClipAt(
            result.Value.Clip,
            position,
            result.Value.Volume,
            result.Value.Space
        );
    }


    public static void Play(string name, Vector3 position)
    {
        if (Pool == null || Library == null)
            return;

        AudioDataResult? result = Library.GetAudio(name);

        if (result == null)
            return;

        Pool.PlayClipAt(
            result.Value.Clip,
            position,
            result.Value.Volume,
            result.Value.Space
        );
    }


    public static AudioSource GetPersistentSource(int id, Transform parent)
    {
        if (Pool == null || Library == null)
            return null;

        if (persistentSources.TryGetValue(id, out AudioSource src))
            return src;

        AudioDataResult? result = Library.GetAudio(id);

        if (result == null)
            return null;

        GameObject obj = new GameObject("PersistentAudio_" + id);
        obj.transform.parent = parent;

        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = result.Value.Clip;
        source.volume = result.Value.Volume;
        source.loop = true;

        source.spatialBlend = result.Value.Space == AudioSpace.ThreeD ? 1f : 0f;

        persistentSources.Add(id, source);

        return source;
    }


    public static void StopPersistent(int id)
    {
        if (!persistentSources.TryGetValue(id, out AudioSource src))
            return;

        if (src != null)
            src.Stop();
    }


    public static void RemovePersistent(int id)
    {
        if (!persistentSources.TryGetValue(id, out AudioSource src))
            return;

        if (src != null)
            Object.Destroy(src.gameObject);

        persistentSources.Remove(id);
    }
}