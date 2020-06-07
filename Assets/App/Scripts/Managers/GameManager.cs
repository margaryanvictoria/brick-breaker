using UnityEngine;

public class GameManager : MonoBehaviour {
    public static event System.Action<int> onScoreUpdated;
    
    private int score = 0;
    private Brick[] bricks = null;
    private int brickCount = 0;

    private void Start() {
        Time.timeScale = 1.0F; // Normal time . . .
        //fill our array of all the objects we can find in the level, of type Brick
        this.bricks = Object.FindObjectsOfType<Brick>();

        for(int i=0; i<this.bricks.Length; ++i) {
            var brick = this.bricks[i];

            brick.onHit += this.OnHit;
            brick.onDestroyed += this.OnBrickDestroyed;

            /*
            //+= subscribes to an event, -+ unsubscribes
            //b for brick
            brick.onHit += (b) => {
                // this is an anonymous function.
                this.score += 1;
            }; 

            brick.onDestroyed += (b) => {
                this.score += 2;
            };*/
        }
        this.brickCount = this.bricks.Length;

        GameManager.onScoreUpdated?.Invoke(this.score = 0);
    }

    private void OnHit(Brick brick) {
        AddToScore(1);
    }

    private void OnBrickDestroyed(Brick brick) {
        brick.onHit -= this.OnHit; // unsubscribe from OnHit . . .
        brick.onDestroyed -= this.OnBrickDestroyed; // unsubscribe

        this.brickCount--;

        AddToScore(2);
        
        if(this.brickCount <= 0) {
            this.OnGameOver();
        }
    }

    private void OnGameOver() {
        Debug.Log("Nice, you won!");
        PlayerPrefs.SetInt("Level 1", this.score); // current session . . .
        // attempt to store the highest score for the long term
        DataManager.GameData.SetScore("Level 1", new Score(this.score, System.DateTime.Now));
        DataManager.SaveGameData();

        var asset = Resources.Load<GameOver>("Game Over");
        var clone = GameObject.Instantiate(asset.gameObject);
        //var gameOver = clone.GetComponent<GameOver>(); //unneeded

        Time.timeScale = 0.001f; // Slow down/Pause . . .
    }

    private void AddToScore(int amount) {
        this.score += amount;
        GameManager.onScoreUpdated?.Invoke(this.score);
    }

    private void OnDestroy() {
        Time.timeScale = 1.0f;
    }
}
