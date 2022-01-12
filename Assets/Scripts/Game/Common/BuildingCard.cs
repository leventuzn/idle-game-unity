using UnityEngine;
using UnityEngine.UI;
using Game.Models;

namespace Game.Common
{
    public class BuildingCard : MonoBehaviour
    {
        [SerializeField]
        private BuildingSO _buildingSO;

        [SerializeField]
        private Text _nameText;

        [SerializeField]
        private Image _buildingImage;

        [SerializeField]
        private GameObject _cost;

        public BuildingSO BuildingSO
        {
            get
            {
                return _buildingSO;
            }
            set
            {
                _buildingSO = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _nameText.text = _buildingSO.buildingName;
            _buildingImage.sprite = _buildingSO.image;
            _cost.transform.Find("GoldCost").Find("Text").GetComponent<Text>().text = _buildingSO.cost.goldCost.ToString();
            _cost.transform.Find("GemCost").Find("Text").GetComponent<Text>().text = _buildingSO.cost.gemCost.ToString();

            if (_buildingSO.cost.gemCost <= 0)
            {
                _cost.transform.Find("GemCost").gameObject.SetActive(false);
                _cost.transform.Find("Seperator").gameObject.SetActive(false);
            }
            else if (_buildingSO.cost.goldCost <= 0)
            {
                _cost.transform.Find("GoldCost").gameObject.SetActive(false);
                _cost.transform.Find("Seperator").gameObject.SetActive(false);
            }
        }
    }
}

