using System;
using UnityEngine;

public class PauseManager : MonoBehaviour, IMenu
{
    public static PauseManager Instance;
    
    [SerializeField] private GameObject _optionsPrefab = null;

    private void Awake()
    {
        if (!ReferenceEquals(Instance, null)) {
            Instance.Open();
            return;
        }
        
        Instance = this;
    }

    public void OpenOptionsMenu()
    {
        Instantiate(_optionsPrefab, transform.parent).GetComponent<OptionsManager>().Init(this);
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
