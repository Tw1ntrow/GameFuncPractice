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

    // IResourceLocation
    public class PrioritizedResourceLocation
    {
        public IResourceLocation location;
        public int priority;
    }

    public void SetDownloadPriority(IResourceLocation location, int priority)
    {
        var prioritized = new PrioritizedResourceLocation() { location = location, priority = priority };
        prioritizedLocations.Add(prioritized);
    }

    private List<PrioritizedResourceLocation> prioritizedLocations = new List<PrioritizedResourceLocation>();

    public event Action<DownloadInfo> ProgressChanged;
    public event Action<DownloadInfo> AssetDownloadCompleted;
    public event Action OnLoadCompleted;

    public static AssetDownloader instance;


    private bool pauseDownload = false;


    public void PauseDownload()
    {
        pauseDownload = true;
    }

    public void ResumeDownload()
    {
        pauseDownload = false;
    }

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
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        // 優先順位に基づいてソートする
        var sortedLocations = locations
            .Select(loc => new PrioritizedResourceLocation
            {
                location = loc,
                priority = prioritizedLocations.FirstOrDefault(p => p.location.PrimaryKey == loc.PrimaryKey)?.priority ?? int.MaxValue
            })
            .OrderBy(p => p.priority)
            .Select(p => p.location)
            .ToList();

        // DependencyHashCodeでグループ化
        var locationGroups = sortedLocations.GroupBy(x => x.DependencyHashCode);

        int completedAssets = 0;
        int totalAssets = locationGroups.Count();

        foreach (var locationGroup in locationGroups)
        {
            // このループ内で一時停止の確認
            while (pauseDownload)
            {
                await Task.Delay(100);
            }

            var handle = Addressables.DownloadDependenciesAsync(locationGroup.ToList());

            // ダウンロード進行状況
            while (!handle.IsDone)
            {
                // 一時停止の確認
                while (pauseDownload)
                {
                    await Task.Delay(100);
                }

                ProgressChanged?.Invoke(new DownloadInfo
                {
                    location = locationGroup.First(),
                    handle = handle,
                    totalAssets = totalAssets,
                    completedAssets = completedAssets
                });
                await Task.Yield();
            }

            OnLoadCompleted?.Invoke();
        }
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

    /// <summary>
    /// 指定したアセットのキャッシュをクリアする
    /// </summary>
    /// <param name="key">キャッシュをクリアするアセットのキー</param>
    public async Task ClearCacheForAsset(string key)
    {
        // アセットのリソースロケーションを取得
        var locations = await Addressables.LoadResourceLocationsAsync(key).Task;
        foreach (var location in locations)
        {
            Addressables.ClearDependencyCacheAsync(location);
        }
    }

}