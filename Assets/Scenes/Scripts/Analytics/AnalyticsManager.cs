using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
        }
    }

    public static void SendEvent()
    {
        var parameters = new Dictionary<string, object>();
        parameters["TestEvent"] = "テストイベント";
        AnalyticsService.Instance.CustomData("TestEvent", parameters);
    }
}
