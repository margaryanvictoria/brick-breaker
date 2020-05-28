using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public Text Score;

    /*
    private void Awake() {
        GameManager.onScoreUpdated += OnScoreUpdated;
    }*/

    //when an object gets created,
    private void OnEnable() {
        GameManager.onScoreUpdated += OnScoreUpdated;
    }

    //when an object gets destroyed, OnDisable() will be called
    private void OnDisable() {
        // Unsubscribe to events . . .
        GameManager.onScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(int score) {
        this.Score.text = "score: " + score;
    }
}
