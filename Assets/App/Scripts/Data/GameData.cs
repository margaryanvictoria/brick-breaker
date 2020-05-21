using UnityEngine;
using System.Collections.Generic;

public class GameData : System.Object {
    private Dictionary<string, Score> scores = new Dictionary<string, Score>();
    // <key, value>
    // key - the ID to be used to look up a particular record
    // value - the data placed into a record that is tied to a unique id

    /// <summary>
    /// Sets the score for a particular level.
    /// </summary>
    /// <param name="levelName"> The level name. </param>
    /// <param name="score"> The score achieved in a session. </param>
    /// <returns> True if a new high score was achieved, else false. </returns>
    public bool SetScore(string levelName, Score score) {
        // we have not yet kept track of this level . . .
        if(this.scores.ContainsKey(levelName) == false) {
            this.scores.Add(levelName, score);
            return true;
        }
        // else, we know we are keeping track of this level . . .
        // let's compare the stored score with the new one . . .
        var lastScore = this.scores[levelName];

        if(score.Value > lastScore.Value) {
            this.scores[levelName] = score;
            return true;
        }

        return false;
    }

}

public class Score : System.Object {
    private int value;
    private System.DateTime date;

    public Score(int value, System.DateTime date) {
        this.value = value;
        this.date = date;
    }

    // C# properties . . .
    public int Value {
        get {
            return this.value;
        } set {
            this.value = value;
        }
    }

    public System.DateTime Date {
        get {
            return this.date;
        } set {
            this.date = value;
        }
    }
}