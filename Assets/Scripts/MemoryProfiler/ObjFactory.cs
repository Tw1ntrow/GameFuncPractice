using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFactory : MonoBehaviour
{
    List<Texture> _textures = new List<Texture>();
    void Start()
    {
        // メモリ使用量を増やすために、テクスチャを10個生成しておく
        for (int i = 0; i < 10; i++)
        {
            var tex = new Texture2D(2048, 2048);
            tex.name = "Unused Texure " + i;
            _textures.Add(tex);
        }
    }

}
