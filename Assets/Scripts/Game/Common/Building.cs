using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Models;

namespace Game.Common
{
    public class Building : MonoBehaviour
    {
        public int id;

        public Image image;

        public Cost cost;

        public float resourceGenerationDuration;

        public GeneratedResources generatedResources;

        public BuildingShape shape;

        public void SetData(BuildingSO buildingSO)
        {
            image.sprite = buildingSO.image;
            cost = buildingSO.cost;
            resourceGenerationDuration = buildingSO.resourceGenerationDuration;
            generatedResources = buildingSO.generatedResources;
            shape = buildingSO.shape;
        }

        public void LoadData(BuildingData buildingData)
        {
            id = buildingData.m_Id;
            resourceGenerationDuration = buildingData.m_ResourceGenerationDuration;
            generatedResources = buildingData.m_GeneratedResources;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(buildingData.m_BuildPositionX, buildingData.m_BuildPositionY);
        }

        public void CreateBuilding()
        {
            id = GameDataManager.Instance.m_GameData.m_MapData.m_Buildings.Count;
            var buildingData = new BuildingData();
            buildingData.m_Id = id;
            buildingData.m_ResourceGenerationDuration = resourceGenerationDuration;
            buildingData.m_GeneratedResources = generatedResources;
            buildingData.m_RemainingGenerationDuration = resourceGenerationDuration;
            buildingData.m_BuildPositionX = GetComponent<RectTransform>().anchoredPosition.x;
            buildingData.m_BuildPositionY = GetComponent<RectTransform>().anchoredPosition.y;
            GameDataManager.Instance.CreateBuildingData(buildingData);
        }
    }
}

