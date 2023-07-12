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
        // ���x���ɕR�Â��A�Z�b�g�̃��\�[�X���P�[�V�������擾
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        // DependencyHashCode�ŃO���[�v��
        var locationGroups = locations.ToList().GroupBy(x => x.DependencyHashCode);

        int completedAssets = 0;
        int totalAssets = locationGroups.Count();

        foreach (var locationGroup in locationGroups)
        {
            // �e�O���[�v���ƂɈˑ��֌W�̃_�E�����[�h���J�n
            var handle = Addressables.DownloadDependenciesAsync(locationGroup.ToList());

            // �_�E�����[�h�i�s��
            while (!handle.IsDone)
            {
                // �C�x���g�𔭍s
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
}