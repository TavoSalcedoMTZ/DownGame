using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Starter : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(STARTER());
    }   

    private IEnumerator STARTER()
    {
        yield return new WaitUntil(() => SceneLoader.Instance != null);

        SceneLoader.Instance.CallFadeIn();
    }
   
}
