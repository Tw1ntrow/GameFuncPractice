using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体のロード処理を管理する
/// ビューの制御もここで行う
/// シングルトンの方が良いかも
/// </summary>
public class LoadScreenManager : MonoBehaviour
{
    public LoadScreenView loadScreen;

    private List<LoadTask> loadTasks = new List<LoadTask>();

    // ロード処理の開始
    public void StartLoad(IEnumerable<ILoadable> loadables)
    {

        // 新しいタスクを作成してロード開始
        foreach (var loadable in loadables)
        {
            var task = new LoadTask(loadable);
            loadTasks.Add(task);
            StartCoroutine(task.StartLoad());
        }

        // ロード画面を表示
        loadScreen.Show();
    }

    // ロードの完了をチェック
    private void Update()
    {
        // 全てのロードタスクの進行度の平均を計算
        float totalProgress = 0;
        foreach (var task in loadTasks)
        {
            totalProgress += task.GetProgress();
        }
        float averageProgress = totalProgress / loadTasks.Count;

        // ロード画面の進行度を更新
        loadScreen.UpdateProgress(averageProgress);

        // 全てのロードが完了したらロード画面を非表示にする
        if (averageProgress >= 1.0f)
        {
            loadScreen.Hide();
        }
    }
}