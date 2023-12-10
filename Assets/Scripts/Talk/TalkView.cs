using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TalkView : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private float typingSpeed = 0.05f;

    private string sentences;

    public Action OnShowed;

    private Coroutine typingCoroutine;

    private void Start()
    {
        SetText("����������������������������������������������������������������������\n��������������������������������������\n����������������������", () => Debug.Log("��b���I�����܂����B"));
    }

    public void SetText(string sentences, Action OnShowed)
    {
        this.sentences = sentences;
        this.OnShowed = OnShowed;
        StartTyping();
    }

    public void StartTyping()
    {
        typingCoroutine = StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences)
        {
            if (letter == '\n' || letter == '\r')
            {
                text.text += "\n";
            }
            else
            {
                text.text += letter;
            }

            // �����̕\�����x�ɍ��킹�đ҂�
            yield return new WaitForSeconds(typingSpeed);
        }

        StopCoroutine(typingCoroutine);
        typingCoroutine = null;
    }

    public void OnClicked()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // �R���[�`�����~�߂�
            text.text = sentences; // �S�ẴZ���t��\��
            typingCoroutine = null; // �R���[�`���̎Q�Ƃ����Z�b�g
        }
        else
        {
            OnShowed?.Invoke();
        }
    }
}