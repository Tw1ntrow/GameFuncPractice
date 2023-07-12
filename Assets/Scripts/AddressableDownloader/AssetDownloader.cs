using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetDownloader : MonoBehaviour
{
    public class DownloadInfo
    {
        public IResourceLocation location;
        public AsyncOperationHandle handle;
        public int totalAssets;
        public int completedAssets;
    }

    public event Action<DownloadInfo> ProgressChanged;
    public event Action<DownloadInfo> AssetDownloadCompleted;
    public event Action OnLoadCompleted;

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

    // ラベルに紐づくアセットのダウンロードを開始
    // ダウンロード進行状況はイベントで通知
    // DependencyHashCode毎にグループ化する事で、アセットバンドル毎に分割してロードができる
    public async void DownloadAsset(string label)
    {
        // ラベルに紐づくアセットのリソースロケーションを取得
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        // DependencyHashCodeでグループ化
        var locationGroups = locations.ToList().GroupBy(x => x.DependencyHashCode);

        int completedAssets = 0;
        int totalAssets = locationGroups.Count();

        foreach (var locationGroup in locationGroups)
        {
            // 各グループごとに依存関係のダウンロードを開始
            var handle = Addressables.DownloadDependenciesAsync(locationGroup.ToList());

            // ダウンロード進行状況
            while (!handle.IsDone)
            {
                // イベントを発行
                ProgressChanged?.Invoke(new DownloadInfo
                {
                    location = locationGroup.First(),
                    handle = handle,
                    totalAssets = totalAssets,
                    completedAssets = completedAssets
                });
                await Task.Yield();
            }

            completedAssets += 1;

            AssetDownloadCompleted?.Invoke(new DownloadInfo
            {
                location = locationGroup.First(),
                handle = handle,
                totalAssets = totalAssets,
                completedAssets = completedAssets
            });
        }

        OnLoadCompleted?.Invoke();
    }

    // アセットをロード
    public async Task<T> LoadAsset<T>(string key)
    {
        try
        {
            var result = await Addressables.LoadAssetAsync<T>(key).Task;
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset: {ex.Message}");
            throw new Exception();
        }
    }

    /// <summary>
    /// アセットをロード
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="asset"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<T> LoadAsset<T>(AssetReference asset)
    {
        try
        {
            var result = await Addressables.LoadAssetAsync<T>(asset).Task;
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset: {ex.Message}");
            throw new Exception();
        }
    }

    /// <summary>
    /// アセットをインスタンス化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<GameObject> InstantiateAsync<T>(string key)
    {
        try
        {
            var result = await Addressables.InstantiateAsync(key).Task;
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset: {ex.Message}");
            throw new Exception();
        }
    }
}