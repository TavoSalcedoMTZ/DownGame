using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance {  get; private set; }


    public PlayerController PlayerController;
    public UIManager UIManager;
    public BindingDetector BindingDetector;


    public void Awake()
    {
        Instance = this;
    }


    public void Lost()
    {
   
    }
}
