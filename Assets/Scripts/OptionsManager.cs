using UnityEngine;

public class OptionsManager : MonoBehaviour, IMenu
{
    private IMenu _parentMenu;
    
    public void Init(IMenu parentMenu)
    {
        _parentMenu = parentMenu;
    }
    
    public void BackButtonPress()
    {
        if(ReferenceEquals(_parentMenu, null)){
            Debug.LogError("Missing reference to parent Menu");    
            return;
        }
        
        _parentMenu.Open();
        Close();
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
