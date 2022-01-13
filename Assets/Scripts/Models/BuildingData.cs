using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Models;

namespace Game.Common
{
    /// <summary>
    /// Model class to store and manage building data.
    /// </summary>
    public class BuildingData
    {
        public const string TokenId = "Id";
        public const string TokenResourceGenerationDuration = "ResourceGenerationDuration";
        public const string TokenGeneratedResources = "GeneratedResources";
        public const string TokenRemainingGenerationDuration = "RemainingGenerationDuration";
        public const string TokenBuildPositionX = "BuildPositionX";
        public const string TokenBuildPositionY = "BuildPositionY";

        public int m_Id;

        public float m_ResourceGenerationDuration;

        public GeneratedResources m_GeneratedResources;

        public float m_RemainingGenerationDuration;

        public float m_BuildPositionX;
        public float m_BuildPositionY;

        public void Reset()
        {
            m_Id = 0;
            m_ResourceGenerationDuration = 0;
            m_GeneratedResources = new GeneratedResources();
            m_RemainingGenerationDuration = 0;
            m_BuildPositionX = 0;
            m_BuildPositionY = 0;
        }

        public JObject Serialize()
        {
            JObject data = new JObject();
            data.Add(TokenId, m_Id);
            data.Add(TokenResourceGenerationDuration, m_ResourceGenerationDuration);
            data.Add(TokenGeneratedResources, JsonConvert.SerializeObject(m_GeneratedResources));
            data.Add(TokenRemainingGenerationDuration, m_RemainingGenerationDuration);
            data.Add(TokenBuildPositionX, m_BuildPositionX);
            data.Add(TokenBuildPositionY, m_BuildPositionY);
            return data;
        }

        public void Deserialize(JObject buildingDataJson)
        {
            if (buildingDataJson[TokenId] != null) { m_Id = buildingDataJson[TokenId].Value<int>(); }
            if (buildingDataJson[TokenResourceGenerationDuration] != null) { m_ResourceGenerationDuration = buildingDataJson[TokenResourceGenerationDuration].Value<int>(); }
            if (buildingDataJson[TokenGeneratedResources] != null) { m_GeneratedResources = JsonConvert.DeserializeObject<GeneratedResources>(buildingDataJson[TokenGeneratedResources].Value<string>()); }
            if (buildingDataJson[TokenRemainingGenerationDuration] != null) { m_RemainingGenerationDuration = buildingDataJson[TokenRemainingGenerationDuration].Value<float>(); }
            if (buildingDataJson[TokenBuildPositionX] != null) { m_BuildPositionX = buildingDataJson[TokenBuildPositionX].Value<float>(); }
            if (buildingDataJson[TokenBuildPositionY] != null) { m_BuildPositionY = buildingDataJson[TokenBuildPositionY].Value<float>(); }
        }
    }
}

