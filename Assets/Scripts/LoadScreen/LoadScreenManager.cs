using System.Collections.Generic;
using UnityEngine;

public class LoadScreenManager : MonoBehaviour
{
    public static LoadScreenManager Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public LoadScreenView loadScreen;
    private List<LoadTask> loadTasks = new List<LoadTask>();

    private void Awake()
    {
        // �V���O���g���̏�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���[�h�����̊J�n
    public void StartLoad(IEnumerable<ILoadable> loadables)
    {
        // �V�����^�X�N���쐬���ă��[�h�J�n
        foreach (var loadable in loadables)
        {
            var task = new LoadTask(loadable);
            loadTasks.Add(task);
            StartCoroutine(task.StartLoad());
        }

        // ���[�h��ʂ�\��
        loadScreen.Show();
    }

    // ���[�h�̊������`�F�b�N
    private void Update()
    {
        // �S�Ẵ��[�h�^�X�N�̐i�s�x�̕��ς��v�Z
        float totalProgress = 0;
        foreach (var task in loadTasks)
        {
            totalProgress += task.GetProgress();
        }
        float averageProgress = totalProgress / loadTasks.Count;

        // ���[�h��ʂ̐i�s�x���X�V
        loadScreen.UpdateProgress(averageProgress);

        // �S�Ẵ��[�h�����������烍�[�h��ʂ��\���ɂ���
        if (averageProgress >= 1.0f)
        {
            loadScreen.Hide();
        }
    }
}