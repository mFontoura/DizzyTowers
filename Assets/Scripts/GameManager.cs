using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int POOL_SIZE = 10;

    private static GameManager sInstance;

    public static GameManager Instance => sInstance;

    [SerializeField] private Level[] _levelConfigs;
    [SerializeField] private Transform _blocksPoolParent;

    private List<Player> _players;
    private int _numOfLevels;
    private GameObject[][] _blockPools;
    

    private void Awake()
    {
        if (sInstance == null) {
            sInstance = this;
            Init();
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        _players = new List<Player>();
        _numOfLevels = _levelConfigs.Length;
        _blockPools = new GameObject[_numOfLevels][];

        InitPools();
    }

    private void InitPools()
    {
        for (var i = 0; i < _blockPools.Length; i++) {
            var blockPrefabs = _levelConfigs[i].GetBlockPrefabs();
            var blockSprites = _levelConfigs[i].GetBlockSprites();
            
            _blockPools[i] = new GameObject [POOL_SIZE];

            // Initialize each element of the pool with a new random block
            for (var j = 0; j < _blockPools[i].Length; j++) {
                _blockPools[i][j] = InstantiateBlock(blockPrefabs[Random.Range(0, blockPrefabs.Length)],
                    blockSprites[Random.Range(0, blockPrefabs.Length)], _levelConfigs[i].SpriteMaterial);
            }
            /*foreach (var block in _blockPools[i]) {
                block = InstantiateBlock(blockPrefabs[Random.Range(0, blockPrefabs.Length)], blockSprites[Random.Range(0, blockPrefabs.Length)]);
            }*/
            
        }
    }

    private GameObject InstantiateBlock(GameObject blockPrefab, Sprite blockSprite, Material spriteMaterial)
    {
        var block = Instantiate(blockPrefab, _blocksPoolParent).AddComponent<Block>();
        block.Init(blockSprite, spriteMaterial);

        return block.gameObject;
    }
}
