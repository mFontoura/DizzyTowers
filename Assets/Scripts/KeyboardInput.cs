using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputHandler
{
    public bool Left()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A);
    }

    public bool Right()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D);
    }
}
