using Unity.Notifications.Android;
using UnityEngine;

public class TestNotification : MonoBehaviour
{
    void Start()
    {
        // イベントに登録
        NotificationManager.OnPermissionRequestCompleted += OnPermissionRequestCompleted;

        // 通知の許可をリクエスト
        StartCoroutine(NotificationManager.RequestNotificationPermission());

    }

    void OnPermissionRequestCompleted(PermissionStatus status)
    {
        // 通知の許可リクエストの結果に応じて動作を変える
        if (status == PermissionStatus.Allowed)
        {
            NotificationManager.RegisterChannel("channel_id", "Test Channel", "This is a test channel");
            NotificationManager.SetAndroidNotification("Test Title", "This is a test notification", 5, "channel_id");

        }
        else if (status == PermissionStatus.Denied)
        {

        }
        else if (status == PermissionStatus.NotRequested)
        {
        }
    }
}