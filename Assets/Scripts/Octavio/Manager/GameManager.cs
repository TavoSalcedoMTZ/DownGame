using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance {  get; private set; }


    public PlayerController PlayerController;
    public UIManager UIManager;
    public BindingDetector BindingDetector;

    public Vector3 CheckPoint;


    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        CheckPoint = PlayerController.transform.position;

    }


    public void Lost()
    {
   
        PlayerController.MovePlayer(CheckPoint);

    }
}
