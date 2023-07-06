using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

/// <summary>
/// �ʒm��o�^����
/// </summary>
public static class NotificationManager
{
    public static event Action<PermissionStatus> OnPermissionRequestCompleted;
    /// <summary>
    /// �ʒm�̋��E�C���h�E��\������
    /// </summary>
    /// <returns></returns>
    public static IEnumerator RequestNotificationPermission()
    {
        if(PlayerPrefs.HasKey("NotificationPermission"))
        {
            if(PlayerPrefs.GetInt("NotificationPermission") == (int)PermissionStatus.DeniedDontAskAgain)
            {
                yield break;
            }
        }

        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
        {
            yield return null;
        }

        if (request.Status == PermissionStatus.DeniedDontAskAgain)
        {
            // �ʒm�������Ȃ���I�������ꍇ�A�ēx�\�����Ȃ�
            PlayerPrefs.SetInt("NotificationPermission", (int)PermissionStatus.DeniedDontAskAgain);
            PlayerPrefs.Save();
        }

        OnPermissionRequestCompleted?.Invoke(request.Status);
        Debug.Log(request.Status.ToString());

    }

    /// <summary>
    /// �ʒm�`�����l����o�^����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="description"></param>
    public static void RegisterChannel(string id,string title,string description)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = id,
            Name = title,
            Description = description,
            Importance = Importance.High,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
    }

    /// <summary>
    /// �ʒm���폜����
    /// </summary>
    public static void AllClear()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllNotifications();
    }

    /// <summary>
    /// �ʒm��o�^����
    /// </summary>
    /// <param name="title"></param>
    /// <param name="text"></param>
    /// <param name="second"></param>
    public static void SetAndroidNotification(string title,string text,int second,string id)
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            //Android�̃A�C�R����ݒ�
            SmallIcon = "small_icon",
            LargeIcon = "large_icon",
            FireTime = System.DateTime.Now.AddSeconds(second),
        };
        AndroidNotificationCenter.SendNotification(notification, id);
    }

    /// <summary>
    /// ���s�[�e�B���O�ʒm��o�^����
    /// </summary>
    /// <param name="title"></param>
    /// <param name="text"></param>
    /// <param name="fireTime"></param>
    /// <param name="interval"></param>
    public static void SetRepeatingNotification(string title, string text, DateTime fireTime, TimeSpan interval)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            SmallIcon = "small_icon",
            LargeIcon = "large_icon",
            FireTime = fireTime,
            RepeatInterval = interval
        };

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

}
