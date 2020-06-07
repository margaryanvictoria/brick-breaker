using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "App/Database/Create Level Database")]
public class LevelDatabase : ScriptableObject {
    [SerializeField]
    private LevelData[] levels = new LevelData[0];

    public IEnumerable<LevelData> Levels {
        get {
            return this.levels;
        }
    }

    [System.Serializable]
    public class LevelData : System.Object {
        [SerializeField]
        private string name;
        [SerializeField]
        private Sprite icon;

        public string Name {
            get {
                return this.name;
            }
        }

        public Sprite Icon {
            get {
                return this.icon;
            }
        }

    }
}
