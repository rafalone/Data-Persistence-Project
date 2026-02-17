using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    public static DataPersistance Instance;

    public PlayerData PlayerData { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }

    public void SaveBestScore(int score)
    {
        if (PlayerData.BestScore < score)
        {
            PlayerData.BestScore = score;
            SavePlayerData(PlayerData);
        }
    }

    public void SavePlayerName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return;

        PlayerData.PlayerName = name;

        SavePlayerData(PlayerData);
    }

    private void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        PlayerData data = new();

        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(json);
        }

        PlayerData = data;
    }
}

[System.Serializable]
public class PlayerData
{
    public string PlayerName;
    public int BestScore;
}