using UnityEditor;
using UnityEngine;

public class AssetNamingChecker : AssetPostprocessor
{
    private void OnPreprocessAsset()
    {
    string assetName = System.IO.Path.GetFileName(assetPath);

    // ここで指定した命名規則をチェック
    if (!assetName.StartsWith("MyAsset_"))
    {
        EditorUtility.DisplayDialog(
            "Invalid Asset Name",
            $"Asset '{assetName}' does not follow the naming convention. Importing has been cancelled.",
            "OK"
        );

        // アセットのインポートを中断するためのエラーを投げる
        // ずっとエラーが出続けるので、コメントアウトしておく
        //throw new AssetImportCancelledException($"Asset '{assetName}' does not follow the naming convention.");
    }
}

public class AssetImportCancelledException : System.Exception
{
    public AssetImportCancelledException(string message) : base(message) { }
}