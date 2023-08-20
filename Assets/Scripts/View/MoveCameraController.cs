using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera movingCamera;
    
    private const int MOVE_PRIORITY = 10;
    private const int NORMAL_PRIORITY = 0;

    public void Changed(bool isMove)
    {
        if(isMove)
        {
            movingCamera.Priority = MOVE_PRIORITY;

        }
        else
        {
            movingCamera.Priority = NORMAL_PRIORITY;
        }
    }
}
