using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    private Toggle fpsToggle30;
    [SerializeField]
    private Toggle fpsToggle60;
    [SerializeField]
    private Text fpsText;
    private void Start()
    {
        int savedFPS = PlayerPrefs.GetInt("FPS", 30);  // デフォルトは30FPS
        SetFPS(savedFPS);
        fpsToggle30.isOn = savedFPS == 30;  // トグルの初期値を設定
        fpsToggle60.isOn = savedFPS == 60;  // トグルの初期値を設定
    }


    private void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
    }

    public void OnFPSToggled()
    {
        // トグルの状態によってFPSを変更する
        int newFPS = fpsToggle60.isOn ? 60 : 30;
        SetFPS(newFPS);
    }

    private void SetFPS(int fps)
    {
        // FPSの値を変更し、その設定を保存する
        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("FPS", fps);
    }
}