using UnityEngine;

public class TrackSelector : MonoBehaviour {
    public AudioClip clip; // This is the clip to be played on the MusicPlayer . . .
    public bool loop; // This will control whether the clip is looped . . .
    //public float volume;// = 1.0f; //gonna try to lower the volume of tracks that are way too loud


    private void Start() {
        //once a scene starts, we want to call music player and pass it this level's clip
        MusicPlayer.Play(this.clip, loop);
    }

    /*
    private void Start() {
        MusicPlayer.Play(this.clip, loop, volume);
    } 
    */
    
    /*
    //volume slider
    public void Slider() {
        volume = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), volume, 0.0F, 1.0F);
        MusicPlayer.UpdateVolume(this.clip, volume);
    }
    */
}
