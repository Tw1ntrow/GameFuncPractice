using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFactory : MonoBehaviour
{
    List<Texture> _textures = new List<Texture>();
    List<Vector3> _hugeDataList = new List<Vector3>();
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(LoadAsyncData());
        }
    }

    // オブジェクト生成のテスト
    void ObjGenerater()
    {
        for (int i = 0; i < 1000; i++)
        {
            new GameObject("Obj " + i);
        }
    }

    void DataGenerater()
    {
        for (int i = 0; i < 1000000; i++)
        {
            _hugeDataList.Add(new Vector3(Random.value, Random.value, Random.value));
        }
    }

    void TextureGenerater()
    {
        // メモリ使用量を増やすために、テクスチャを10個生成しておく
        for (int i = 0; i < 10; i++)
        {
            var tex = new Texture2D(2048, 2048);
            tex.name = "Unused Texure " + i;
            _textures.Add(tex);
        }
    }

    // 非同期処理のテスト
    IEnumerator LoadAsyncData()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        Debug.Log("Data Loaded!");
    }

}
