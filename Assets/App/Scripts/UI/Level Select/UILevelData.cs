using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class UILevelData : MonoBehaviour {
    [SerializeField]
    private Text levelName;
    [SerializeField]
    private Text highscore;
    [SerializeField]
    private Image icon;

    public void SetLevelData(LevelDatabase.LevelData levelData) {
        this.levelName.text = levelData.Name;
        this.icon.sprite = levelData.Icon;

        var score = DataManager.GameData.GetScore(levelData.Name); 
        if (score == null) {
            this.highscore.text = "Highscore: --";
        } else {
            this.highscore.text = "Highscore: " + score.Value;
        }

        var button = this.GetComponent<Button>();
        button.interactable = false;

        var iterator = DataManager.GameData.LevelsUnlocked.GetEnumerator();

        while (iterator.MoveNext()) {
            var levelName = iterator.Current;

            if(string.Compare(levelData.Name, levelName) == 0) {
                button.interactable = true;

                button.onClick.AddListener(() => {
                    SceneManager.LoadScene(levelData.Name);
                });

                break;
            }
        }
    }

}
