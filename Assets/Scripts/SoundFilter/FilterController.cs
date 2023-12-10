using UnityEngine;
using UnityEngine.UI;

public class FilterController : MonoBehaviour
{
    [SerializeField]
    private Slider lowPassSlider;
    [SerializeField]
    private Slider highPassSlider;

    private AudioLowPassFilter lowPassFilter;
    private AudioHighPassFilter highPassFilter;

    void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        highPassFilter = GetComponent<AudioHighPassFilter>();

        lowPassSlider.value = lowPassFilter.cutoffFrequency;
        highPassSlider.value = highPassFilter.cutoffFrequency;

        // スライダーのリスナーを設定
        lowPassSlider.onValueChanged.AddListener(OnLowPassSliderChanged);
        highPassSlider.onValueChanged.AddListener(OnHighPassSliderChanged);
    }

    void OnLowPassSliderChanged(float value)
    {
        lowPassFilter.cutoffFrequency = value;
    }

    void OnHighPassSliderChanged(float value)
    {
        highPassFilter.cutoffFrequency = value;
    }
}