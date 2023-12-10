using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SocialConnectManager : MonoBehaviour
{
    public void OnCapture()
    {
        // 投稿したいテキストを取得
        string textToPost = "Hello, Twitter!";

        // テキストをツイッターに投稿
        CaptureScreenShot(textToPost);
    }
    private void CaptureScreenShot(string text)
    {
        string tweetText = UnityWebRequest.EscapeURL(text);

        // ツイッターアプリがインストールされている場合はアプリを、そうでなければブラウザを開くURLスキームを作成
        string scheme = "twitter://post?message=" + tweetText;
        string fallbackUrl = "https://twitter.com/intent/tweet?text=" + tweetText;

        try
        {
            var uriClass = new AndroidJavaClass("android.net.Uri");
            var uri = uriClass.CallStatic<AndroidJavaObject>("parse", scheme);
            var intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.VIEW", uri);

            // Intentを使ってアクティビティを開始
            var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intent);
        }
        catch
        {
            // Twitterアプリがインストールされていない場合はブラウザでツイートページを開く
            Application.OpenURL(fallbackUrl);
        }

    }
}
