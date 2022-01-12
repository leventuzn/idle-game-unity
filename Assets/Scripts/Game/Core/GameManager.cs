using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Models;
using Game.Common;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        [SerializeField]
        private GameObject _emptyTilePoolPrefab;
        
        [SerializeField]
        private GameObject _buildingTilePoolPrefab;

        [SerializeField]
        private GameObject _buildingPrefab;

        [SerializeField]
        private GameObject _buildings;
        
        [SerializeField]
        private GameObject _tileGrid;

        private GameObject _emptyTilePool;
        private GameObject _buildingTilePool;

        public List<BuildingSO> buildingCardDatas;

        public GameObject[] tiles;

        public GoldSystem goldSystem;
        public GemSystem gemSystem;
        public CountdownSystem countdownSystem;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            goldSystem = GetComponent<GoldSystem>();
            gemSystem = GetComponent<GemSystem>();
            countdownSystem = GetComponent<CountdownSystem>();

            tiles = new GameObject[100];

            InitializeObjectPools();
            LoadGameData();
            LoadMap();
            LoadBuildings();
        }

        public void UpdateTileAt(int x, int y)
        {
            var tileIndex = x + (y * 10);
            _emptyTilePool.GetComponent<ObjectPool>().ReturnToPool(tiles[tileIndex]);
            var tile = _buildingTilePool.GetComponent<ObjectPool>().GetFromPool();
            tile.transform.SetParent(_tileGrid.transform);
            tile.transform.SetSiblingIndex(tileIndex);
            tile.transform.localScale = new Vector3(1f, 1f, 1f);
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tiles[tileIndex] = tile;
            GameDataManager.Instance.UpdateTileStatus(x, y);
        }

        public void RestartGame()
        {
            GameDataManager.Instance.m_GameData.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void InitializeObjectPools()
        {
            _emptyTilePool = Instantiate(_emptyTilePoolPrefab);
            _emptyTilePool.name = _emptyTilePoolPrefab.name;
            _buildingTilePool = Instantiate(_buildingTilePoolPrefab);
            _buildingTilePool.name = _buildingTilePoolPrefab.name;
        }

        private void LoadGameData()
        {
            GameDataManager.Instance.Initialize();
        }

        private void LoadMap()
        {
            var tileStatus = GameDataManager.Instance.m_GameData.m_MapData.m_TileStatus;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    var tileIndex = i + (j * 10);
                    var tile = tileStatus[tileIndex] ? _buildingTilePool.GetComponent<ObjectPool>().GetFromPool() : _emptyTilePool.GetComponent<ObjectPool>().GetFromPool();
                    tile.transform.SetParent(_tileGrid.transform);
                    tile.transform.localScale = new Vector3(1f, 1f, 1f);
                    tile.GetComponent<Tile>().x = i;
                    tile.GetComponent<Tile>().y = j;
                    tiles[tileIndex] = tile;
                }
            }
        }

        private void LoadBuildings()
        {
            var buildings = GameDataManager.Instance.m_GameData.m_MapData.m_Buildings;
            foreach(var data in buildings)
            {
                var building = Instantiate(_buildingPrefab, _buildings.transform);
                building.name = _buildingPrefab.name;
                building.GetComponent<Building>().LoadData(data);
            }
        }
    }
}

