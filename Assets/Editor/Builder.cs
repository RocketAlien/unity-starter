using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class Builder : EditorWindow {

	private const string DirectoryName = "Build";
	
    public static void BuildForIos(string productName, string[] scenes, string companyName, string bundleId) {
        PlayerSettings.iOS.sdkVersion      = iOSSdkVersion.DeviceSDK;
        PlayerSettings.iOS.targetOSVersion = iOSTargetOSVersion.iOS_5_0;
        PlayerSettings.iOS.targetDevice    = iOSTargetDevice.iPhoneAndiPad;
        Build(BuildTarget.iPhone, productName, scenes, companyName, bundleId);
    }

    public static void BuildForAndroid(string productName, string[] scenes, string companyName, string bundleId) {
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel9;
        PlayerSettings.Android.targetDevice  = AndroidTargetDevice.ARMv7;
        Build(BuildTarget.Android, productName + ".apk", scenes, companyName, bundleId);
    }

    public static void Build(BuildTarget buildTarget, string productName, string[] scenePaths, string companyName, string bundleId) {
        if (EditorUserBuildSettings.activeBuildTarget != buildTarget) {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        }
		if (!Directory.Exists(DirectoryName)) {
			Directory.CreateDirectory(DirectoryName);
		}
        PlayerSettings.productName = productName;
        productName = productName.Replace(" ", null);
        if (string.IsNullOrEmpty(bundleId)) {
            bundleId = productName;
        }
        PlayerSettings.strippingLevel = StrippingLevel.UseMicroMSCorlib;
        PlayerSettings.bundleIdentifier = "com." + companyName + "." + bundleId;
		string appPath = Path.Combine(DirectoryName, string.Format("{0}-{1}", buildTarget.ToString(), productName));
        BuildPipeline.BuildPlayer(scenePaths, appPath, buildTarget, BuildOptions.ShowBuiltPlayer);
    }
}
