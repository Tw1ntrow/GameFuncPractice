using UnityEngine;

public class ChatGptEntry : MonoBehaviour
{
    void Start()
    {
        var gpt = new ChatGptConnection();
        //gpt.RequestAsync("Unityについて教えて下さい");
        // トークン毎にお金が掛かるためコメントアウト
    }

}
