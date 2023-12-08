using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceOrientationController : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void Start()
    {
        ChangeDebugText();
    }

    public void OnPortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        ChangeDebugText();

    }
    public void OnLandscapeLeft()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ChangeDebugText();

    }

    public void OnLandscapeRight()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        ChangeDebugText();

    }

    public void OnAutoRotation()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        ChangeDebugText();

    }

    private void ChangeDebugText()
    {
        text.text = Screen.orientation.ToString();
    }

}
