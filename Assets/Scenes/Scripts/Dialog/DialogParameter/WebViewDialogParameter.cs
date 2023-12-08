using UnityEngine;

public class WebViewDialogParameter : DialogParameter
{
    public string url;
    public Vector2 WebViewSize;

    public WebViewDialogParameter(string url, Vector2 webViewSize)
    {
        this.url = url;
        WebViewSize = webViewSize;
    }
}
