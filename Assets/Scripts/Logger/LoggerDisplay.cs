using UnityEngine;

/// <summary>
/// Logger�N���X�ŌĂ΂ꂽ���O��\������
/// </summary>
public class LoggerDisplay : MonoBehaviour
{
    private int lineHeight = 20;

    // �e�X�g�p�̃��O���o��
    private void Start()
    {
        Logger.Log("This is a test log.");
        Logger.LogWarning("This is a test warning.");
        Logger.LogError("This is a test error.");

        // ���O�̃t�B���^�����O��ݒ�
        Logger.SetFilter(Logger.LogType.Warning);

    }

    private void OnGUI()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        int currentLine = 0;

        // �e���O��\��
        foreach (var log in Logger.GetLogs())
        {
            // ���O�̎�ނɉ����ăe�L�X�g�F��ύX
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

            // �w�i
            GUI.backgroundColor = Color.gray;
            GUI.Box(new Rect(10, 10 + lineHeight * currentLine, 500, lineHeight), "");

            // ���O���b�Z�[�W��\��
            GUI.Label(new Rect(10, 10 + lineHeight * currentLine, 500, lineHeight), $"{log.timestamp}: {log.message}");

            // �e�L�X�g�F�Ɣw�i�F����x���ɖ߂�
            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.white;

            // �s�����X�V
            currentLine++;
        }
#endif
    }
}