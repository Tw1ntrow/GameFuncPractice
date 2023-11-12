using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSaveProject
{
    private const float saveIntervalSeconds = 1800;
    private static double lastSaveTime;

    static AutoSaveProject()
    {
        EditorApplication.update += OnUpdate;
        lastSaveTime = EditorApplication.timeSinceStartup;
    }

    private static void OnUpdate()
    {
        double currentTime = EditorApplication.timeSinceStartup;
        if (currentTime - lastSaveTime >= saveIntervalSeconds)
        {
            SaveProject();
            lastSaveTime = currentTime;
        }
    }

    private static void SaveProject()
    {
        EditorApplication.SaveScene();
        EditorApplication.SaveAssets();
        Debug.Log("Auto Save: Project saved.");
    }
}
