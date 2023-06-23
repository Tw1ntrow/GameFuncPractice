using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewDialog : MonoBehaviour,IDialog
{
    private WebViewObject webViewObject;

    public Action<IDialog> OnClickCloseButton { get; set; }

    public void CloseDialog()
    {
        if (webViewObject != null)
        {
            Destroy(webViewObject.gameObject);
            webViewObject = null;
        }

        Destroy(this.gameObject);
    }

    public void OnClickClose()
    {
        OnClickCloseButton?.Invoke(this);
    }

    public void ViewDialog(DialogParameter parameter)
    {

        if (parameter is not WebViewDialogParameter)
        {
            Debug.LogError("パラメーターが不適切です");
            return;
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = (parameter as WebViewDialogParameter).WebViewSize;

        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init(
            ld: (msg) => Debug.Log(string.Format("CallOnLoaded[{0}]", msg)),
            enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
    webViewObject.bitmapRefreshCycle = 1;
#endif
        webViewObject.LoadURL((parameter as WebViewDialogParameter).url);

        // RectTransformの位置をスクリーン座標に変換
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);

        // RectTransformの各角のワールド座標をスクリーン座標に変換する
        var margins = WebViewUtils.ToMargins(rectTransform);

        webViewObject.SetMargins(margins.Left, margins.Top, margins.Right, margins.Bottom, true);

        Debug.Log($"SetWebView Top:{margins.Top} left:{margins.Left} right:{margins.Right} bottom:{margins.Bottom}");

        webViewObject.SetVisibility(true);
    }
}
