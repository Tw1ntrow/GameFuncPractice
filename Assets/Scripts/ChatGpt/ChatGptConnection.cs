using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static ChatGptModel;

public class ChatGptConnection
{
    private readonly string APIKEY = "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";

    //会話履歴を保持する
    private readonly List<ChatGPTMessageModel> _msgList = new List<ChatGPTMessageModel>();

    public ChatGptConnection()
    {
        _msgList.Add(new ChatGPTMessageModel()
        { role = "system", content = "淡々とした口調で" }
        );
    }

    public async UniTask<ChatGPTResponseModel> RequestAsync(string userMsg)
    {
        //エンドポイント
        var apiUrl = "https://api.openai.com/v1/chat/completions";

        _msgList.Add(new ChatGPTMessageModel { role = "user", content = userMsg });

        var headers = new Dictionary<string, string>
        {
            { "Authorization", "Bearer " + APIKEY},
            { "Content-type", "application/json"},
            { "X-Slack-No-Retry", "1"}
        };

        //パラメータ設定
        var param = new ChatGPTCompletionRequestModel()
        {
            model = "gpt-3.5-turbo",
            messages = _msgList
        };

        var paramJson = JsonUtility.ToJson(param);

        Debug.Log("User:" + userMsg);

        using var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(paramJson)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new Exception();
        }
        else
        {
            var responseString = request.downloadHandler.text;
            var responseObject = JsonUtility.FromJson<ChatGPTResponseModel>(responseString);
            Debug.Log("ChatGPT:" + responseObject.choices[0].message.content);
            _msgList.Add(responseObject.choices[0].message);
            return responseObject;
        }
    }
}
