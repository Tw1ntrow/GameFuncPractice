using System.Collections;
using UnityEngine;

/// <summary>
/// ロード処理を扱う
/// </summary>
public class LoadTask
{
    private ILoadable loadable;  // ロードする対象
    private float progress = 0;  // ロードの進行度（0から1）

    public LoadTask(ILoadable loadable)
    {
        this.loadable = loadable;
    }

    // ロードの開始
    public IEnumerator StartLoad()
    {
        // ロード処理を開始
        IEnumerator loadCoroutine = loadable.Load();

        while (loadCoroutine.MoveNext())
        {
            // 進行度が帰ってくるのを想定している
            progress = (float)loadCoroutine.Current;
            yield return null;
        }
    }

    // ロードの進行度を取得
    public float GetProgress()
    {
        return progress;
    }
}