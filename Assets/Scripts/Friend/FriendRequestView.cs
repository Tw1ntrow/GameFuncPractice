using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendRequestView : MonoBehaviour
{
    [SerializeField] 
    private UnityEngine.UI.Text requestNotificationText;

    private List<UserProfile> friendRequests = new List<UserProfile>();

    public void AddFriendRequest(UserProfile userProfile)
    {
        friendRequests.Add(userProfile);
        UpdateUI();
    }

    private void UpdateUI()
    {
        //　サーバー側で何かしら処理する
    }

    public void AcceptRequest(UserProfile userProfile)
    {
        friendRequests.Remove(userProfile);
        UpdateUI();
    }
}
