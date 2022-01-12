using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Models;

namespace Game.Common
{
    /// <summary>
    /// Model class to store and manage building data.
    /// </summary>
    public class BuildingData
    {
        public const string TokenId = "Id";
        public const string TokenBuilding = "Building";
        public const string TokenRemainingGenerationDuration = "RemainingGenerationDuration";

        public int m_Id;

        public Building m_Building;

        public float m_RemainingGenerationDuration;

        public void Reset()
        {
            m_Id = 0;
            m_Building = new Building();
            m_RemainingGenerationDuration = 0;
        }

        public JObject Serialize()
        {
            JObject data = new JObject();
            data.Add(TokenId, m_Id);
            data.Add(TokenBuilding, JsonConvert.SerializeObject(m_Building));
            data.Add(TokenRemainingGenerationDuration, m_RemainingGenerationDuration);
            return data;
        }

        public void Deserialize(JObject buildingDataJson)
        {
            if (buildingDataJson[TokenId] != null) { m_Id = buildingDataJson[TokenId].Value<int>(); }
            if (buildingDataJson[TokenBuilding] != null) { m_Building = JsonConvert.DeserializeObject<Building>(buildingDataJson[TokenBuilding].Value<string>()); }
            if (buildingDataJson[TokenRemainingGenerationDuration] != null) { m_RemainingGenerationDuration = buildingDataJson[TokenRemainingGenerationDuration].Value<float>(); }
        }
    }
}

