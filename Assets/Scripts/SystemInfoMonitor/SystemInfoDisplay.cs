using UnityEngine;

public class SystemInfoDisplay : MonoBehaviour
{
    private Vector2 scrollPosition;

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(500), GUILayout.Height(300));

        GUILayout.Label($"OS Version: {SystemInfoHelper.GetOSVersion()}");
        GUILayout.Label($"CPU Info: {SystemInfoHelper.GetCPUInfo()}");
        GUILayout.Label($"GPU Info: {SystemInfoHelper.GetGPUInfo()}");
        GUILayout.Label($"RAM Info: {SystemInfoHelper.GetRAMInfo()}");
        GUILayout.Label($"Screen Info: {SystemInfoHelper.GetScreenInfo()}");
        GUILayout.Label($"Battery Info: {SystemInfoHelper.GetBatteryInfo()}");

        GUILayout.EndScrollView();
    }
}