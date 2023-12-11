using UnityEngine;

namespace GameFunc
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5.0f;
        [SerializeField]
        private float mouseSensitivity = 100.0f;
        [SerializeField]
        private Transform playerBody;
        [SerializeField]
        private Camera playerCamera;

        private float xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
            GetComponent<CharacterController>().Move(movement * speed * Time.deltaTime);
        }
    }
}