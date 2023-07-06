using Unity.Notifications.Android;
using UnityEngine;

public class TestNotification : MonoBehaviour
{
    void Start()
    {
        // �C�x���g�ɓo�^
        NotificationManager.OnPermissionRequestCompleted += OnPermissionRequestCompleted;

        // �ʒm�̋������N�G�X�g
        StartCoroutine(NotificationManager.RequestNotificationPermission());

    }

    void OnPermissionRequestCompleted(PermissionStatus status)
    {
        // �ʒm�̋����N�G�X�g�̌��ʂɉ����ē����ς���
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