using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ロード画面の見た目を実装する
/// </summary>
public class LoadScreenView : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScreenObject;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private Text progressText;
    // ロード画面を表示する
    public void Show()
    {
        loadScreenObject.SetActive(true);
    }

    // ロード画面を非表示にする
    public void Hide()
    {
        loadScreenObject.SetActive(false);
    }

    // ロード進行度を更新する
    public void UpdateProgress(float progress)
    {
        // 見た目の更新
        progressBar.value = progress;
        progressText.text = string.Format("{0:0}%", progress * 100);
    }
}