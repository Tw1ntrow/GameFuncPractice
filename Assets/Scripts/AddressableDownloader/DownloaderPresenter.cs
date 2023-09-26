using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloaderPresenter : MonoBehaviour
{
    [SerializeField]
    private DownloadProgressbar progressBar;
    [SerializeField]
    private UnityEngine.UI.Image image;



    void Start()
    {
        AssetDownloader.instance.ProgressChanged += UpdateProgress;
        AssetDownloader.instance.AssetDownloadCompleted += HandleAssetDownloadCompleted;
        AssetDownloader.instance.OnLoadCompleted += AllDownloadCompleted;
        AssetDownloader.instance.DownloadAsset("TestLabel");
    }

    void UpdateProgress(AssetDownloader.DownloadInfo info)
    {
        float individualProgress = info.handle.PercentComplete;
        float totalProgress = (float)info.completedAssets / info.totalAssets;

        progressBar.UpdateProgressSlider(individualProgress, totalProgress, $"{totalProgress * 100f}%");
    }

    void HandleAssetDownloadCompleted(AssetDownloader.DownloadInfo info)
    {
        UpdateProgress(info);
    }

    void AllDownloadCompleted()
    {
        progressBar.OnComplete();
        LoadTexture();
    }

    /// <summary>
    /// ダウンロードしたアセットをロード
    /// </summary>
    private async void LoadTexture()
    {
        image.sprite = await AssetDownloader.instance.LoadAsset<Sprite>("Assets/Texture/AddressableDownloader/G-GEAR_C1.jpg");
    }
}