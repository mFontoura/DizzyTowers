using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private const int POOL_SIZE = 50;

    private static GameManager sInstance;

    public static GameManager Instance => sInstance;

    [SerializeField] private Level[] _levelConfigs;
    [SerializeField] private Transform _blocksPoolParent;
    [SerializeField] private Vector3 _spawnPoint;

    private List<Player> _players;
    private int _numOfLevels;
    private int _currentLevel;
    private Block[][] _blockPools;
    private int _currentPoolIndex;
    private bool _gameIsRunning;
    private Block _activeBlock;
    private float _passedTimeSinceLastDrop;
    

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
        _blockPools = new Block[_numOfLevels][];

        InitPools();
    }

    private void InitPools()
    {
        for (var i = 0; i < _blockPools.Length; i++) {
            var blockPrefabs = _levelConfigs[i].GetBlockPrefabs();
            var blockSprites = _levelConfigs[i].GetBlockSprites();
            
            _blockPools[i] = new Block [POOL_SIZE];

            // Initialize each element of the pool with a new random block
            for (var j = 0; j < _blockPools[i].Length; j++) {
                _blockPools[i][j] = InstantiateBlock(blockPrefabs[Random.Range(0, blockPrefabs.Length)],
                    blockSprites[Random.Range(0, blockPrefabs.Length)], _levelConfigs[i].SpriteMaterial);
            }
        }
    }

    private Block InstantiateBlock(GameObject blockPrefab, Sprite blockSprite, Material spriteMaterial)
    {
        var block = Instantiate(blockPrefab, _blocksPoolParent).AddComponent<Block>();
        block.Init(blockSprite, spriteMaterial);

        return block;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _gameIsRunning = true;
    }

    private void PauseGame()
    {
        _gameIsRunning = false;
    }

    private void Update()
    {
        if (!_gameIsRunning) return;
        
        if (ReferenceEquals(_activeBlock, null)) {
            var blockObject = GetNextBlockToSpawn(_blockPools[_currentLevel]);
            SpawnBlock(blockObject);
            _activeBlock = blockObject;
        }
        else {
            if (!_activeBlock.IsActive) {
                _activeBlock = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!ReferenceEquals(_activeBlock, null)) {
            _passedTimeSinceLastDrop += Time.deltaTime;
            if (_passedTimeSinceLastDrop >= 1f) {
                _activeBlock.DropDownOneBlock();
                _passedTimeSinceLastDrop = 0f;
            }
        }
    }

    private Block GetNextBlockToSpawn(IReadOnlyList<Block> blockPool)
    {
        _currentPoolIndex++;
        return blockPool[_currentPoolIndex];
    }

    private void SpawnBlock(Block block)
    {
        block.transform.position = _spawnPoint;
        block.Activate(true);
    }
}
