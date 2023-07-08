using UnityEngine;

/// <summary>
/// Loggerクラスで呼ばれたログを表示する
/// </summary>
public class LoggerDisplay : MonoBehaviour
{
    private int lineHeight = 20;

    // テスト用のログを出力
    private void Start()
    {
        Logger.Log("This is a test log.");
        Logger.LogWarning("This is a test warning.");
        Logger.LogError("This is a test error.");

        // ログのフィルタリングを設定
        Logger.SetFilter(Logger.LogType.Warning);

    }

    private void OnGUI()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        int currentLine = 0;

        // 各ログを表示
        foreach (var log in Logger.GetLogs())
        {
            // ログの種類に応じてテキスト色を変更
            switch (log.type)
            {
                case Logger.LogType.Log:
                    GUI.contentColor = Color.white;
                    break;
                case Logger.LogType.Warning:
                    GUI.contentColor = Color.yellow;
                    break;
                case Logger.LogType.Error:
                    GUI.contentColor = Color.red;
                    break;
            }

            // 背景
            GUI.backgroundColor = Color.gray;
            GUI.Box(new Rect(10, 10 + lineHeight * currentLine, 500, lineHeight), "");

            // ログメッセージを表示
            GUI.Label(new Rect(10, 10 + lineHeight * currentLine, 500, lineHeight), $"{log.timestamp}: {log.message}");

            // テキスト色と背景色を一度元に戻す
            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.white;

            // 行数を更新
            currentLine++;
        }
#endif
    }
}