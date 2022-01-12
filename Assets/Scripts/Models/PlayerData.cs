using Newtonsoft.Json.Linq;

/// <summary>
/// Model class to store and manage player data.
/// </summary>
public class PlayerData
{
    public const string TokenGolds = "Golds";
    public const string TokenGems = "Gems";

    public int m_Golds;
    public int m_Gems;

    public void Reset()
    {
        m_Golds = 10;
        m_Gems = 10;
    }

    public JObject Serialize()
    {
        JObject data = new JObject();
        data.Add(TokenGolds, m_Golds);
        data.Add(TokenGems, m_Gems);
        return data;
    }

    public void Deserialize(JObject playerDataJson)
    {
        if (playerDataJson[TokenGolds] != null) { m_Golds = playerDataJson[TokenGolds].Value<int>(); }
        if (playerDataJson[TokenGems] != null) { m_Gems = playerDataJson[TokenGems].Value<int>(); }
    }
}
