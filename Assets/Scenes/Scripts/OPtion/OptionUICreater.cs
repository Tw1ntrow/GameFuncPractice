using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUICreater : MonoBehaviour
{
    [SerializeField]
    private OptionView optionView;
    [SerializeField]
    private Button seButton;
    void Start()
    {
        SoundManager.Instance.PlayBgm(0);
        optionView.OnBgmVolumeChanged += SoundManager.Instance.SetBgmVolume;
        optionView.OnSeVolumeChanged += SoundManager.Instance.SetSeVolume;
    }

    public void OnClickSeButton()
    {
        SoundManager.Instance.PlaySe(0);
    }

}
