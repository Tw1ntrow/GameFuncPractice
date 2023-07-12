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
        SetText("あああああああああああああああああああああああああああああああああああ\nあああああああああああああああああああ\nあああああああああああ", () => Debug.Log("会話が終了しました。"));
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

            // 文字の表示速度に合わせて待つ
            yield return new WaitForSeconds(typingSpeed);
        }

        StopCoroutine(typingCoroutine);
        typingCoroutine = null;
    }

    public void OnClicked()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // コルーチンを止める
            text.text = sentences; // 全てのセリフを表示
            typingCoroutine = null; // コルーチンの参照をリセット
        }
        else
        {
            OnShowed?.Invoke();
        }
    }
}