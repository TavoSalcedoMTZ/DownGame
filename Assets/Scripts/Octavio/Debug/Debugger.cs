using UnityEngine;

public class Debugger : MonoBehaviour
{

    public void Log(string message)
    {
        Debug.Log(message);
    }

    public void TestMessage()
    {
        Log("This is a test message from the Debugger.");
    }

}
