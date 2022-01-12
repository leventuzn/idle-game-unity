using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class GameDataManager
    {
        public GameData m_GameData;

        private static GameDataManager _instance;

        public static GameDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameDataManager();
                }
                return _instance;
            }
        }

        public void Initialize()
        {
            m_GameData = new GameData();
        }

        public void UpdateGoldValue(int value)
        {
            m_GameData.m_PlayerData.m_Golds = value;
        }

        public void UpdateGemValue(int value)
        {
            m_GameData.m_PlayerData.m_Gems = value;
        }

        public void UpdateTileStatus(int x, int y)
        {
            var tileIndex = x + (y * 10);
            m_GameData.m_MapData.m_TileStatus[tileIndex] = true;
        }
    }
}

