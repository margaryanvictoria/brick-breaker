using UnityEngine;

//look up the "where T : . . . " stuff
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> :  MonoBehaviour where T : MonoSingleton<T> {
    private static T instance;
    private static object mutex = new object();

    public static T Instance {
        get {
            // ensures thread safety . . .
            lock(MonoSingleton<T>.mutex) {
                if(MonoSingleton<T>.instance == null) {
                    // Unity check . . . 
                    MonoSingleton<T>.instance = Object.FindObjectOfType<T>();

                    // if this doesn't exist in the scene . . .
                    if(MonoSingleton<T>.instance == null) {
                        // we must create an instance . . .
                        var go = new GameObject("[Singleton] " + typeof(T).Name);

                        MonoSingleton<T>.instance = go.AddComponent<T>();
                    }
                    // Prevents Singleton component in UNITY from being destroyed when
                    // switching scenes . . .
                    DontDestroyOnLoad(MonoSingleton<T>.instance);
                }
            }
            return MonoSingleton<T>.instance;
        }
    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SOSingleton<T> : ScriptableObject where T : SOSingleton<T> {
    private static T instance;
    private static object mutex = new object();

    public static T Instance {
        get {
            lock (SOSingleton<T>.mutex) {
                if (SOSingleton<T>.instance == null) {
                    var asset = Resources.Load<T>(typeof(T).Name);

                    if(asset == null) {
                        throw new System.NullReferenceException("Asset was not able to be loaded for  " + typeof(T).Name);
                    }

                    SOSingleton<T>.instance = asset; // Save the loaded asset . . .
                }
            }
        return SOSingleton<T>.instance;
        }
    }
}



//Boiler plate code...
/*
public abstract class SOSingleton<T> : ScriptableObject where T : SOSingleton<T> {
    private static T instance;
    private static object mutex = new object();

    public static T Instance {
        get {
            lock (SOSingleton<T>.mutex) {
                if (SOSingleton<T>.instance == null) {

                }
            }
            return SOSingleton<T>.instance;
        }
    }
}
*/