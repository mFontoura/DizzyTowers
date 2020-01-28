﻿using UnityEngine;

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
}
