using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

/// <summary>
/// 通知を登録する
/// </summary>
public static class NotificationManager
{
    public static event Action<PermissionStatus> OnPermissionRequestCompleted;
    /// <summary>
    /// 通知の許可ウインドウを表示する
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
            // 通知を許可しないを選択した場合、再度表示しない
            PlayerPrefs.SetInt("NotificationPermission", (int)PermissionStatus.DeniedDontAskAgain);
            PlayerPrefs.Save();
        }

        OnPermissionRequestCompleted?.Invoke(request.Status);
        Debug.Log(request.Status.ToString());

    }

    /// <summary>
    /// 通知チャンネルを登録する
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
    /// 通知を削除する
    /// </summary>
    public static void AllClear()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllNotifications();
    }

    /// <summary>
    /// 通知を登録する
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
            //Androidのアイコンを設定
            SmallIcon = "small_icon",
            LargeIcon = "large_icon",
            FireTime = System.DateTime.Now.AddSeconds(second),
        };
        AndroidNotificationCenter.SendNotification(notification, id);
    }

    /// <summary>
    /// リピーティング通知を登録する
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
