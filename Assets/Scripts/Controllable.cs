using System;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    //TODO:move to config file
    private const float BLOCK_SIZE = 0.50f; 
    
    private IInputHandler _inputHandler;
    private Vector3 _position;
    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
        _position = _transform.position;
        //decide what input handler to get
        //TODO: move decision to another file
#if UNITY_EDITOR
        _inputHandler = gameObject.AddComponent<KeyboardInput>();
#elif UNITY_ANDROID || UNITY_IOS
        _inputHandler = gameObject.AddComponent<TouchInput>();
#endif
    }

    private void Update()
    {
        if (_inputHandler.Left()) {
            _position = new Vector3(_position.x - BLOCK_SIZE, _position.y);
        }

        if (_inputHandler.Right()) {
            _position = new Vector3(_position.x + BLOCK_SIZE, _position.y);
        }

        _transform.position = _position;
    }

    public void DropDownOneBlock()
    {
        _position = new Vector3(_position.x, _position.y - BLOCK_SIZE);
    }
}
