using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Slider redSlider, greenSlider, blueSlider;
    [SerializeField]
    private Image colorDisplay;
    [SerializeField] 
    private RectTransform captureArea;

    private Vector3 startMousePosition;
    private bool isMousePressed;
    private enum DrawingMode { Line, Square }
    private DrawingMode currentMode = DrawingMode.Line;

    private void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        Color selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        colorDisplay.color = selectedColor;

        ApplyColorToLineRenderer(selectedColor);
    }


    private void ApplyColorToLineRenderer(Color color)
    {
        lineRenderer.startColor = color; 
        lineRenderer.endColor = color;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0) && IsInDrawingArea(mousePos))
        {
            // �}�E�X���삪captureArea���ŊJ�n���ꂽ�ꍇ
            StartDrawing(mousePos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // �}�E�X�{�^���������ꂽ��`����I��
            isMousePressed = false;
        }

        if (isMousePressed && IsInDrawingArea(mousePos))
        {
            // �}�E�X���삪�����Ă��āA�Ȃ�����captureArea���ɂ���ꍇ
            UpdateDrawing(mousePos);
        }
    }

    private void DrawLine()
    {
        // �}�E�X�̈ʒu���擾
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentMousePosition);
    }

    private void DrawSquare()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0;

        lineRenderer.positionCount = 5;

        // �����`�̒��_��ݒ�
        lineRenderer.SetPosition(0, startMousePosition);
        lineRenderer.SetPosition(1, new Vector3(startMousePosition.x, currentMousePosition.y, 0));
        lineRenderer.SetPosition(2, currentMousePosition);
        lineRenderer.SetPosition(3, new Vector3(currentMousePosition.x, startMousePosition.y, 0));
        lineRenderer.SetPosition(4, startMousePosition); // �l�p�`�����
    }

    public void ChangeDrawingSquare()
    {
        if (currentMode == DrawingMode.Line)
        {
            currentMode = DrawingMode.Square;
        }
    }

    public void ChangeDrawingLine()
    {
        if (currentMode == DrawingMode.Square)
        {
            currentMode = DrawingMode.Line;
        }
    }

    private bool IsInDrawingArea(Vector3 mousePosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(captureArea, mousePosition, Camera.main);
    }

    private void StartDrawing(Vector3 mousePosition)
    {
        isMousePressed = true;
        startMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        startMousePosition.z = 0;

        if (currentMode == DrawingMode.Square)
        {
            lineRenderer.positionCount = 4; // �l�p�`�̒��_
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    private void UpdateDrawing(Vector3 mousePosition)
    {
        if (currentMode == DrawingMode.Line)
        {
            DrawLine();
        }
        else if (currentMode == DrawingMode.Square)
        {
            DrawSquare();
        }
    }


    public void SaveDrawing()
    {
        StartCoroutine(CaptureAndSave());
    }

    private IEnumerator CaptureAndSave()
    {
        yield return new WaitForEndOfFrame();

        // RectTransform����X�N���[�����W���擾
        Rect rect = GetScreenRectFromRectTransform(captureArea);
        Texture2D screenImage = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenImage.ReadPixels(rect, 0, 0);
        screenImage.Apply();

        byte[] imageBytes = screenImage.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, "savedDrawing.png");
        File.WriteAllBytes(filePath, imageBytes);

        Debug.Log($"Drawing saved to {filePath}");
    }

    private Rect GetScreenRectFromRectTransform(RectTransform rectTransform)
    {
        Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);
        Rect rect = new Rect(rectTransform.position.x, Screen.height - rectTransform.position.y, size.x, size.y);
        rect.x -= rectTransform.pivot.x * size.x;
        rect.y -= (1.0f - rectTransform.pivot.y) * size.y;
        return rect;
    }
}