using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public EnemyController enemyController;
    public GameObject parryIndicator;


    public void ShowParryIndicator()
        {
            parryIndicator.SetActive(true);
        }
    
        public void HideParryIndicator()
        {
            parryIndicator.SetActive(false);
    }


}
