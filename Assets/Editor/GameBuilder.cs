using UnityEngine;
using UnityEditor;

public class GameBuilder : Builder {

    private static string[] gameScenes = {
        "Assets/Scenes/LogoScene.unity",
        "Assets/Scenes/MenuScene.unity",
		"Assets/Scenes/GameScene.unity",
    };

    [MenuItem("Build/iOS/Starter")]
    public static void BuildStarterForIos() {
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
        BuildForIos("Starter", gameScenes, "rocketalien", "starter");
    }

    [MenuItem("Build/Android/Starter")]
    public static void BuildStarterForAndroid() {
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
        BuildForAndroid("Starter", gameScenes, "rocketalien", "starter");
    }
}
