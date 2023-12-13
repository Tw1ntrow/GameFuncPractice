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
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private Transform bulletSpawnPoint;
        [SerializeField]
        private float fireRate = 0.1f;
        [SerializeField]
        private float bulletSpeed = 20f;
        [SerializeField]
        private float bulletLifeTime = 3f;

        private CharacterController controller;
        private float xRotation = 0f;
        private Vector3 velocity;
        private bool isGrounded;
        private float nextFireTime = 0f;

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
            ProcessShooting();
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

        private void ProcessShooting()
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Vector3 cameraPosition = playerCamera.transform.position;

            // 発射位置をプレイヤーカメラの位置に合わせる
            bulletSpawnPoint.position = cameraPosition;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, playerCamera.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.AddForce(playerCamera.transform.forward * bulletSpeed, ForceMode.Impulse);
            }

            Destroy(bullet, bulletLifeTime);
        }
    }
}