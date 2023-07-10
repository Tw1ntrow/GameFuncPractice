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

    // ���x���ɕR�Â��A�Z�b�g���_�E�����[�h����
    // ��Ƀ��x���ɕR�Â��A�Z�b�g�̃��\�[�X���P�[�V�������擾���A
    // ���̃��\�[�X���P�[�V�����ɕR�Â��ˑ��֌W���X�g���[�W�Ƀ_�E�����[�h����
    // �X�g���[�W�Ƀ_�E�����[�h�����A�Z�b�g��LoadAssetAsync�Ŏ擾����
    public async void DownloadAsset(string label)
    {
        List<IResourceLocation> locations = new List<IResourceLocation>();
        var loadOperation = await Addressables.LoadResourceLocationsAsync(label).Task;
        locations = loadOperation.ToList();

        foreach (var location in locations)
        {
            var handle = Addressables.LoadAssetAsync<object>(location);

            // �_�E�����[�h�i�s��
            while (!handle.IsDone)
            {
                // �C�x���g�𔭍s
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