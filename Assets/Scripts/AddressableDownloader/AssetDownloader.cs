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
        //�@�V���O���g���C���X�^���X����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //�@�C���X�^���X����������Ă���ꍇ�̓C���X�^���X��j��
            Destroy(gameObject);
        }
    }

    // ���x���ɕR�Â��A�Z�b�g�̃_�E�����[�h���J�n
    // �_�E�����[�h�i�s�󋵂̓C�x���g�Œʒm
    // DependencyHashCode���ɃO���[�v�����鎖�ŁA�A�Z�b�g�o���h�����ɕ������ă��[�h���ł���
    public async void DownloadAsset(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        // �D�揇�ʂɊ�Â��ă\�[�g����
        var sortedLocations = locations
            .Select(loc => new PrioritizedResourceLocation
            {
                location = loc,
                priority = prioritizedLocations.FirstOrDefault(p => p.location.PrimaryKey == loc.PrimaryKey)?.priority ?? int.MaxValue
            })
            .OrderBy(p => p.priority)
            .Select(p => p.location)
            .ToList();

        // DependencyHashCode�ŃO���[�v��
        var locationGroups = sortedLocations.GroupBy(x => x.DependencyHashCode);

        int completedAssets = 0;
        int totalAssets = locationGroups.Count();

        foreach (var locationGroup in locationGroups)
        {
            // ���̃��[�v���ňꎞ��~�̊m�F
            while (pauseDownload)
            {
                await Task.Delay(100);
            }

            var handle = Addressables.DownloadDependenciesAsync(locationGroup.ToList());

            // �_�E�����[�h�i�s��
            while (!handle.IsDone)
            {
                // �ꎞ��~�̊m�F
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

    // �A�Z�b�g�����[�h
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
    /// �A�Z�b�g�����[�h
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
    /// �A�Z�b�g���C���X�^���X��
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
    /// �w�肵���A�Z�b�g�̃L���b�V�����N���A����
    /// </summary>
    /// <param name="key">�L���b�V�����N���A����A�Z�b�g�̃L�[</param>
    public async Task ClearCacheForAsset(string key)
    {
        // �A�Z�b�g�̃��\�[�X���P�[�V�������擾
        var locations = await Addressables.LoadResourceLocationsAsync(key).Task;
        foreach (var location in locations)
        {
            Addressables.ClearDependencyCacheAsync(location);
        }
    }

}