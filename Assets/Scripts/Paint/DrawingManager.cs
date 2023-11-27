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
        // 必要に応じて他のコンポーネントに色を適用する
        ApplyColorToLineRenderer(selectedColor);
    }


    private void ApplyColorToLineRenderer(Color color)
    {
        lineRenderer.startColor = color; 
        lineRenderer.endColor = color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startMousePosition.z = 0;

            if (currentMode == DrawingMode.Square)
            {
                lineRenderer.positionCount = 4; // 四角形の頂点
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        if (isMousePressed)
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
    }

    private void DrawLine()
    {
        // マウスの位置を取得
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

        // 正方形の頂点を設定
        lineRenderer.SetPosition(0, startMousePosition);
        lineRenderer.SetPosition(1, new Vector3(startMousePosition.x, currentMousePosition.y, 0));
        lineRenderer.SetPosition(2, currentMousePosition);
        lineRenderer.SetPosition(3, new Vector3(currentMousePosition.x, startMousePosition.y, 0));
        lineRenderer.SetPosition(4, startMousePosition); // 四角形を閉じる
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
}