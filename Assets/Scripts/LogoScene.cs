using UnityEngine;
using System.Collections;

public class LogoScene : BaseScene {

    public float Delay = 3.0f;

    new void Start() {
        base.Start();
        Invoke("LoadNextScene", Delay);
    }
}
