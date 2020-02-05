using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = null;
    [SerializeField] private Transform _parentTransform = null;

    public void InstantiatePrefab()
    {
        if (ReferenceEquals(_parentTransform, null)) {
            Instantiate(_prefab);
            return;
        }
        
        InstantiatePrefabWithParent();
    }

    private void InstantiatePrefabWithParent()
    {
        Instantiate(_prefab, _parentTransform);
    }
}
