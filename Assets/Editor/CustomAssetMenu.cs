using UnityEngine;
using UnityEditor;

public class CustomAssetMenu
{
    // テクスチャにメニューを追加
    [MenuItem("Assets/Custom Menu Item for Texture", false, 1)]
    private static void NewMenuOption()
    {
        // 選択されたテクスチャを取得
        Texture2D selectedTexture = Selection.activeObject as Texture2D;

        if (selectedTexture != null)
        {
            Debug.Log("Selected Texture: " + selectedTexture.name);
        }
    }

    // メニュー項目の有効/無効を制御する
    [MenuItem("Assets/Custom Menu Item for Texture", true)]
    private static bool NewMenuOptionValidation()
    {
        // 選択されたアセットがテクスチャかどうかをチェック
        return Selection.activeObject is Texture2D;
    }
}