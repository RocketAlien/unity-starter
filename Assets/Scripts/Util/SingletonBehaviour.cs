using UnityEngine;

//turn any subclass into a singleton behaviour
public class SingletonBehaviour<T> : BaseBehaviour where T : BaseBehaviour {

    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }
}
