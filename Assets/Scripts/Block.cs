using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("beh");
        if (other.gameObject.CompareTag("physics_activator")) {
            DisableControls();
            
        }
    }

    private void DisableControls()
    {
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.gravityScale = 1;
        GetComponent<RoutineTask>().StopTask();
        GetComponent<Controllable>().enabled = false;
    }
}
