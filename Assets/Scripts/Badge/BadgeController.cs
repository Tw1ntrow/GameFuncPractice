using UnityEngine;
using UnityEngine.UI;

public class BadgeController : MonoBehaviour
{
    [SerializeField]
    private Image badgeIcon; // �o�b�W�A�C�R��
    [SerializeField]
    public Text badgeText; // �o�b�W�e�L�X�g

    private int notificationCount = 0; // �ʒm��

    private const string NotificationKey = "NotificationCount";

    void Start()
    {
        LoadNotifications();
        UpdateBadge();
    }

    public void AddNotification()
    {
        notificationCount++;
        SaveNotifications();
        UpdateBadge();
    }

    public void ClearNotifications()
    {
        notificationCount = 0;
        SaveNotifications();
        UpdateBadge();
    }

    private void UpdateBadge()
    {
        badgeIcon.gameObject.SetActive(notificationCount > 0);
        if (badgeText != null)
        {
            badgeText.text = notificationCount.ToString();
        }
    }

    private void SaveNotifications()
    {
        PlayerPrefs.SetInt(NotificationKey, notificationCount);
        PlayerPrefs.Save();
    }

    private void LoadNotifications()
    {
        notificationCount = PlayerPrefs.GetInt(NotificationKey, 0);
    }
}