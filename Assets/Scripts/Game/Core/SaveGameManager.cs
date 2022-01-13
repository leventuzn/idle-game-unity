using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Game.Core
{
    public class SaveGameManager : MonoBehaviour
    {
        private static SaveGameManager _instance;

        public static SaveGameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private string m_SaveGameFolder = "IdleGame";
        private string m_SavedGameFileName = "save";
        private string m_SavedGameFileExtension = "json";
        private string m_persistentDataPath;

        public bool m_Initialized = false;
        

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveGameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<SaveGameManager>();
                }
            } else if (_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            m_persistentDataPath = Application.persistentDataPath;
        }

        public void Initialize()
        {
            m_Initialized = true;
            m_persistentDataPath = Application.persistentDataPath;
            CreateDirectory();
        }

        private string GetSaveGameFilePath()
        {
            return m_persistentDataPath + "/" + m_SaveGameFolder + "/" + m_SavedGameFileName + "." + m_SavedGameFileExtension;
        }

        private void CreateDirectory()
        {
            string saveDirectory = m_persistentDataPath + "/" + m_SaveGameFolder;
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
        }

        public bool SaveGame(GameData gameData)
        {
            bool success = true;
            try
            {
                JObject jObject = gameData.Serialize();
                string jsonString = jObject.ToString(Newtonsoft.Json.Formatting.None);
                string saveGamePath = GetSaveGameFilePath();
                var file = new StreamWriter(saveGamePath);
                file.WriteLine(jsonString);
                file.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error saving game file: " + ex.Message);
                success = false;
            }
            return success;
        }

        public bool LoadGame(GameData gameData)
        {
            bool success = true;
            try
            {
                string saveGamePath = GetSaveGameFilePath();
                var file = new StreamReader(saveGamePath);
                string jsonString = file.ReadToEnd();
                file.Close();
                JObject jObject = JObject.Parse(jsonString);
                gameData.Deserialize(jObject);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading game file: " + ex.Message);
                success = false;
            }
            return success;
        }
    }
}

