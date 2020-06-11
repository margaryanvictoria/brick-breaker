using UnityEngine;
using System.Collections;

// This forces the MusicPlayer component to also have attached to it
// an AudioSource component . . .
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoSingleton<MusicPlayer> {
    private AudioSource current;
    private AudioSource next;
    //public AudioSource temp;

    private void Awake() {
        this.current = this.GetComponent<AudioSource>(); //get our audio source component

        var go = new GameObject("Next"); //create a next object, so we can switch to the next clip
        go.transform.SetParent(this.transform); //set it as a child of our singleton music player

        this.next = go.AddComponent<AudioSource>(); //also make it an audio source
    }

    public static void Play(AudioClip clip) {
        // Play clip, with looping set to false . . .
        MusicPlayer.Play(clip, false);
    }

    public static void Play(AudioClip clip, bool loop) {
        var audioSource = Instance.GetComponent<AudioSource>();

        //if the clip is already playing, don't reset it to the beginning
        //if (audioSource.clip == clip) return;
        /*
        audioSource.clip = clip;
        audioSource.loop = loop;

        audioSource.Play();
        */

        Instance.StartCoroutine(Instance._DoPlay(clip, loop));
    }

    // Original -------------------------------------------------------------
    
    IEnumerator _DoPlay(AudioClip clip, bool loop) {
        //if we don't have audio playing, set the Track Player's clip to play
        if (this.current.isPlaying == false) {
            this.current.clip = clip;
            this.current.loop = loop;
            this.current.Play();

            yield break; // Forces the coroutine to terminate . . .
        }

        //StopCoroutine("_Fade");
        // Tell the current Audio Source to fade its volume . . .
        //from its current volume, all the way to 0
        StartCoroutine(this._Fade(this.current, this.current.volume, 0.0f, 1.0f));

        // Assign the new clip to next, and fade it in . . .
        this.next.clip = clip;
        this.next.loop = loop;
        this.next.Play();

        // Wait until this routine has finished . . .
        yield return StartCoroutine(this._Fade(this.next, 0.0f, 1.0f, 5.0f));

        // Once the next clip has fully faded in, swap the next to current . . .
        var temp = this.current;
        this.current = this.next;
        this.next = temp;

        // Reset next . . .
        this.next.Stop();
        this.next.clip = null;
        this.next.volume = 0.0f;

        yield return null;
    }

    IEnumerator _Fade(AudioSource source, float from, float to, float time) {
        var eTime = 0.0f; // Elapsed time . . .
        var progress = 0.0f; // Progress . . .
        
        while(source.volume <= 1.0f) {
            progress = eTime / time; // Normalize the time . . .

            var volume = Mathf.Lerp(from, to, progress);

            source.volume = volume;

            //eTime += Time.deltaTime;
            //eTime += 0.005f;
            eTime += Time.unscaledDeltaTime;
            
            //yield return new WaitForEndOfFrame(); // Waits until the entire render frame has completed . . .
            yield return null;
        }
    }
    

    /*
    IEnumerator _DoPlay(AudioClip clip, bool loop) {
        //if the scene's track is not playing, play it
        if (this.current.isPlaying == false) {
            this.current.clip = clip;
            this.current.loop = loop;
            this.current.Play();
            yield break; // Forces the coroutine to terminate . . .
        }
        temp = current;
        this.next.clip = clip;
        this.next.loop = loop;
        this.next.Play();
        //else start a cross fade between this clip and whatever the next scene's track is
        yield return StartCoroutine(this._CrossFade(temp, next, 3.0f));
        //yield return null;
    }

    IEnumerator _CrossFade(AudioSource a, AudioSource b, float time) {
        var eTime = 0.0f; // Elapsed time . . .
        var progress = 0.0f; // Progress . . .
        //need to lerp for clip a and b

        while (a.volume <= 1.0f) {
            progress = eTime / time; // Normalize the time . . .
            var volumeA = Mathf.Lerp( , progress);

            yield return null;
    }
    */

    // ---------------------------------------------------------------

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

}
