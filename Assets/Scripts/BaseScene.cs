using UnityEngine;
using System.Collections;

//base scene class for all game scenes to inherit from
public class BaseScene : BaseBehaviour {

    public string StartSceneName = "LogoScene";
    public string NextSceneName  = "MenuScene";
    public bool LoadStartSceneOnResume = true;

    private bool isLoading = false;

#if UNITY_ANDROID
    private const float ExitDelay = 0.25f;
    private bool isRunning = true;
#endif
	
    //subclasses should call base.Awake() if overloading this methid
    protected void Awake() {
    }
	
    //subclasses should call base.Start() if overloading this methid
    protected void Start() {
        DisableScreenSaver();
    }
	
    //subclasses should call base.Update() if overloading this methid
    protected void Update() {
#if UNITY_ANDROID
        if (isRunning) {
            //handle back button on Android devices
            //TODO: handle back button on Windows Phone devices
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Application.loadedLevelName == StartSceneName) {
                    //exit game when in start scene
                    isRunning = false;
                }
                else {
                    //load start scene when in other scenes
                    LoadStartScene();
                }
            }
        }
        else {
            //show several frames before exit to avoid animation stutter
            Invoke("Exit", ExitDelay);
        }
#endif
    }

    private void OnApplicationPause(bool paused) {
        if (!paused && LoadStartSceneOnResume) {
            //load start scene on resume
            LoadStartScene();
        }
    }
	
    public void LoadScene(string name) {
        StartCoroutine(BeginLoadScene(name));
    }

    public IEnumerator BeginLoadScene(string name) {
        //TODO: show progress bar
        if (!isLoading) {
            isLoading = true;
            yield return Application.LoadLevelAsync(name);
        }
        yield break;
    }

    public void LoadStartScene() {
        LoadScene(StartSceneName);
    }
	
    public void LoadNextScene() {
        LoadScene(NextSceneName);
    }
	
    public IEnumerator BeginLoadStartScene() {
        yield return StartCoroutine(BeginLoadScene(StartSceneName));
    }
	
    public IEnumerator BeginLoadNextScene() {
        yield return StartCoroutine(BeginLoadScene(NextSceneName));
    }
	
    public void DisableScreenSaver() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
