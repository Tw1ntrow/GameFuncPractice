using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text chatDisplay;
    [SerializeField]
    private InputField chatInput;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Photonサーバーに接続
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // ロビーに参加
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("ChatRoom", new RoomOptions { MaxPlayers = 20 }, null); // チャットルームに参加または作成
    }

    public void SendChatMessage()
    {
        if (!string.IsNullOrEmpty(chatInput.text))
        {
            photonView.RPC("UpdateChat", RpcTarget.All, PhotonNetwork.NickName, chatInput.text);
            chatInput.text = "";
        }
    }

    [PunRPC]
    public void UpdateChat(string playerName, string message)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "名無し";
        }
        chatDisplay.text += $"\n{playerName}: {message}";
    }
}
