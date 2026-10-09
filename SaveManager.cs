
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public int currentLevel = 1;
    public float playerHealth = 100f;
    public float hunger = 100f;
    public float thirst = 100f;
    public float stamina = 100f;
    public float playerX;
    public float playerY;
    public float playerZ;
}

public class SaveManager : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform player;
    [SerializeField] private SurvivalStats survivalStats;

    [Header("Progress")]
    [SerializeField, Min(1)] private int currentLevel = 1;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath,
                     "earth_survival_save.json");

    public void SaveGame()
    {
        GameSaveData data = new GameSaveData();

        data.currentLevel = currentLevel;

        if (player != null)
        {
            Vector3 position = player.position;
            data.playerX = position.x;
            data.playerY = position.y;
            data.playerZ = position.z;
        }

        if (survivalStats != null)
        {
            data.playerHealth = survivalStats.Health;
            data.hunger = survivalStats.Hunger;
            data.thirst = survivalStats.Thirst;
            data.stamina = survivalStats.Stamina;
        }

        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);

            Debug.Log("Game saved successfully.");
        }
        catch (Exception exception)
        {
            Debug.LogError("Save failed: " + exception.Message);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found.");
            return;
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            GameSaveData data =
                JsonUtility.FromJson<GameSaveData>(json);

            if (data == null)
            {
                Debug.LogError("Save data is invalid.");
                return;
            }

            currentLevel = Mathf.Max(1, data.currentLevel);

            if (player != null)
            {
                player.position = new Vector3(
                    data.playerX,
                    data.playerY,
                    data.playerZ
                );
            }

            if (survivalStats != null)
            {
                survivalStats.SetStats(
                    data.playerHealth,
                    data.hunger,
                    data.thirst,
                    data.stamina
                );
            }

            Debug.Log("Game loaded successfully.");
        }
        catch (Exception exception)
        {
            Debug.LogError("Load failed: " + exception.Message);
        }
    }

    public void DeleteSave()
    {
        try
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);

            Debug.Log("Save data deleted.");
        }
        catch (Exception exception)
        {
            Debug.LogError("Delete failed: " + exception.Message);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = Mathf.Max(1, level);
    }
}
