using System;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private FloatVar _towerHeight = null;
    [SerializeField] private Transform _firstBackground = null;
    [SerializeField] private GameObject _backgroundPrefab = null;
    [SerializeField] private float _cameraSpeed = 1;
    
    private Camera _camera;
    private Transform _cameraTransform;
    private float _backgroundHeight;
    private float backgroundLimit;
    private float _prevBackPos;
    

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraTransform = _camera.transform;

        _prevBackPos = _firstBackground.position.y;
        _backgroundHeight = _firstBackground.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        //backgroundLimit = 0f;
        
    }

    private void Start()
    {
        backgroundLimit += GameManager.Instance.StartingHeight;
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
        if (!(_towerHeight.value > backgroundLimit)) return;
        
        _prevBackPos += _backgroundHeight;
        var newBackground = Instantiate(_backgroundPrefab).transform;
        newBackground.position = new Vector3(_firstBackground.transform.position.x, _prevBackPos, _firstBackground.transform.position.z);
        backgroundLimit += _backgroundHeight;
    }
    

    private void UpdateCameraPosition()
    {
        var position = _cameraTransform.position;
        position = new Vector3(
            position.x,
            Mathf.Lerp(position.y, _towerHeight.value > 0 ? _towerHeight.value : 0, Time.deltaTime * _cameraSpeed),
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
