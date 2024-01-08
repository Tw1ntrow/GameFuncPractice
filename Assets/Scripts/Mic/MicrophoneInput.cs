using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // マイクが接続されているか確認
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("マイクが見つかりません。");
            return;
        }

        // デフォルトのマイクを使用
        string micName = Microphone.devices[0];
        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        audioSource.loop = true;

        // マイクが準備されるまで待つ
        // Updateで待ってもいい
        while (!(Microphone.GetPosition(null) > 0))
        {
        }

        audioSource.Play();
    }
}