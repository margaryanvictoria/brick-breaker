using UnityEngine;

public class TrackSelector : MonoBehaviour {
    public AudioClip clip; // This is the clip to be played on the MusicPlayer . . .
    public bool loop; // This will control whether the clip is looped . . .
    
    private void Start() {
        MusicPlayer.Play(this.clip, loop);
    }
}
