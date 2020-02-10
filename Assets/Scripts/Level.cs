using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create New Difficulty level", order = 1 )]
public class Level : ScriptableObject
{
    [SerializeField] private string _levelName;
    [SerializeField] private GameObject[] _blockPrefabs = null;
    [SerializeField] private Sprite[] _blockImages = null;
    [SerializeField] private Material _spriteMaterial = null;

    public Material SpriteMaterial => _spriteMaterial;


    public GameObject[] GetBlockPrefabs()
    {
        return _blockPrefabs;
    }

    public Sprite[] GetBlockSprites()
    {
        return _blockImages;
    }
}
