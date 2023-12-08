using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// �J�X�^�����K�[
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
    private static int maxLogs = 100;  // ���O�̍ő�ێ���

    private static LogType filter = LogType.Log;  // ���̃��x���ȏ�̃��O�̂ݕ\������

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
        // ���O��ǉ�
        logs.Add(logMessage);

        // ���O�����ő吔�𒴂�����Â����̂���폜
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
        // �f�x���b�v�����g�r���h�܂��̓G�f�B�^�[�Ŏ��s���̏ꍇ�ɂ̂݃��O��Ԃ�
        return logs.Where(l => l.type >= filter);
#else
        // ����ȊO�̏ꍇ�͋�̃��X�g��Ԃ�
        return new List<LogMessage>();
#endif
    }
}