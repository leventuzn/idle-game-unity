using System;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// This class handles the gold system in the game. It is used to manage golds and other classes can subscribe to it.
    /// </summary>
    public class GoldSystem : MonoBehaviour
    {
        private Action<int> onGoldsChanged;

        public void AddGold(int value)
        {
            var prevGolds = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds;
            var newGolds = prevGolds + value;

            if (onGoldsChanged != null)
            {
                onGoldsChanged(newGolds);
            }

            GameDataManager.Instance.UpdateGoldValue(newGolds);
        }

        public void SpendGold(int value)
        {
            var prevGolds = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds;
            var newGolds = prevGolds - value;

            if (onGoldsChanged != null)
            {
                onGoldsChanged(newGolds);
            }

            GameDataManager.Instance.UpdateGoldValue(newGolds);
        }

        public void ResetGoldUI()
        {
            var golds = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds;
            if (onGoldsChanged != null)
            {
                onGoldsChanged(golds);
            }
        }

        /// <summary>
        /// Registers the specified callback to be called when the amount of golds changes.
        /// </summary>
        /// <param name="callback">The callback to register.</param>
        public void Subscribe(Action<int> callback)
        {
            onGoldsChanged += callback;
        }

        /// <summary>
        /// Unregisters the specified callback to be called when the amount of golds changes.
        /// </summary>
        /// <param name="callback">The callback to unregister.</param>
        public void Unsubscribe(Action<int> callback)
        {
            if (onGoldsChanged != null)
            {
                onGoldsChanged -= callback;
            }
        }
    }
}

