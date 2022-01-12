using System;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// This class handles the gem system in the game. It is used to manage gems and other classes can subscribe to it.
    /// </summary>
    public class GemSystem : MonoBehaviour
    {
        private Action<int> onGemsChanged;

        public void AddGem(int value)
        {
            var prevGems = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems;
            var newGems = prevGems + value;

            if (onGemsChanged != null)
            {
                onGemsChanged(newGems);
            }

            GameDataManager.Instance.UpdateGemValue(newGems);
        }

        public void SpendGem(int value)
        {
            var prevGems = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems;
            var newGems = prevGems - value;

            if (onGemsChanged != null)
            {
                onGemsChanged(newGems);
            }

            GameDataManager.Instance.UpdateGemValue(newGems);
        }

        public void ResetGemUI()
        {
            var gems = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems;
            if (onGemsChanged != null)
            {
                onGemsChanged(gems);
            }
        }

        /// <summary>
        /// Registers the specified callback to be called when the amount of gems changes.
        /// </summary>
        /// <param name="callback">The callback to register.</param>
        public void Subscribe(Action<int> callback)
        {
            onGemsChanged += callback;
        }

        /// <summary>
        /// Unregisters the specified callback to be called when the amount of gems changes.
        /// </summary>
        /// <param name="callback">The callback to unregister.</param>
        public void Unsubscribe(Action<int> callback)
        {
            if (onGemsChanged != null)
            {
                onGemsChanged -= callback;
            }
        }
    }
}

