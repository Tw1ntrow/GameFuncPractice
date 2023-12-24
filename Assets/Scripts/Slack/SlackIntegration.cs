using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SlackIntegration : MonoBehaviour
{
    private const string webhookUrl = "https://hooks.slack.com/services/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";

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            StartCoroutine(SendSlackNotificationDelayed(logString));
        }
    }

    IEnumerator SendSlackNotificationDelayed(string logMessage)
    {
        // ƒrƒ‹ƒhŠ®—¹’Ê’m’x‰„
        yield return new WaitForSeconds(1);

        string payload = "{\"text\": \"Unity Build Completed:\\n" + logMessage + "\"}";

        UnityWebRequest request = new UnityWebRequest(webhookUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(payload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");

        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Slack Notification Sent");
        }
    }


    public void OnBuildComplete(string logMessage)
    {
        StartCoroutine(SendSlackNotificationDelayed(logMessage));
    }
}