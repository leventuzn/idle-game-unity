using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Models;
using TMPro;

namespace Game.UI
{
    public class ResourcesPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _goldText;

        [SerializeField]
        private Text _gemText;

        [SerializeField]
        private GameObject _floatingTextPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _goldText.text = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds.ToString();
            _gemText.text = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems.ToString();
            GameManager.Instance.goldSystem.Subscribe(OnGoldsChanged);
            GameManager.Instance.gemSystem.Subscribe(OnGemsChanged);
        }

        private void OnDestroy()
        {
            GameManager.Instance.goldSystem.Unsubscribe(OnGoldsChanged);
            GameManager.Instance.gemSystem.Unsubscribe(OnGemsChanged);
        }

        /// <summary>
        /// Called when the number of golds changes.
        /// </summary>
        /// <param name="numStars">The current number of golds.</param>
        private void OnGoldsChanged(int numGolds)
        {
            var amount = numGolds - int.Parse(_goldText.text);
            _goldText.text = numGolds.ToString();
            GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gold, amount, _goldText.transform.position);
        }

        /// <summary>
        /// Called when the number of gems changes.
        /// </summary>
        /// <param name="numStars">The current number of gems.</param>
        private void OnGemsChanged(int numGems)
        {
            var amount = numGems - int.Parse(_gemText.text);
            _gemText.text = numGems.ToString();
            GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gem, amount, _gemText.transform.position);
        }
    }
}

