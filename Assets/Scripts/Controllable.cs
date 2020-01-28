using System;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    private enum State
    {
        Free,
        Selected,
        MovingLeft,
        MovingRight
    }
    
    //TODO:move to config file
    private const float BLOCK_SIZE = 0.50f;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxSpeed = 1f;
    [SerializeField] private float _timeToMaxSpeed = 2f;
    
    private IInputHandler _inputHandler;
    private Vector3 _position;
    private Transform _transform;
    private State _state;
    private float _currentHoldingTime;
    private float _currentSpeed;
    
    private void Awake()
    {
        _transform = transform;
        _position = _transform.position;
        _state = State.Free;
        _currentSpeed = _maxSpeed;
        
        //decide what input handler to get
        //TODO: move decision to another file
//#if UNITY_EDITOR
//        _inputHandler = gameObject.AddComponent<KeyboardInput>();
//#elif UNITY_ANDROID || UNITY_IOS
        _inputHandler = gameObject.AddComponent<TouchInput>();
//#endif
    }

    private void Update()
    {
        switch (_state) {
            case State.Free:{
                if (_inputHandler.Left() || _inputHandler.Right()) {
                    _state = State.Selected;
                }

                break;
            }
            case State.Selected:{
                if (_inputHandler.HoldingLeft()) {
                    _position = new Vector3(_position.x - BLOCK_SIZE, _position.y);
                    _state = State.MovingLeft;
                }

                if (_inputHandler.HoldingRight()) {
                    _position = new Vector3(_position.x + BLOCK_SIZE, _position.y);
                    _state = State.MovingRight;
                }

                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    _currentHoldingTime = 0;
                    _currentSpeed = _maxSpeed;
                    _state = State.Free;
                }

                break;
            }
            case State.MovingLeft:
                _currentHoldingTime += Time.deltaTime * _speed;
                if (_currentHoldingTime >= _currentSpeed) {
                    _position = new Vector3(_position.x - BLOCK_SIZE, _position.y);
                    _currentSpeed -= 0.2f;
                    _currentHoldingTime = 0;
                }
                
                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    _currentHoldingTime = 0;
                    _currentSpeed = _maxSpeed;
                    _state = State.Free;
                }

                break;
            case State.MovingRight:
                _currentHoldingTime += Time.deltaTime * _speed;
                if (_currentHoldingTime >= _currentSpeed) {
                    _position = new Vector3(_position.x + BLOCK_SIZE, _position.y);
                    _currentSpeed -= 0.2f;
                    _currentHoldingTime = 0;
                }
                
                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    _currentHoldingTime = 0;
                    _currentSpeed = _maxSpeed;
                    _state = State.Free;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        _transform.position = _position;
    }

    public void DropDownOneBlock()
    {
        _position = new Vector3(_position.x, _position.y - BLOCK_SIZE);
    }
}
