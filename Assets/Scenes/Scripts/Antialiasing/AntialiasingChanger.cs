using UnityEngine;
using UnityEngine.UI;

public class AntiAliasingSwitch : MonoBehaviour
{
    public Dropdown dropdown;

    private void Start()
    {
        dropdown.value = QualitySettings.antiAliasing;

        dropdown.onValueChanged.AddListener(SetAntiAliasing);
    }

    public void SetAntiAliasing(int level)
    {
        switch (level)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
            default:
                Debug.Log("Invalid value.");
                break;
        }
    }
}