
using UnityEngine;

public class UserProfile
{
    public string UserID { get; private set; }
    public string UserName { get; private set; }
    public Sprite Avatar { get; private set; }

    public UserProfile(string userID, string userName, Sprite avatar)
    {
        UserID = userID;
        UserName = userName;
        Avatar = avatar;
    }
}
