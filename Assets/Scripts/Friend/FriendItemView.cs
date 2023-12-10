using UnityEngine;
using UnityEngine.Rendering;

public class FriendItemView : MonoBehaviour
{
    [SerializeField]
    protected UnityEngine.UI.Text userNameText;
    [SerializeField]
    protected UnityEngine.UI.Image avatarImage;

    public virtual void Initialize(string name,Sprite avatar)
    {
        userNameText.text = name;
        avatarImage.sprite = avatar;
    }
}
