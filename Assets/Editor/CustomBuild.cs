using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildMenu : EditorWindow
{
    [MenuItem("Custom/Build Android")]
    public static void BuildAndroid()
    {
        string date = System.DateTime.Now.ToString("yyyyMMdd");

        string buildPath = "Builds/Android/";
        int buildCount = 1;

        while (File.Exists(buildPath + date + "_" + buildCount + ".apk"))
        {
            buildCount++;
        }
        
        string buildName = date + "_" + buildCount;
        
        string buildPathWithFilename = buildPath + buildName + ".apk";
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
        buildPlayerOptions.locationPathName = buildPathWithFilename;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + buildName);
        }
        else
        {
            Debug.LogError("Build failed: " + summary.result);
        }
    }
}