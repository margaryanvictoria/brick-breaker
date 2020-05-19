using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public Text Score;

    private void Awake() {
        GameManager.onScoreUpdated += OnScoreUpdated;
    }
    private void OnScoreUpdated(int score) {
        this.Score.text = "score: " + score;
    }
}
