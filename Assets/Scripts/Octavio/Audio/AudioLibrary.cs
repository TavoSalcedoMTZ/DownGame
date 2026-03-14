using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct AudioDataElement
{
    public int AudioID;
    public string AudioName;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume;
    public AudioSpace space;
}
public enum AudioSpace
{
    TwoD,
    ThreeD
}

public struct AudioDataResult
{
    public AudioClip Clip;
    public float Volume;
    public AudioSpace Space;

    public AudioDataResult(AudioClip clip, float volume, AudioSpace space)
    {
        Clip = clip;
        Volume = volume;
        Space = space;
    }
}

public class AudioLibrary : MonoBehaviour
{
    public AudioDataElement[] audioDataElements;

    private Dictionary<int, AudioDataElement> audioById;
    private Dictionary<string, AudioDataElement> audioByName;

    void Awake()
    {
        audioById = new Dictionary<int, AudioDataElement>();
        audioByName = new Dictionary<string, AudioDataElement>();

        foreach (var element in audioDataElements)
        {
            if (!audioById.ContainsKey(element.AudioID))
                audioById.Add(element.AudioID, element);

            if (!audioByName.ContainsKey(element.AudioName))
                audioByName.Add(element.AudioName, element);
        }

        AudioManager.Library = this;
    }

    public void Start()
    {
        
    }

    public AudioDataResult? GetAudio(int id)
    {
        if (audioById.TryGetValue(id, out AudioDataElement element))
            return new AudioDataResult(element.audioClip, element.volume, element.space);

        return null;
    }

    public AudioDataResult? GetAudio(string name)
    {
        if (audioByName.TryGetValue(name, out AudioDataElement element))
            return new AudioDataResult(element.audioClip, element.volume, element.space);

        return null;
    }
}