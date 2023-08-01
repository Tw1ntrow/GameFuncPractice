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

        // ���ϐ�����r���h�p�X���擾
        string buildOutputPath = GetCustomArgumentValue(args, "buildPath");
        if (string.IsNullOrEmpty(buildOutputPath))
        {
            Debug.Log("buildOutputPath null");    
            buildOutputPath = DEFAULT_BUILDPATH; // ���ϐ����ݒ肳��Ă��Ȃ��ꍇ�A�f�t�H���g��BUILDPATH���g�p
        }

        string buildFolderPath = GetBuildFolderPath(developBuild);
        string buildPath = Path.Combine(buildOutputPath, buildFolderPath);

        Debug.Log("OutPutDirectory:" + buildPath);
        // �o�͐�t�H���_���쐬
        if (!Directory.Exists(buildPath))
        {
            Directory.CreateDirectory(buildPath);
        }

        // �r���h�Ώۂ̃V�[���ꗗ���擾
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        // �r���h�̐ݒ�
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = Path.Combine(buildPath, locationPathName),
            target = BuildTarget.StandaloneWindows64,
            options = developBuild ? BuildOptions.Development : BuildOptions.None,
        };

        // �r���h���s
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