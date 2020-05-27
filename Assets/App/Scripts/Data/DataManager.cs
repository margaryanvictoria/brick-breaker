using UnityEngine;

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

    }
}
