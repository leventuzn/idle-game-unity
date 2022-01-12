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

        public Vector3 position;

        public void SetData(BuildingSO buildingData)
        {
            image.sprite = buildingData.image;
            id = GameDataManager.Instance.m_GameData.m_MapData.m_Buildings.Count;
            cost = buildingData.cost;
            resourceGenerationDuration = buildingData.resourceGenerationDuration;
            generatedResources = buildingData.generatedResources;
            shape = buildingData.shape;
        }

        public void LoadData(BuildingData buildingData)
        {
            id = buildingData.m_Id;
            image.sprite = buildingData.m_Building.image.sprite;
            cost = buildingData.m_Building.cost;
            resourceGenerationDuration = buildingData.m_Building.resourceGenerationDuration;
            generatedResources = buildingData.m_Building.generatedResources;
            shape = buildingData.m_Building.shape;
            position = buildingData.m_Building.position;
        }

        public void CreateAndUpdateData()
        {
            var buildingData = new BuildingData();
            buildingData.m_Id = id;
            buildingData.m_Building = GetComponent<Building>();
            buildingData.m_RemainingGenerationDuration = resourceGenerationDuration;
            GameDataManager.Instance.m_GameData.m_MapData.UpdateBuildingData(buildingData);
        }


    }
}

