using System.Collections;
using UnityEngine;

/// <summary>
/// ���[�h����������
/// </summary>
public class LoadTask
{
    private ILoadable loadable;  // ���[�h����Ώ�
    private float progress = 0;  // ���[�h�̐i�s�x�i0����1�j

    public LoadTask(ILoadable loadable)
    {
        this.loadable = loadable;
    }

    // ���[�h�̊J�n
    public IEnumerator StartLoad()
    {
        // ���[�h�������J�n
        IEnumerator loadCoroutine = loadable.Load();

        while (loadCoroutine.MoveNext())
        {
            // �i�s�x���A���Ă���̂�z�肵�Ă���
            progress = (float)loadCoroutine.Current;
            yield return null;
        }
    }

    // ���[�h�̐i�s�x���擾
    public float GetProgress()
    {
        return progress;
    }
}