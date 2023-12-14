using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // テレポート距離
    [SerializeField]
    private float teleportDistance = 5.0f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ProcessTeleport();
    }

    private void ProcessTeleport()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, teleportDistance))
            {
                Vector3 teleportPosition = hit.point;

                characterController.enabled = false;
                transform.position = teleportPosition;
                characterController.enabled = true;
            }
            else
            {
                // Rayが何も衝突しなかった場合、teleportDistanceだけ前進
                Vector3 teleportPosition = transform.position + transform.forward * teleportDistance;
                characterController.enabled = false;
                transform.position = teleportPosition;
                characterController.enabled = true;
            }
        }
    }
}
