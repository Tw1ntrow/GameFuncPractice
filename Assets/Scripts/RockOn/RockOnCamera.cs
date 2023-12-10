using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockOnCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera rockonCamera;

    [SerializeField]
    private TextMesh enemyText;

    // カメラでロックオンを試みる範囲の半径
    public float lockonRadius = 5000.0f;

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Vector3 cameraPosition = rockonCamera.transform.position;

            Vector3 cameraForward = rockonCamera.transform.forward;

            Collider[] colliders = Physics.OverlapSphere(cameraPosition + cameraForward * lockonRadius, lockonRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    rockonCamera.LookAt = collider.transform;
                    enemyText.text = "ロックオン！";
                    enemyText.color = Color.red; 
                    enemyText.fontSize = 18;
                    break; // 一つ見つけたら終了
                }
            }
        }
    }
}
