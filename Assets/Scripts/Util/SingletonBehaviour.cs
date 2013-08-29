using UnityEngine;

//turn any base behaviour subclass into a singleton behaviour
public class SingletonBehaviour<T> : BaseBehaviour where T : BaseBehaviour {

    public bool Persistent = false;

    public static T Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            //destroy game object that persisted across scenes
            if (Persistent) Destroy(gameObject);
        }
        else {
            if (Persistent) DontDestroyOnLoad(gameObject);
            Instance = this as T;
        }
    }
}
