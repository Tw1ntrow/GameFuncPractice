using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGage : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private PlayerAttribute param;  // param is now a member variable.

    void Start()
    {
        param = new PlayerAttribute();
        param.MaxHP = 100;
        param.HP = 100;
        param.OnChangedHP += ChangedGage;
    }

    private void ChangedGage(int hp)
    {
        slider.value = (float)hp / param.MaxHP;
    }
}