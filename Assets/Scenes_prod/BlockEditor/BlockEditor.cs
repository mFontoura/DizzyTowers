using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BlockEditor
{

    public class BlockEditor : MonoBehaviour
    {
        private const int NUM_GRID_ROWS = 5;
        private const int NUM_GRID_COLUMNS = 4;
        private const float INCREMENT = 0.50f;

        [SerializeField] private GameObject _blockPiecePrefab = null;
        [SerializeField] private InputField _newBlockNameInputField = null; 

        private GridBlock[,] _grid;

        private void Awake()
        {
            GenerateGrid(NUM_GRID_ROWS, NUM_GRID_COLUMNS);
        }

        private void GenerateGrid(int rows, int columns)
        {
            float xIncrement = 0;
            float yIncrement = 0;

            _grid = new GridBlock[rows, columns];
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < columns; j++) {
                    //instantiate preview block
                    _grid[i,j] = InstantiateBlock(xIncrement, yIncrement);
                    yIncrement += INCREMENT;
                }
                xIncrement += INCREMENT;
                yIncrement = 0;
            }
        }

        private GridBlock InstantiateBlock(float gridBlockX, float gridBlockY)
        {
            var block = Instantiate(_blockPiecePrefab, transform).AddComponent<GridBlock>();
            block.Init(gridBlockX, gridBlockY);
            
            return block;
        }
        
        public void SaveBlock()
        {
            var newBlock = new GameObject();
            newBlock.name = "NewBlock";
            foreach (var gridBlock in _grid) {
                if (gridBlock.IsActive()) {
                    gridBlock.transform.SetParent(newBlock.transform);
                }
                else {
                    StartCoroutine(gridBlock.NiceDestroyCr());
                }
            }

            var rb = newBlock.AddComponent<Rigidbody2D>();
            newBlock.AddComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;
            
            rb.AddForce(new Vector2(Random.value, 6f), ForceMode2D.Impulse);
            rb.AddTorque(Random.value, ForceMode2D.Impulse);

            var localPath = "Assets/Blocks/prefab_block_" + _newBlockNameInputField.text + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            //TODO: we lose the reference to the materials of the grid blocks
            PrefabUtility.SaveAsPrefabAsset(newBlock.gameObject, localPath);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}