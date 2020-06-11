using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text score; // The Last Session Score . . .
    public Text best; // The overall highest score . . .
    //public Button onRetry, onQuit;

    private void Start() {
        var gameData = DataManager.GameData;

        //var score = gameData.GetScore("Level 1");
        var score = gameData.GetScore(SceneManager.GetActiveScene().name);

        if (score != null) {
            this.best.text = "Best Score: " + score.Value; // persistent session data . . .
        }

        //this.score.text = "Score: " + PlayerPrefs.GetInt("Level 1"); // the current session . . .
        this.score.text = "Score: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
        //this.onRetry.onClick.AddListener(this.OnRetry); //etc etc
    }

    public void OnRetry() {
        Debug.Log("OnRetry request.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit() {
        Debug.Log("OnQuit request.");
        SceneManager.LoadScene("Main");
    }
}
