using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MaintainAspectRatio : MonoBehaviour
{
    private const float targetAspect = 16.0f / 9.0f;

    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        UpdateCameraViewport();
    }

    void Update()
    {
        UpdateCameraViewport();
    }

    void UpdateCameraViewport()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;
        Rect rect = cameraComponent.rect;

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cameraComponent.rect = rect;
    }
}