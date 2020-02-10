using UnityEngine;

public class MainMenuManager : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject _optionsPrefab = null;

    public void OpenOptionsMenu()
    {
        Instantiate(_optionsPrefab, transform.parent).GetComponent<OptionsManager>().Init(this, Color.white);
        Close();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }
}
