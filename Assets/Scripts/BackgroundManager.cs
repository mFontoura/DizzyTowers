using System;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private FloatVar _towerHeight = null;
    [SerializeField] private Transform _firstBackground = null;
    [SerializeField] private GameObject _backgroundPrefab = null;
    
    private Camera _camera;
    private Transform _cameraTransform;
    private float _backgroundHeight;
    private float backgroundLimit;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraTransform = _camera.transform;

        _backgroundHeight = _firstBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        backgroundLimit = _backgroundHeight / 3;
    }

    private void Update()
    {
        UpdateCameraPosition();
        UpdateBackground();
    }

    private void LateUpdate()
    {
        DebugHeight();
    }
    
    private void UpdateBackground()
    {
        if ((_towerHeight.value - GameManager.Instance.StartingHeight) > backgroundLimit) {
            var newBackground = Instantiate(_backgroundPrefab).GetComponent<Transform>();
            newBackground.position = new Vector3(_firstBackground.transform.position.x, _towerHeight.value + _backgroundHeight, _firstBackground.transform.position.z);
            backgroundLimit += _backgroundHeight;
        }
    }
    

    private void UpdateCameraPosition()
    {
        var position = _cameraTransform.position;
        position = new Vector3(
            position.x,
            Mathf.Lerp(position.y, _towerHeight.value > 0 ? _towerHeight.value : 0, Time.deltaTime),
            position.z);
        _cameraTransform.position = position;
    }

    private void DebugHeight()
    {
        var position = _cameraTransform.position;
        var start = new Vector3(position.x - 2, _towerHeight.value, position.z);
        var end = new Vector3(position.x + 2, _towerHeight.value, position.z);
        
        Debug.DrawLine(start, end, Color.black);
    }

}
