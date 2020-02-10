using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputHandler
{
    public bool Left()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    public bool HoldingLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    public bool Right()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    
    public bool HoldingRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    public bool ReleaseLeft()
    {
        return Input.GetKeyUp(KeyCode.A);
    }

    public bool ReleaseRight()
    {
        return Input.GetKeyUp(KeyCode.D);
    }

    public bool Action()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }
    
    public bool Down()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    public bool ReleaseDown()
    {
        return Input.GetKeyUp(KeyCode.S);
    }

    public bool HoldingDown()
    {
        return Input.GetKey(KeyCode.S);
    }
}
