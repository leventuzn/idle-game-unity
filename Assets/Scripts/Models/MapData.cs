using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Game.Common;

/// <summary>
/// Model class to store and manage map data.
/// </summary>
public class MapData
{
    public const string TokenTileStatus = "TileStatus";
    public const string TokenBuildings = "Buildings";

    public bool[] m_TileStatus; // Holds status of the tiles. True means the tile is occupied by a building and false means tile is empty.
    public List<BuildingData> m_Buildings;
    public void Reset()
    {
        m_TileStatus = new bool[100];
        m_Buildings = new List<BuildingData>();
    }

    public BuildingData GetBuildingData(int buildingId)
    {
        BuildingData data = null;
        for (int i = 0; i < m_Buildings.Count; i++)
        {
            var building = m_Buildings[i];
            if (building.m_Id == buildingId)
            {
                data = building;
                break;
            }
        }
        return data;
    }

    public void UpdateBuildingData(BuildingData buildingData)
    {
        BuildingData data = GetBuildingData(buildingData.m_Id);
        if (data == null)
        {
            m_Buildings.Add(buildingData);
        }
        else
        {
            data.m_Building = buildingData.m_Building;
            data.m_RemainingGenerationDuration = buildingData.m_RemainingGenerationDuration;
        }
    }

    public JObject Serialize()
    {
        JObject data = new JObject();
        JArray jArray = new JArray();
        SortBuildingData();
        foreach(BuildingData buildingData in m_Buildings)
        {
            jArray.Add(buildingData.Serialize());
        }
        data.Add(TokenBuildings, jArray);
        data.Add(TokenTileStatus, JsonConvert.SerializeObject(m_TileStatus));
        return data;
    }

    public void Deserialize(JObject mapDataJson)
    {
        if(mapDataJson[TokenBuildings] != null)
        {
            BuildingData buildingData = null;
            foreach(JObject data in mapDataJson[TokenBuildings])
            {
                buildingData = new BuildingData();
                buildingData.Deserialize(data);
                m_Buildings.Add(buildingData);
            }
        }
        if (mapDataJson[TokenTileStatus] != null) { m_TileStatus = JsonConvert.DeserializeObject<bool[]>(mapDataJson[TokenTileStatus].Value<string>()); }
    }

    private void SortBuildingData()
    {
        List<BuildingData> buildings = m_Buildings.GroupBy(s => s.m_Building).Select(x => x.First()).ToList();
        m_Buildings = buildings;
    }
}

