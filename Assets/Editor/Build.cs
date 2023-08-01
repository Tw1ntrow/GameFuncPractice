using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Build : MonoBehaviour
{
    private const string DEFAULT_BUILDPATH = "Build/Windows";

    [MenuItem("Build/Build for Windows")]
    public static void BuildForWindows()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        foreach(string arg in args) 
        {
            Debug.Log("arg"+arg);
        }
        bool developBuild = GetCustomArgumentValue(args, "developBuild") == "true";
        Debug.Log("developBuild:" + developBuild.ToString());

        string locationPathName = developBuild ? "ProjectX_dev.exe" : "ProjectX.exe";

        // 環境変数からビルドパスを取得
        string buildOutputPath = GetCustomArgumentValue(args, "buildPath");
        if (string.IsNullOrEmpty(buildOutputPath))
        {
            Debug.Log("buildOutputPath null");    
            buildOutputPath = DEFAULT_BUILDPATH; // 環境変数が設定されていない場合、デフォルトのBUILDPATHを使用
        }

        string buildFolderPath = GetBuildFolderPath(developBuild);
        string buildPath = Path.Combine(buildOutputPath, buildFolderPath);

        Debug.Log("OutPutDirectory:" + buildPath);
        // 出力先フォルダを作成
        if (!Directory.Exists(buildPath))
        {
            Directory.CreateDirectory(buildPath);
        }

        // ビルド対象のシーン一覧を取得
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        // ビルドの設定
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = Path.Combine(buildPath, locationPathName),
            target = BuildTarget.StandaloneWindows64,
            options = developBuild ? BuildOptions.Development : BuildOptions.None,
        };

        // ビルド実行
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    private static string GetCustomArgumentValue(string[] args, string argName)
    {
        string argumentFlag = "-CustomArgs:" + argName + "=";
        Debug.Log("Looking for argument flag: " + argumentFlag);

        string arg = args.FirstOrDefault(a => a.StartsWith(argumentFlag));
        Debug.Log("Found argument: " + arg);

        if (arg != null)
        {
            string value = arg.Substring(argumentFlag.Length);
            Debug.Log("Extracted value: " + value);

            if (!string.IsNullOrEmpty(value))
            {
                Debug.Log("Returning value: " + value);
                return value;
            }
        }

        Debug.Log("No valid argument found, returning null.");
        return null;
    }

    private static string GetBuildFolderPath(bool developBuild)
    {
        string buildTypeFolder = developBuild ? "Develop" : "Release";
        string dateFolder = DateTime.Now.ToString("yyyyMMdd");
        string fileName = developBuild ? "ProjectX_dev" : "ProjectX";
        string buildFolderPath = Path.Combine(buildTypeFolder, dateFolder, fileName);
        return buildFolderPath;
    }
}