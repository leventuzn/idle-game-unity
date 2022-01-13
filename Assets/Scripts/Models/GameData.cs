using Newtonsoft.Json.Linq;

/// <summary>
/// Model class to store and manage game data.
/// </summary>
public class GameData
{
    public const string TokenPlayerData = "PlayerData";
    public const string TokenMapData = "MapData";

    public PlayerData m_PlayerData;
    public MapData m_MapData;

    public GameData()
    {
        m_PlayerData = new PlayerData();
        m_MapData = new MapData();
        Reset();
    }

    public void Reset()
    {
        m_PlayerData.Reset();
        m_MapData.Reset();
    }

    public JObject Serialize()
    {
        JObject data = new JObject();
        data.Add(TokenPlayerData, m_PlayerData.Serialize());
        data.Add(TokenMapData, m_MapData.Serialize());
        return data;
    }

    public void Deserialize(JObject gameDataJson)
    {
        if (gameDataJson[TokenPlayerData] != null) { m_PlayerData.Deserialize(gameDataJson[TokenPlayerData] as JObject); }
        if (gameDataJson[TokenMapData] != null) { m_MapData.Deserialize(gameDataJson[TokenMapData] as JObject); }
    }
}
