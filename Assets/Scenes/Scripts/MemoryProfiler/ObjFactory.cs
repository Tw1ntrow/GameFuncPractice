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

    // �I�u�W�F�N�g�����̃e�X�g
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
        // �������g�p�ʂ𑝂₷���߂ɁA�e�N�X�`����10�������Ă���
        for (int i = 0; i < 10; i++)
        {
            var tex = new Texture2D(2048, 2048);
            tex.name = "Unused Texure " + i;
            _textures.Add(tex);
        }
    }

    // �񓯊������̃e�X�g
    IEnumerator LoadAsyncData()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        Debug.Log("Data Loaded!");
    }

}
