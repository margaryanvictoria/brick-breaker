using UnityEngine;

//look up the "where T : . . . " stuff
public abstract class Singleton<T> :  MonoBehaviour where T : Singleton<T> {
    private static T instance;
    private static object mutex = new object();

    public static T Instance {
        get {
            // ensures thread safety . . .
            lock(Singleton<T>.mutex) {
                if(Singleton<T>.instance == null) {
                    // Unity check . . . 
                    Singleton<T>.instance = Object.FindObjectOfType<T>();

                    // if this doesn't exist in the scene . . .
                    if(Singleton<T>.instance == null) {
                        // we must create an instance . . .
                        var go = new GameObject("[Singleton] " + typeof(T).Name);

                        Singleton<T>.instance = go.AddComponent<T>();
                    }
                }
            }
            return Singleton<T>.instance;
        }
    }
}
