using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    void Start()
    {
        this.transform.DOMove(new Vector3(2f, 0f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);
        this.transform.DOMove(new Vector3(0f, 0f, 1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

}
