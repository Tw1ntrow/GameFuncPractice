using UnityEditor;
using UnityEngine;

public class AssetNamingChecker : AssetPostprocessor
{
    private void OnPreprocessAsset()
    {
    string assetName = System.IO.Path.GetFileName(assetPath);

    // �����Ŏw�肵�������K�����`�F�b�N
    if (!assetName.StartsWith("MyAsset_"))
    {
        EditorUtility.DisplayDialog(
            "Invalid Asset Name",
            $"Asset '{assetName}' does not follow the naming convention. Importing has been cancelled.",
            "OK"
        );

        // �A�Z�b�g�̃C���|�[�g�𒆒f���邽�߂̃G���[�𓊂���
        // �����ƃG���[���o������̂ŁA�R�����g�A�E�g���Ă���
        //throw new AssetImportCancelledException($"Asset '{assetName}' does not follow the naming convention.");
    }
}

public class AssetImportCancelledException : System.Exception
{
    public AssetImportCancelledException(string message) : base(message) { }
}