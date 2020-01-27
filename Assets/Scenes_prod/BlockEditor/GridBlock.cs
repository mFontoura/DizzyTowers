using System;
using UnityEngine;

namespace BlockEditor
{
    public class GridBlock : MonoBehaviour
    {
        public float x;
        public float y;

        private bool _active;
        private Camera _mainCamera;
        private Collider2D _collider;

        private void Awake()
        {
            _mainCamera = Camera.current;
            _collider = GetComponent<Collider2D>();
        }

        public void Init(float gridBlockX, float gridBlockY)
        {
            transform.localPosition = new Vector3(gridBlockX, gridBlockY);
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }

        private void ToggleActive()
        {
            _active = !_active;
        }

        private void Update()
        {
            if (Input.touchCount > 0) {
                var touch = Input.GetTouch(0);
                
                var wp = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                var touchPosition = new Vector2(wp.x, wp.y);
                
                 
                if (_collider == Physics2D.OverlapPoint(touchPosition)){
                    Debug.Log("HIT!");
                }
                else{
                    Debug.Log("MISS");
                }
            }
        }
    }
}