using UnityEngine;
using Newtonsoft.Json; // this is how we will serialize to JSON . . .
using System.IO;

public class DataManager : Singleton<DataManager> {
    private GameData gameData = null;
    
    public static GameData GameData {
        get {
            if (DataManager.Instance == null) return null;

            if (DataManager.Instance.gameData == null) {
                // load it from disc . . .
                DataManager.Instance.gameData = new GameData();
            }

            return DataManager.Instance.gameData;
        }
    }

    public static void SaveGameData() {
#if UNITY_EDITOR
        var location = Application.dataPath;
#else
        var location = Application.persistentDataPath;
#endif
        // Linux/UNIX: Directories are / separated.
        // Windows: Directories are \ separated.
        Debug.Log("Location: " + location);

        var data_folder = location + Path.DirectorySeparatorChar + "Data";

        Debug.Log("Data Folder: " + data_folder);

        if(Directory.Exists(data_folder) == false) {
            Directory.CreateDirectory(data_folder);
        }
    }
}
