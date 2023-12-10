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
        int savedFPS = PlayerPrefs.GetInt("FPS", 30);  // �f�t�H���g��30FPS
        SetFPS(savedFPS);
        fpsToggle30.isOn = savedFPS == 30;  // �g�O���̏����l��ݒ�
        fpsToggle60.isOn = savedFPS == 60;  // �g�O���̏����l��ݒ�
    }


    private void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
    }

    public void OnFPSToggled()
    {
        // �g�O���̏�Ԃɂ����FPS��ύX����
        int newFPS = fpsToggle60.isOn ? 60 : 30;
        SetFPS(newFPS);
    }

    private void SetFPS(int fps)
    {
        // FPS�̒l��ύX���A���̐ݒ��ۑ�����
        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("FPS", fps);
    }
}