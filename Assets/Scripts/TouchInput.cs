using System;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour, IInputHandler
{
    private enum TouchState
    {
        Pressed,
        Up
    }

    private Vector3 _lastPosition;
    private float _currentHoldTime;
    //private float _maxTimeToActivateAction = 0.1f;
    
    public bool Left()
    {
        if (Input.touchCount <= 0) return false;
        if (Input.GetTouch(0).phase != TouchPhase.Began) return false;

        _currentHoldTime += Time.deltaTime;
        _lastPosition = Input.GetTouch(0).position;
        return true;

    }

    public bool Right()
    {
        if (Input.touchCount <= 0) return false;
        if (Input.GetTouch(0).phase != TouchPhase.Began) return false;
        
        _currentHoldTime += Time.deltaTime;
        _lastPosition = Input.GetTouch(0).position;
        return true;

    }

    public bool HoldingLeft()
    {
        if (Input.touchCount <= 0) return false;
        if (Input.GetTouch(0).phase != TouchPhase.Moved) return false;
        if (!(Input.GetTouch(0).position.x < _lastPosition.x)) return false;
        
        _currentHoldTime = 0;
        _lastPosition = Input.GetTouch(0).position;
        return true;
    }

    public bool HoldingRight()
    {
        if (Input.touchCount <= 0) return false;
        if (Input.GetTouch(0).phase != TouchPhase.Moved) return false;
        if (!(Input.GetTouch(0).position.x > _lastPosition.x)) return false;
        
        _currentHoldTime = 0;
        _lastPosition = Input.GetTouch(0).position;
        return true;
    }

    public bool ReleaseLeft()
    {
        if (Input.touchCount <= 0) return false;
        return Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    public bool ReleaseRight()
    {
        if (Input.touchCount <= 0) return false;
        return Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    public bool Action()
    {
        if (Input.touchCount <= 0) return false;
        
        return Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    public bool HoldingDown()
    {
        throw new NotImplementedException();
    }

    public bool Down()
    {
        throw new NotImplementedException();
    }

    public bool ReleaseDown()
    {
        throw new NotImplementedException();
    }
}
