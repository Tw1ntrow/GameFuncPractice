using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFactory : MonoBehaviour
{
    List<Texture> _textures = new List<Texture>();
    void Start()
    {
        // �������g�p�ʂ𑝂₷���߂ɁA�e�N�X�`����10�������Ă���
        for (int i = 0; i < 10; i++)
        {
            var tex = new Texture2D(2048, 2048);
            tex.name = "Unused Texure " + i;
            _textures.Add(tex);
        }
    }

}
