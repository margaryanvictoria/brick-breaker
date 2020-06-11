using UnityEngine;
using Newtonsoft.Json; // this is how we will serialize to JSON . . .
using System.IO;

public class DataManager : MonoSingleton<DataManager> {
    private GameData gameData = null;
    
    public static GameData GameData {
        get {
            //if we dont have a datamanager, return null
            if (DataManager.Instance == null) return null;

            //if we dont have a gameData, load or make one
            if (DataManager.Instance.gameData == null) {
                // load it from disc . . .
                //DataManager.Instance.gameData = new GameData();
                DataManager.LoadGameData();

                DataManager.Instance.gameData.Validate();
            }

            return DataManager.Instance.gameData;
        }
    }

    private static void LoadGameData() {
        var data_folder = DataManager.GetDataFolder();

        var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";

        // Check if the file for GameData.json exists . . .
        if (File.Exists(path) == false) {
            // It does not exist so create a brand new instance . . .
            DataManager.Instance.gameData = new GameData();
            // Now save it to disc . . . 
            DataManager.SaveGameData();
            // We are done . . .
            return;
        }

        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
            using (var reader = new StreamReader(stream)) {
                //read the file and store the data in 'json'
                var json = reader.ReadToEnd();

                //read the json and cast it to a generic object, but we convert it to <GameData> type
                //this is the same as typecasting it as such: (GameData) JsonConvert..., but that's not safe,
                //aka, possible data loss. Then we need to save it as the gameData
                DataManager.Instance.gameData = JsonConvert.DeserializeObject<GameData>(json);
            }
        }
    }

    public static void SaveGameData() {
        var data_folder = DataManager.GetDataFolder();

        Debug.Log("Data Folder: " + data_folder);

        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;

        string json = JsonConvert.SerializeObject(DataManager.GameData, settings);

        Debug.Log(json); //debug the json scores to console

        var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";

        // Creates or Opens the file . . .
        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) {
            // Creates a writable stream . . .
            using (var writer = new StreamWriter(stream)) {
                // Writes a string to the stream . . .
                writer.WriteLine(json);
            }
        }
    }

    private static string GetDataFolder() {
#if UNITY_EDITOR
        var location = Application.dataPath;
#else
        var location = Application.persistentDataPath;
#endif
        // Linux/UNIX: Directories are / separated.
        // Windows: Directories are \ separated.
        Debug.Log("Location: " + location);

        var data_folder = location + Path.DirectorySeparatorChar + "Data";

        // If the folder does not exist . . .
        if (Directory.Exists(data_folder) == false) {
            // Create it . . .
            Directory.CreateDirectory(data_folder);
        }

        return data_folder;
    }
}
