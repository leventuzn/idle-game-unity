using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Common;
using Game.Models;
using TMPro;

namespace Game.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Transform _bar;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private GameObject _floatingTextPrefab;

        [SerializeField]
        private Building _building;

        private float _remainingTime;

        // Start is called before the first frame update
        void Start()
        {   
            _remainingTime = GameDataManager.Instance.m_GameData.m_MapData.GetBuildingData(_building.id).m_RemainingGenerationDuration;
            var scaleFactor = (_building. resourceGenerationDuration - _remainingTime) / _building.resourceGenerationDuration;
            _bar.localScale = new Vector3(scaleFactor, 1f);
            _text.text = $"{_remainingTime}s";
            GameManager.Instance.countdownSystem.Subscribe(OnTick);
        }

        private void OnDestroy()
        {
            GameManager.Instance.countdownSystem.Unsubscribe(OnTick);
        }

        private void OnTick()
        {
            _remainingTime--;
            if (_remainingTime <= 0)
            {
                _remainingTime = GameDataManager.Instance.m_GameData.m_MapData.GetBuildingData(_building.id).m_RemainingGenerationDuration;
                var goldsGenerated = _building.generatedResources.goldsGenerated;
                var gemsGenerated = _building.generatedResources.gemsGenerated;
                if(goldsGenerated > 0)
                {
                    GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gold, goldsGenerated, transform.position);
                    GameManager.Instance.goldSystem.AddGold(goldsGenerated);
                }
                if(gemsGenerated > 0)
                {
                    GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gem, gemsGenerated, transform.position);
                    GameManager.Instance.gemSystem.AddGem(gemsGenerated);
                }
            }
            var scaleFactor = (_building.resourceGenerationDuration - _remainingTime) / _building.resourceGenerationDuration;
            _bar.localScale = new Vector3(scaleFactor, 1f);
            _text.text = $"{_remainingTime}s";
        }
    }
}

