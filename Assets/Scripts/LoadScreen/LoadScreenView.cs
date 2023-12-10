using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���[�h��ʂ̌����ڂ���������
/// </summary>
public class LoadScreenView : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScreenObject;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private Text progressText;
    // ���[�h��ʂ�\������
    public void Show()
    {
        loadScreenObject.SetActive(true);
    }

    // ���[�h��ʂ��\���ɂ���
    public void Hide()
    {
        loadScreenObject.SetActive(false);
    }

    // ���[�h�i�s�x���X�V����
    public void UpdateProgress(float progress)
    {
        // �����ڂ̍X�V
        progressBar.value = progress;
        progressText.text = string.Format("{0:0}%", progress * 100);
    }
}