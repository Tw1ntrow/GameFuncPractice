using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FriendSearchView : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.InputField searchInputField;
    [SerializeField]
    private UnityEngine.UI.Button searchButton;
    [SerializeField]
    private Sprite testAvator;
    [SerializeField]
    private FriendListView friendRequestView;

    [SerializeField]
    private FriendSearchItem searchItem;

    private void Awake()
    {
        searchButton.onClick.AddListener(OnSearchButtonClicked);
    }

    private void OnSearchButtonClicked()
    {
        Debug.Log("Searching for: " + searchInputField.text);
        FriendSearchItem friendItem = Instantiate(searchItem, this.transform);
        friendItem.Initialize(searchInputField.text, testAvator);
        friendItem.OnClickRequestButton += AddFriend;

    }

    private void AddFriend(string name,Sprite avator)
    {
        friendRequestView.AddFriend(name, avator);
        gameObject.SetActive(false);
    }
}
