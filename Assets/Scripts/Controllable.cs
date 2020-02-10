using System;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    private enum State
    {
        Free,
        Selected,
        MovingLeft,
        MovingRight,
        MovingDown
    }
    
    //TODO:move to config file
    private const float BLOCK_SIZE = 0.50f;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxSpeed = 1f;
    //[SerializeField] private float _timeToMaxSpeed = 2f;
    
    private IInputHandler _inputHandler;
    private Vector3 _position;
    private Transform _transform;
    private State _state;
    private float _currentHoldingTime;
    private float _currentSpeed;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _transform = transform;
        _position = _transform.position;
        _state = State.Free;
        _currentSpeed = _maxSpeed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
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
        switch (_state) {
            case State.Free:{
                if (_inputHandler.Left() || _inputHandler.Right() || _inputHandler.Down()) {
                    _state = State.Selected;
                }
                
                if (_inputHandler.Action()) {
                    Rotate();
                }

                break;
            }
            case State.Selected:{
                if (_inputHandler.HoldingLeft()) {
                    _position = new Vector3(_position.x - BLOCK_SIZE, transform.position.y);
                    _state = State.MovingLeft;
                    _rigidbody2D.MovePosition(new Vector2(_position.x, transform.position.y));
                    break;
                }

                if (_inputHandler.HoldingRight()) {
                    _position = new Vector3(_position.x + BLOCK_SIZE, transform.position.y);
                    _state = State.MovingRight;
                    _rigidbody2D.MovePosition(new Vector2(_position.x, transform.position.y));
                    break;
                }

                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    ResetBlockState();
                }

                if (_inputHandler.HoldingDown()) {
                    _position = new Vector3(_position.x, transform.position.y - BLOCK_SIZE);
                    _state = State.MovingDown;
                    _rigidbody2D.MovePosition(new Vector2(transform.position.x, _position.y));
                    break;
                }
                
                if (_inputHandler.Action()) {
                    Rotate();
                }

                break;
            }
            case State.MovingLeft:
                _currentHoldingTime += Time.deltaTime * _speed;
                if (_currentHoldingTime >= _currentSpeed) {
                    _position = new Vector3(_position.x - BLOCK_SIZE, transform.position.y);
                    _currentSpeed -= 0.2f;
                    _currentHoldingTime = 0;
                    _rigidbody2D.MovePosition(new Vector2(_position.x, transform.position.y));
                }
                
                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    ResetBlockState();
                }

                break;
            case State.MovingRight:
                _currentHoldingTime += Time.deltaTime * _speed;
                if (_currentHoldingTime >= _currentSpeed) {
                    _position = new Vector3(_position.x + BLOCK_SIZE, transform.position.y);
                    _currentSpeed -= 0.2f;
                    _currentHoldingTime = 0;
                    _rigidbody2D.MovePosition(new Vector2(_position.x, transform.position.y));
                }
                
                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight()) {
                    ResetBlockState();
                }
                break;
            case State.MovingDown:
                _currentHoldingTime += Time.deltaTime * _speed;
                if (_currentHoldingTime >= _currentSpeed) {
                    _position = new Vector3(_position.x, transform.position.y - BLOCK_SIZE);
                    _currentSpeed -= 0.2f;
                    _currentHoldingTime = 0;
                    _rigidbody2D.MovePosition(new Vector2(transform.position.x, _position.y));
                }
                
                if (_inputHandler.ReleaseLeft() || _inputHandler.ReleaseRight() || _inputHandler.ReleaseDown()) {
                    ResetBlockState();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
    }

    private void ResetBlockState()
    {
        _currentHoldingTime = 0;
        _currentSpeed = _maxSpeed;
        _state = State.Free;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0,0,90f));
    }
}
