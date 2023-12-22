using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SlackIntegration : MonoBehaviour
{
    private const string webhookUrl = "https://hooks.slack.com/services/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";

    private void Start()
    {
        SendSlackMessage("Game started!");
    }

    public void SendSlackMessage(string message)
    {
        StartCoroutine(PostToSlack(message));
    }

    IEnumerator PostToSlack(string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("payload", $"{{\"text\":\"{message}\"}}");

        using (UnityWebRequest www = UnityWebRequest.Post(webhookUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to send message to Slack: {www.error}");
            }
            else
            {
                Debug.Log("Message sent to Slack successfully!");
            }
        }
    }
}