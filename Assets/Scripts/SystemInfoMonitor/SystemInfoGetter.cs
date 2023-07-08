using UnityEngine;

public static class SystemInfoHelper
{
    public static string GetOSVersion()
    {
        return SystemInfo.operatingSystem;
    }

    public static string GetCPUInfo()
    {
        return $"Type: {SystemInfo.processorType}, Count: {SystemInfo.processorCount}, Frequency: {SystemInfo.processorFrequency} MHz";
    }

    public static string GetGPUInfo()
    {
        return $"Name: {SystemInfo.graphicsDeviceName}, Memory: {SystemInfo.graphicsMemorySize} MB, Version: {SystemInfo.graphicsDeviceVersion}";
    }

    public static string GetRAMInfo()
    {
        return $"{SystemInfo.systemMemorySize} MB";
    }

    public static string GetScreenInfo()
    {
        return $"Resolution: {Screen.currentResolution}, DPI: {Screen.dpi}";
    }

    public static string GetBatteryInfo()
    {
        return $"Status: {SystemInfo.batteryStatus}, Level: {SystemInfo.batteryLevel * 100}%";
    }
}