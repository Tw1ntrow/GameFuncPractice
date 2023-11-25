using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private Vector3 mousePosition;
    private bool isMousePressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            lineRenderer.positionCount = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        if (isMousePressed)
        {
            // マウスが押されている間、線を描く
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
        }
    }
}