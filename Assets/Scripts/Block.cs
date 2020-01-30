using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    //TODO:move to config file
    private const float BLOCK_SIZE = 0.50f;
    
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private Controllable _controllable;
    private bool _isActive;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _controllable = GetComponent<Controllable>();
        _rigidbody2D.gravityScale = 0;
    }
    
    public void Init(Sprite blockSprite, Material spriteMaterial)
    {
        foreach (var blockRenderer in GetComponentsInChildren<SpriteRenderer>()) {
            blockRenderer.sprite = blockSprite;
            blockRenderer.sortingLayerName = "GameplayObjs";
            blockRenderer.material = spriteMaterial;
        }

        gameObject.tag = "physics_activator";
        gameObject.SetActive(false);
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
        _controllable.enabled = false;
        _isActive = false;
    }


    public void Activate(bool activate)
    {
        _controllable.enabled = activate;
        _rigidbody2D.gravityScale = 0;
        _isActive = activate;
        
        gameObject.SetActive(activate);
    }
    
    public void DropDownOneBlock()
    {
        _rigidbody2D.MovePosition(new Vector3(transform.position.x, transform.position.y - BLOCK_SIZE));
    }
}
