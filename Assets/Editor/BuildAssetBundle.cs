using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BuildAssetBundle : EditorWindow {

    [MenuItem("Assets/Build AssetBundle From Selection - Dependency Tracking")]
    public static void ExportResourceWithTracking() {
        ExportResource(true);
    }

    [MenuItem("Assets/Build AssetBundle From Selection - No Dependency Tracking")]
    public static void ExportResourceNoTracking() {
        ExportResource(false);
    }

    [MenuItem("Assets/Build AssetBundles From Directory of Files - Dependency Tracking")]
    public static void ExportResourcesWithTracking() {
        ExportResources(true);
    }

    [MenuItem("Assets/Build AssetBundles From Directory of Files - No Dependency Tracking")]
    public static void ExportResourcesNoTracking() {
        ExportResources(false);
    }

    private static void ExportResources(bool dependencyTracking) {
        //get selected directory
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.Length == 0) {
            //nothing selected
            return;
        }

        Debug.Log(path);
        path = path.Replace("Assets/", "");
        Debug.Log(path);

        string[] fileEntries = Directory.GetFiles(Path.Combine(Application.dataPath, path));
        foreach (string fileName in fileEntries) {

            Debug.Log(fileName);
            string filePath = fileName.Replace("\\", "/");
            int index = filePath.LastIndexOf("/");
            filePath  = filePath.Substring(index);
            Debug.Log(filePath);

            string localPath = Path.Combine("Assets", path);
            Debug.Log(localPath);
            if (index > 0) {
                localPath += filePath;
            }
            Debug.Log(localPath);

            Object asset = AssetDatabase.LoadMainAssetAtPath(localPath);
            if (asset != null) {
                Debug.Log(asset.name);
                string bundlePath = Path.Combine("Assets", path);
                bundlePath = Path.Combine(bundlePath, asset.name + ".unity3d");
                Debug.Log(bundlePath);

                //build resource file from selection
                BuildAssetBundleOptions dependencyTrackingOption
                    = dependencyTracking ? BuildAssetBundleOptions.CollectDependencies : 0;

                BuildPipeline.BuildAssetBundle(asset, null, bundlePath,
                    BuildAssetBundleOptions.CompleteAssets | dependencyTrackingOption,
                    EditorUserBuildSettings.activeBuildTarget);
            }
        }
    }

    private static void ExportResource(bool dependencyTracking) {
        //open save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length == 0) {
            //nothing selected
            return;
        }
        //build resource file from selection
        if (dependencyTracking) {
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path,
                BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.CollectDependencies,
                EditorUserBuildSettings.activeBuildTarget);
            Selection.objects = selection;
        }
        else {
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path,
                BuildAssetBundleOptions.CompleteAssets, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}
