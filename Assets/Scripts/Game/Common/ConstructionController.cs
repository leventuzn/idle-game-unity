using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Models;

namespace Game.Common
{
    public class ConstructionController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _shapeArea;

        [SerializeField]
        private GameObject _image;

        [SerializeField]
        private GameObject _progressBar;

        private List<GameObject> _shapeAreaTiles;

        private List<GameObject> _constructionAreaTiles;

        private bool _isAreaEmpty;

        

        public void BeginDrag()
        {
            UnderConstruction(true);
            SetShape(GetComponent<Building>().shape);
            _shapeAreaTiles = new List<GameObject>();
            for (int i = 0; i < _shapeArea.transform.childCount; i++)
            {
                var fieldObject = _shapeArea.transform.GetChild(i).gameObject;
                if (fieldObject.activeSelf)
                {
                    _shapeAreaTiles.Add(fieldObject);
                }
            }
        }

        public void UpdateState()
        {
            _isAreaEmpty = true;
            _constructionAreaTiles = new List<GameObject>();
            foreach (var tile in _shapeAreaTiles)
            {
                if (tile.GetComponent<CollisionDetector>().collisionStatus == CollisionStatus.BuildingTile)
                {
                    _isAreaEmpty = false;
                    break;
                }
                else if (tile.GetComponent<CollisionDetector>().collisionStatus == CollisionStatus.Null)
                {
                    _isAreaEmpty = false;
                    break;
                }
                _constructionAreaTiles.Add(tile.GetComponent<CollisionDetector>().colliderObject);
            }

            Color color = _isAreaEmpty ? Color.green : Color.red;
            color.a = 0.5f;
            foreach (var tile in _shapeAreaTiles)
            {
                tile.GetComponent<Image>().color = color;
            }
        }

        public void Construct()
        {
            if (_isAreaEmpty)
            {
                GetComponent<Building>().CreateAndUpdateData();
                UnderConstruction(false);
                SetPosition();
                foreach (var tile in _constructionAreaTiles)
                {
                    var x = tile.GetComponent<Tile>().x;
                    var y = tile.GetComponent<Tile>().y;
                    GameManager.Instance.UpdateTileAt(x, y);
                }
                _progressBar.GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gold, -GetComponent<Building>().cost.goldCost, _progressBar.transform.position);
                GameManager.Instance.goldSystem.SpendGold(GetComponent<Building>().cost.goldCost);
                _progressBar.GetComponent<AnimationController>().FloatingTextAnimation(ResourceType.Gem, -GetComponent<Building>().cost.gemCost, _progressBar.transform.position);
                GameManager.Instance.gemSystem.SpendGem(GetComponent<Building>().cost.gemCost);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void SetShape(BuildingShape shape)
        {
            var activeFields = 0;
            switch (shape)
            {
                case BuildingShape.ShapeTwo:
                    activeFields = 2;
                    break;
                case BuildingShape.ShapeThree:
                    activeFields = 3;
                    break;
                case BuildingShape.ShapeFour:
                    activeFields = 4;
                    break;
                case BuildingShape.ShapeFive:
                    activeFields = 5;
                    break;
            }

            for (int i = _shapeArea.transform.childCount - 1; i >= activeFields; i--)
            {
                _shapeArea.transform.GetChild(i).gameObject.SetActive(false);
            }

            SetFieldAlignment(activeFields);
        }

        private void SetFieldAlignment(int activeFields)
        {
            if (activeFields == 5)
            {
                _shapeArea.GetComponent<GridLayoutGroup>().constraintCount = 3;
                _shapeArea.GetComponent<GridLayoutGroup>().startCorner = GridLayoutGroup.Corner.UpperLeft;
            }
            else
            {
                _shapeArea.GetComponent<GridLayoutGroup>().constraintCount = 2;
                _shapeArea.GetComponent<GridLayoutGroup>().startCorner = GridLayoutGroup.Corner.LowerLeft;
            }
        }

        private void SetPosition()
        {
            var sumPositions = Vector3.zero;
            foreach (var tile in _constructionAreaTiles)
            {
                sumPositions += tile.transform.position;
            }
            var centerPosition = sumPositions / _constructionAreaTiles.Count;
            transform.position = centerPosition;
        }

        public void UnderConstruction(bool underConstruction)
        {
            _image.SetActive(underConstruction);
            _shapeArea.SetActive(underConstruction);
            _progressBar.SetActive(!underConstruction);
        }
    }
}

