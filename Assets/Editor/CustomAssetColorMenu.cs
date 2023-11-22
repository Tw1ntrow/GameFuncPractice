using UnityEngine;
using UnityEditor;

public class CustomAssetColorMenu
{
    [MenuItem("Custom Tools/Apply Custom Color to All Assets")]
    private static void ApplyColorToAllAssets()
    {
        // 進捗バーを表示
        EditorUtility.DisplayProgressBar("Applying Color", "Applying custom color to all assets...", 0.0f);

        var assets = AssetDatabase.GetAllAssetPaths();
        for (int i = 0; i < assets.Length; i++)
        {
            var labelColor = new Color(1.0f, 0.5f, 0.0f);
            AssetDatabase.SetLabels(AssetDatabase.LoadAssetAtPath<Object>(assets[i]), new[] { "<color=#" + ColorUtility.ToHtmlStringRGB(labelColor) + ">●</color>" });

            // 進捗を更新
            if (i % 10 == 0)
                EditorUtility.DisplayProgressBar("Applying Color", "Applying custom color to all assets...", (float)i / assets.Length);
        }

        // 進捗バーを閉じる
        EditorUtility.ClearProgressBar();
    }

    [MenuItem("Custom Tools/Reset Color of All Assets")]
    private static void ResetColorOfAllAssets()
    {
        // 進捗バーを表示
        EditorUtility.DisplayProgressBar("Resetting Color", "Resetting color of all assets...", 0.0f);

        var assets = AssetDatabase.GetAllAssetPaths();
        for (int i = 0; i < assets.Length; i++)
        {
            // ラベルをクリア
            AssetDatabase.SetLabels(AssetDatabase.LoadAssetAtPath<Object>(assets[i]), new string[0]);

            // 進捗を更新
            if (i % 10 == 0)
                EditorUtility.DisplayProgressBar("Resetting Color", "Resetting color of all assets...", (float)i / assets.Length);
        }

        // 進捗バーを閉じる
        EditorUtility.ClearProgressBar();
    }
}