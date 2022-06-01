using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Handles the name. Not much use for it other than a cheeky mention on game over/game win.

public class DataController : MonoBehaviour
{
    public static DataController instance;

    // All scripts can read the playerName, but it can be changed only by using the SetName() script below.
    private string _playerName;
    public string playerName
    {
        get { return _playerName; }
        private set { _playerName = value; }
    } // ENCAPSULATION

    void Awake()
    {
        DataSingleton(); // ABSTRACTION
    }

    private void DataSingleton()
    {
        // Singleton script, ensures only one instance of this exists at a time.

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }

    [System.Serializable]
    public class SaveData
    {
        public string savedName;
    }

    // This should be the only way you can set the name (aside from loading the Json). Also saves it automatically.
    public void SetName(string name)
    {
        playerName = name;
        SaveName();
    }

    // Save and load the name to json
    private void SaveName()
    {
        SaveData data = new SaveData();
        data.savedName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savename.json", json);
    }

    private void LoadName()
    {
        string path = Application.persistentDataPath + "/savename.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.savedName;
        }
    }

}
