using UnityEngine;

//look up the "where T : . . . " stuff
public abstract class Singleton<T> :  MonoBehaviour where T : Singleton<T> {
    private static T instance;
    private static object mutex = new object();

    public static T Instance {
        get {
            lock(mutex) {
                if(instance == null) {
                    // we must create an instance . . .
                    var go = new GameObject("[Singleton] " + typeof(T).Name);

                    go.AddComponent<T>();
                }
            }
            return Singleton<T>.instance;
        }
    }
}
