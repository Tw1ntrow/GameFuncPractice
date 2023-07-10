using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadProgressbar : MonoBehaviour
{
    [SerializeField]
    private Slider totalProgressSlider;
    [SerializeField]
    private Slider currentProgressSlider;
    [SerializeField]
    private Text progressText;

    public void UpdateProgressSlioder(float percentComplete, float totalComplete, string progress)
    {
        currentProgressSlider.value = percentComplete;
        totalProgressSlider.value = totalComplete;
        progressText.text = progress;

    }

    public void OnComplete()
    {
        // �{���͊������o�����ꂽ�肷��
        currentProgressSlider.value = 1;
        totalProgressSlider.value = 1;
        progressText.text = "�_�E�����[�h�����I";
    }
}
