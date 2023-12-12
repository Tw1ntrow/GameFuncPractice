using UnityEngine;

namespace GameFunc
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5.0f;
        [SerializeField]
        private float jumpHeight = 2.0f;
        [SerializeField]
        private float mouseSensitivity = 100.0f;
        [SerializeField]
        private Transform playerBody;
        [SerializeField]
        private Camera playerCamera;
        [SerializeField]
        private float gravity = -9.81f;

        private CharacterController controller;
        private float xRotation = 0f;
        private Vector3 velocity;
        private bool isGrounded;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            ProcessMouseLook();
            ProcessMovement();
            ProcessGravityAndJump();
        }

        private void ProcessMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void ProcessMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * speed * Time.deltaTime);
        }

        private void ProcessGravityAndJump()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}