using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string ScoreTitle = "Best Score: ";

    [Serializable]
    public class SaveData
    {
        public string playerName;
        public int highScore;
    }

    public SaveData lastSavedData;

    public string currentPlayer;


    public static GameManager Instance;

    //Verifies if no other instances are baing created.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }

    //Loads game data from persistent path if such exist - otherwise, create new empty data.
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            lastSavedData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            lastSavedData = new SaveData();
        }
    }

    //Tries to save new data, if score i greather than the last high score.
    public void SaveGameData(int newPlayerScore)
    {
        if(newPlayerScore> lastSavedData.highScore)
        {
            lastSavedData.highScore = newPlayerScore;
            lastSavedData.playerName = currentPlayer;

            string json = JsonUtility.ToJson(lastSavedData);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    //Returns phrase with high score
    public string GetBestScoreText()
    {
        return $"{ScoreTitle}{Instance.lastSavedData.playerName} : {Instance.lastSavedData.highScore}";
    }
}
