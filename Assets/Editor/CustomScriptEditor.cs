using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // オリジナルのインスペクターGUIを表示
        DrawDefaultInspector();

        CustomParam CustomParam = (CustomParam)target;

        // カスタムUIを追加
        GUILayout.Label("Custom Inspector");
        CustomParam.intValue = EditorGUILayout.IntField("Integer Value", CustomParam.intValue);
        CustomParam.floatValue = EditorGUILayout.FloatField("Float Value", CustomParam.floatValue);
        CustomParam.stringValue = EditorGUILayout.TextField("String Value", CustomParam.stringValue);
        CustomParam.toggleValue = EditorGUILayout.Toggle("Toggle Value", CustomParam.toggleValue);
        CustomParam.colorValue = EditorGUILayout.ColorField("Color Value", CustomParam.colorValue);
        CustomParam.vectorValue = EditorGUILayout.Vector3Field("Vector3 Value", CustomParam.vectorValue);
        CustomParam.gameObjectValue = EditorGUILayout.ObjectField("Game Object", CustomParam.gameObjectValue, typeof(GameObject), true) as GameObject;

        // 値が変更されたときにシーンを更新
        if (GUI.changed)
        {
            EditorUtility.SetDirty(CustomParam);
        }
    }
}
