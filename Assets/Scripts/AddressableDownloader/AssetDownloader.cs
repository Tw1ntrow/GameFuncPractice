using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetDownloader:MonoBehaviour
{
    public class DownloadInfo
    {
        public IResourceLocation location;
        public AsyncOperationHandle handle;
        public int totalAssets;
    }

    public event Action<DownloadInfo> ProgressChanged;
    public event Action<DownloadInfo> AssetDownloadCompleted;
    public event Action OnloadCompleted;


    public static AssetDownloader instance;

    private void Awake()
    {
        //　シングルトンインスタンス生成
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //　インスタンスが生成されている場合はインスタンスを破棄
            Destroy(gameObject);
        }
    }

    // ラベルに紐づくアセットをダウンロードする
    // 先にラベルに紐づくアセットのリソースロケーションを取得し、
    // そのリソースロケーションに紐づく依存関係をストレージにダウンロードする
    // ストレージにダウンロードしたアセットはLoadAssetAsyncで取得する
    public async void DownloadAsset(string label)
    {
        List<IResourceLocation> locations = new List<IResourceLocation>();
        var loadOperation = await Addressables.LoadResourceLocationsAsync(label).Task;
        locations = loadOperation.ToList();

        foreach (var location in locations)
        {
            var handle = Addressables.LoadAssetAsync<object>(location);

            // ダウンロード進行状況
            while (!handle.IsDone)
            {
                // イベントを発行
                ProgressChanged?.Invoke(new DownloadInfo
                {
                    location = location,
                    handle = handle,
                    totalAssets = locations.Count
                });
                await System.Threading.Tasks.Task.Yield();
            }

            AssetDownloadCompleted?.Invoke(new DownloadInfo
            {
                location = location,
                handle = handle
            });
        }

        OnloadCompleted?.Invoke();
    }
}