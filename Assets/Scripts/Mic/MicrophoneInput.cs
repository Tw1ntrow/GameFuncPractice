using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // �}�C�N���ڑ�����Ă��邩�m�F
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("�}�C�N��������܂���B");
            return;
        }

        // �f�t�H���g�̃}�C�N���g�p
        string micName = Microphone.devices[0];
        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        audioSource.loop = true;

        // �}�C�N�����������܂ő҂�
        // Update�ő҂��Ă�����
        while (!(Microphone.GetPosition(null) > 0))
        {
        }

        audioSource.Play();
    }
}