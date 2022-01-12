using UnityEngine;
using Game.Core;
using Game.Common;

namespace Game.UI
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _buildingCardPrefab;

        [SerializeField]
        private GameObject _grid;

        // Start is called before the first frame update
        void Start()
        {
            InitializeBuildingCardDatas();
            GameManager.Instance.goldSystem.Subscribe(OnGoldsChanged);
            GameManager.Instance.gemSystem.Subscribe(OnGemsChanged);
        }

        private void OnDestroy()
        {
            GameManager.Instance.goldSystem.Unsubscribe(OnGoldsChanged);
            GameManager.Instance.gemSystem.Unsubscribe(OnGemsChanged);
        }

        private void InitializeBuildingCardDatas()
        {
            var golds = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds;
            var gems = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems;
            foreach (var data in GameManager.Instance.buildingCardDatas)
            {
                var buildingCard = Instantiate(_buildingCardPrefab, _grid.transform);
                buildingCard.name = _buildingCardPrefab.name;
                buildingCard.GetComponent<BuildingCard>().BuildingSO = data;
                if (buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.goldCost > golds || buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.gemCost > gems)
                {
                    buildingCard.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Called when the number of golds changes.
        /// </summary>
        /// <param name="numStars">The current number of golds.</param>
        private void OnGoldsChanged(int numGolds)
        {
            var gems = GameDataManager.Instance.m_GameData.m_PlayerData.m_Gems;
            for (int i = 0; i < _grid.transform.childCount; i++)
            {
                var buildingCard = _grid.transform.GetChild(i);
                if (buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.goldCost <= numGolds && buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.gemCost <= gems)
                {
                    buildingCard.gameObject.SetActive(true);
                }
                else
                {
                    buildingCard.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Called when the number of gems changes.
        /// </summary>
        /// <param name="numStars">The current number of gems.</param>
        private void OnGemsChanged(int numGems)
        {
            var golds = GameDataManager.Instance.m_GameData.m_PlayerData.m_Golds;
            for (int i = 0; i < _grid.transform.childCount; i++)
            {
                var buildingCard = _grid.transform.GetChild(i);
                if (buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.gemCost <= numGems && buildingCard.GetComponent<BuildingCard>().BuildingSO.cost.goldCost <= golds)
                {
                    buildingCard.gameObject.SetActive(true);
                }
                else
                {
                    buildingCard.gameObject.SetActive(false);
                }
            }
        }
    }
}

