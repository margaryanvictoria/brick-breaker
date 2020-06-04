using UnityEngine;

// This forces the MusicPlayer component to also have attached to it
// an AudioSource component . . .
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer> {
    /*
    public static void Play(AudioClip clip) {
        //AudioSource is required cause of [ReqComp], so we just take the guaranteed
        //AudioSource for whatever Singleton game object has this MusicPlayer script
        var audioSource = Instance.GetComponent<AudioSource>();

        // We set the clip to play based on the clip passed in the parameter . . .
        audioSource.clip = clip;

        // We play the clip on the AudioSource . . .
        audioSource.Play();
    }
    */

    public static void Play(AudioClip clip) {
        MusicPlayer.Play(clip, false);
    }

    public static void Play(AudioClip clip, bool loop) {
        var audioSource = Instance.GetComponent<AudioSource>();

        //if the clip is already playing, don't reset it to the beginning
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.loop = loop;

        audioSource.Play();
    }
}
