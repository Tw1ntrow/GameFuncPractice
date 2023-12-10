using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendListView : MonoBehaviour
{
    [SerializeField]
    private Transform friendListContainer;
    [SerializeField]
    private FriendItemView friendItemPrefab;
    [SerializeField]
    private Sprite testAvator;

    private List<UserProfile> friends = new List<UserProfile>();

    private void Start()
    {
        AddFriend("test0", testAvator);
        AddFriend("test1", testAvator);
        AddFriend("test2", testAvator);
        AddFriend("test3", testAvator);
    }

    public void AddFriend(string name,Sprite avator)
    {
        friends.Add(new UserProfile(friends.Count.ToString(),name,avator));
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var friend in friends)
        {
            FriendItemView friendItem = Instantiate(friendItemPrefab, friendListContainer);
            friendItem.Initialize(friend.UserName,friend.Avatar);
        }
    }
}
