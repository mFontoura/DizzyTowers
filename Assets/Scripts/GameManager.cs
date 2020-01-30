using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum EndGame
    {
        Victory,
        Defeat
    }
    
    public delegate void OnGameEnded (EndGame endGame);
    public static event OnGameEnded gameEnded;
    
    private const int POOL_SIZE = 50;
    //TODO: should all be in a config
    private const float UNIT_SIZE = 0.50f;
    private const float TARGET_HEIGHT_TO_WIN = 6;
    private const int STARTING_HEALTH_POINTS = 5;

    private static GameManager sInstance;

    public static GameManager Instance => sInstance;

    [SerializeField] private Level[] _levelConfigs;
    [SerializeField] private Transform _blocksPoolParent;
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private float _startingHeight;
    [SerializeField] private FloatVar _currentHeight;
    [SerializeField] private IntVar _currentHealthPoints;

    private List<Player> _players;
    private int _numOfLevels;
    private int _currentLevel;
    private Block[][] _blockPools;
    private int _currentPoolIndex;
    private bool _gameIsRunning;
    private Block _activeBlock;
    private float _passedTimeSinceLastDrop;
    private float _heightGoal;
    private bool _gameHasEnded;


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
        _heightGoal = (UNIT_SIZE * TARGET_HEIGHT_TO_WIN) + _startingHeight;
        _currentHeight.SetValue(_startingHeight);
        
        //TODO: health system should be moved to a different system or maybe to the player
        _currentHealthPoints.SetValue(STARTING_HEALTH_POINTS);

        InitPools();
        _players.Add(new Player(_currentHealthPoints, _blockPools));
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
        block.blockTouchedKillZone += MoveBackToPool;

        return block;
    }

    private void MoveBackToPool(Block block)
    {
        block.Activate(false);
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
        if (!_gameIsRunning || _gameHasEnded) return;
        
        if(ReachedTargetHeight()) return;

        if (_currentHealthPoints.GetValue() <= 0) {
            gameEnded?.Invoke(EndGame.Defeat);
            _gameHasEnded = true;
            return;
        }
        
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

    private bool ReachedTargetHeight()
    {
        //TODO: Need to optimize a loooot
        var highestY = _startingHeight;
        foreach (var child in _blocksPoolParent.GetComponentsInChildren<Block>()) {
            if (child.IsActive) continue;
            if (child.transform.position.y > highestY) {
                highestY = child.transform.position.y;
            }

        }
        
        _currentHeight.SetValue(highestY);

        if (!(highestY >= _heightGoal)) return false;
        _gameHasEnded = true;
        gameEnded?.Invoke(EndGame.Victory);
        return true;

    }

    private void FixedUpdate()
    {
        if (!ReferenceEquals(_activeBlock, null)) {
            _passedTimeSinceLastDrop += Time.deltaTime;
            if (_passedTimeSinceLastDrop >= 0.5f) {
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
