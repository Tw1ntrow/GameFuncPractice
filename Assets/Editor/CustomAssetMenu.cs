using UnityEngine;
using UnityEditor;

public class CustomAssetMenu
{
    // �e�N�X�`���Ƀ��j���[��ǉ�
    [MenuItem("Assets/Custom Menu Item for Texture", false, 1)]
    private static void NewMenuOption()
    {
        // �I�����ꂽ�e�N�X�`�����擾
        Texture2D selectedTexture = Selection.activeObject as Texture2D;

        if (selectedTexture != null)
        {
            Debug.Log("Selected Texture: " + selectedTexture.name);
        }
    }

    // ���j���[���ڂ̗L��/�����𐧌䂷��
    [MenuItem("Assets/Custom Menu Item for Texture", true)]
    private static bool NewMenuOptionValidation()
    {
        // �I�����ꂽ�A�Z�b�g���e�N�X�`�����ǂ������`�F�b�N
        return Selection.activeObject is Texture2D;
    }
}