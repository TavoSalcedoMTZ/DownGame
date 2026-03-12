using UnityEngine;
using System.Collections;

public class EnemyFX : MonoBehaviour
{

    public ParticleSystem hitEffect;
    public ParticleSystem DieParticle;
    public ParticleSystem AfterDie;
    public GameObject Mesh;


    public void StartDieSequence() {
    
        StartCoroutine(DieSequence());    
    }

    private IEnumerator DieSequence()
    {
        float delay = hitEffect.main.duration * 0.85f;
        hitEffect.Play();
        yield return delay;
        DieParticle.Play();
        yield return delay;
        Mesh.SetActive(false);
        AfterDie.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5F);
        Destroy(gameObject);

    }
   

}
