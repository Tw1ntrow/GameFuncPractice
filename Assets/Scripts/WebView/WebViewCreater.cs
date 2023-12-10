using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewCreater : MonoBehaviour
{
    void Start()
    {

        DialogManager.Instance.CreateDialog<WebViewDialog>(new WebViewDialogParameter("https://www.google.co.jp",new Vector2(1000,600)));
    }


}
