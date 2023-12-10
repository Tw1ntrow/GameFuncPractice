using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : MonoBehaviour,IVolumeAdjustable
{
    [SerializeField]
    private Slider bgmSlider = default;
    [SerializeField]
    private Slider seSlider = default;

    public Action<float> OnBgmVolumeChanged { get ; set ; }
    public Action<float> OnSeVolumeChanged { get ; set ; }

    private void Start()
    {
        bgmSlider.value = SoundManager.Instance.BgmVolume;
        seSlider.value = SoundManager.Instance.SEVolume;
    }


    public void OnBgmSliderChanged()
    {
        OnBgmVolumeChanged?.Invoke(bgmSlider.value);
    }

    public void OnSeSliderChanged()
    {
        OnSeVolumeChanged?.Invoke(seSlider.value);
    }

}
