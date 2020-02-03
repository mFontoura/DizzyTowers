using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parentTransform;

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
