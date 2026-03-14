using UnityEngine;

public class SceneLoaderBridge : MonoBehaviour
{
    public bool FadeInOnStart;
   

    public void Start()
    {
        if (FadeInOnStart)
            FadeIn();
    }

    public void LoadScene(string sceneName)
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneName);
    }

    public void FadeIn()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.CallFadeIn();
    }

    public void FadeOut()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.CallFadeOut();
    }




}