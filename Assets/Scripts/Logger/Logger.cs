using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// カスタムロガー
/// </summary>
public static class Logger
{
    public class LogMessage
    {
        public string message;
        public LogType type;
        public string timestamp;
    }

    public enum LogType
    {
        Log,
        Warning,
        Error
    }

    private static List<LogMessage> logs = new List<LogMessage>();
    private static int maxLogs = 100;  // ログの最大保持数

    private static LogType filter = LogType.Log;  // このレベル以上のログのみ表示する

    public static void Log(string message)
    {
        AddLog(new LogMessage { message = message, type = LogType.Log, timestamp = System.DateTime.Now.ToString("HH:mm:ss") });
        Debug.Log(message);
    }

    public static void LogWarning(string message)
    {
        AddLog(new LogMessage { message = message, type = LogType.Warning, timestamp = System.DateTime.Now.ToString("HH:mm:ss") });
        Debug.LogWarning(message);
    }

    public static void LogError(string message)
    {
        AddLog(new LogMessage { message = message, type = LogType.Error, timestamp = System.DateTime.Now.ToString("HH:mm:ss") });
        Debug.LogError(message);
    }

    private static void AddLog(LogMessage logMessage)
    {
        // ログを追加
        logs.Add(logMessage);

        // ログ数が最大数を超えたら古いものから削除
        if (logs.Count > maxLogs)
        {
            logs.RemoveAt(0);
        }
    }

    public static void SetFilter(LogType logType)
    {
        filter = logType;
    }

    public static IEnumerable<LogMessage> GetLogs()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        // デベロップメントビルドまたはエディターで実行中の場合にのみログを返す
        return logs.Where(l => l.type >= filter);
#else
        // それ以外の場合は空のリストを返す
        return new List<LogMessage>();
#endif
    }
}