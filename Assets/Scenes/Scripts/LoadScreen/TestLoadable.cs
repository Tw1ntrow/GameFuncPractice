using System.Collections;
using UnityEngine;

/// <summary>
/// ���[�h��ʃe�X�g�p
/// </summary>
public class TestLoadable : MonoBehaviour, ILoadable
{
    [SerializeField]
    private float loadTime = 5f; // �����[�h����

    [SerializeField]
    private LoadScreenManager loadScreenManager;

    //�@�e�X�g�p�Ƀ��[�h�J�n
    private void Start()
    {
        loadScreenManager.StartLoad(new ILoadable[] { this });
    }

    public IEnumerator Load()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadTime);
            yield return progress; // �i���󋵂�Ԃ�
        }

        yield return 1f; // ���[�h����
    }
}