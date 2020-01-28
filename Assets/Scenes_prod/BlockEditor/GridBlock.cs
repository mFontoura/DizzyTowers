#if UNITY_EDITOR

using System;
using System.Collections;
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
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            //TODO: optmize
            _mainCamera = FindObjectOfType<Camera>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(float gridBlockX, float gridBlockY)
        {
            transform.localPosition = new Vector3(gridBlockX, gridBlockY);
            _spriteRenderer.material.color = Color.red;
            _collider.isTrigger = true;

        }

        private void Activate(bool activate)
        {
            _active = activate;

            if (activate) {
                _spriteRenderer.material.color = Color.white;
                _collider.usedByComposite = true;
                _spriteRenderer.sortingOrder = 1;
            }
            else {
                _spriteRenderer.material.color = Color.red;
                _collider.usedByComposite = false;
                _spriteRenderer.sortingOrder = 0;
            }
        }
        
        public bool IsActive()
        {
            return _active;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            
            var wp = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var touchPosition = new Vector2(wp.x, wp.y);

            if (_collider == Physics2D.OverlapPoint(touchPosition)) {
                Activate(true);
            }
        }

        public IEnumerator NiceDestroyCr()
        {
            yield return new WaitForSeconds(1);
            while (Math.Abs(_spriteRenderer.color.a) > 0.001f) {
                _spriteRenderer.color = new Color(
                    _spriteRenderer.color.r,
                    _spriteRenderer.color.g,
                    _spriteRenderer.color.b, 
                    Mathf.Lerp(_spriteRenderer.color.a, 0, (Time.deltaTime/0.5f)));
                //_spriteRenderer.color.a.
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
#endif