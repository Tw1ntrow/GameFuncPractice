using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloaderPresenter : MonoBehaviour
{
    [SerializeField]
    private DownloadProgressbar progressBar;

    private int completedAssets;

    void Start()
    {
        AssetDownloader.instance.ProgressChanged += UpdateProgress;
        AssetDownloader.instance.AssetDownloadCompleted += HandleAssetDownloadCompleted;
        AssetDownloader.instance.OnloadCompleted += AllDownloadCompleted;
        AssetDownloader.instance.DownloadAsset("TestLabel");
    }

    void UpdateProgress(AssetDownloader.DownloadInfo info)
    {
        float individualProgress = info.handle.PercentComplete;

        float totalProgress = (float)completedAssets / info.totalAssets;

        progressBar.UpdateProgressSlioder(individualProgress, totalProgress, $"{totalProgress * 100f}%");
    }

    void HandleAssetDownloadCompleted(AssetDownloader.DownloadInfo info)
    {
        completedAssets += 1;
        UpdateProgress(info);
    }

    void AllDownloadCompleted()
    {
        progressBar.OnComplete();
    }
}