using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
    private string apiUrl = "http://localhost:8000/register"; // Laravel APIのエンドポイント

    public void RegisterUser(string username, string password)
    {
        StartCoroutine(RegisterCoroutine(username, password));
    }

    private IEnumerator RegisterCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("User registered successfully");
        }
    }
}
