using UnityEngine;
using System.Collections;

//base scene class for all game scenes to inherit from
public class BaseScene : MonoBehaviour {

    public string StartSceneName = "LogoScene";
    public string NextSceneName  = "MenuScene";
    public bool ShowStartSceneOnResume = true;

    private bool isLoading = false;

#if UNITY_ANDROID
    private const float ExitDelay = 0.25f;
    private bool isRunning = true;
    private float elapsedTime = 0.0f;
#endif
	
    //subclasses should call base.Awake()
    protected void Awake() {
    }
	
    //subclasses should call base.Start()
    protected void Start() {
        DisableScreenSaver();
    }
	
    //subclasses should call base.Update()
    protected void Update() {
#if UNITY_ANDROID
        if (isRunning) {
            //handle back button on Android devices
            //TODO: handle back button on Windows Phone devices
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (Application.loadedLevelName == StartSceneName) {
                    //exit game only if in start scene
                    isRunning = false;
                }
                else {
                    LoadStartScene();
                }
            }
        }
        else {
            //show several frames before exit to avoid animation stutter
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= ExitDelay) {
                Application.Quit();
            }
        }
#endif
    }

    private void OnApplicationPause(bool paused) {
        if (!paused && ShowStartSceneOnResume) {
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
