using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Common
{
    public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private GameObject _buildingPrefab;

        private GameObject _building;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var buildings = FindObjectOfType<Canvas>().transform.Find("Map").Find("Buildings");
            _building = Instantiate(_buildingPrefab, buildings.transform);
            _building.name = _buildingPrefab.name;
            _building.transform.SetAsLastSibling();
            _building.GetComponent<RectTransform>().position = eventData.position;
            _building.GetComponent<Building>().SetData(GetComponent<BuildingCard>().BuildingSO);
            _building.GetComponent<ConstructionController>().BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var canvas = FindObjectOfType<Canvas>();
            _building.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
            _building.GetComponent<ConstructionController>().UpdateState();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _building.GetComponent<ConstructionController>().Construct();
        }
    }
}

