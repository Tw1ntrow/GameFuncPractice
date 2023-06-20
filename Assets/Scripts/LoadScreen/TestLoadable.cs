using System.Collections;
using UnityEngine;

/// <summary>
/// ロード画面テスト用
/// </summary>
public class TestLoadable : MonoBehaviour, ILoadable
{
    [SerializeField]
    private float loadTime = 5f; // 仮ロード時間

    [SerializeField]
    private LoadScreenManager loadScreenManager;

    //　テスト用にロード開始
    private void Start()
    {
        loadScreenManager.StartLoad(new ILoadable[] { this });
    }

    public IEnumerator Load()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadTime);
            yield return progress; // 進捗状況を返す
        }

        yield return 1f; // ロード完了
    }
}