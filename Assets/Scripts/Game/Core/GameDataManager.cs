using Game.Common;
using System.Collections;
using UnityEngine;

namespace Game.Core
{
    public class GameDataManager : MonoBehaviour
    {
        public GameData m_GameData;

        private static GameDataManager _instance;

        public static GameDataManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameDataManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameDataManager>();
                }
            } else if (_instance != this)
            {
                Debug.Log("destroyed");
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize()
        {
            m_GameData = new GameData();

            LoadGame();
        }

        public void UpdateGoldValue(int value)
        {
            m_GameData.m_PlayerData.m_Golds = value;
            SaveGame();
        }

        public void UpdateGemValue(int value)
        {
            m_GameData.m_PlayerData.m_Gems = value;
            SaveGame();
        }

        public void UpdateTileStatus(int x, int y)
        {
            var tileIndex = x + (y * 10);
            m_GameData.m_MapData.m_TileStatus[tileIndex] = true;
            SaveGame();
        }

        public void CreateBuildingData(BuildingData buildingData)
        {
            m_GameData.m_MapData.m_Buildings.Add(buildingData);
            SaveGame();
        }

        public void UpdateRemainingGenerationDuration(int id, float remainingTime)
        {
            m_GameData.m_MapData.m_Buildings.Find(x => x.m_Id == id).m_RemainingGenerationDuration = remainingTime;
        }

        public void SaveGame()
        {
            SaveGameManager.Instance.SaveGame(m_GameData);
        }

        public void LoadGame()
        {
            StartCoroutine(LoadGameCoroutine());
        }

        public void RestartGame()
        {
            m_GameData = new GameData();
            SaveGame();
        }

        public IEnumerator LoadGameCoroutine()
        {
            while (!SaveGameManager.Instance.m_Initialized)
            {
                yield return new WaitForSeconds(0.01f);
            }

            SaveGameManager.Instance.LoadGame(m_GameData);
        }
    }
}

