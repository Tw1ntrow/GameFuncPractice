using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    private float sensitivity = 100;
    [SerializeField]
    private float loudnessThreshold = 1.0f;
    [SerializeField]
    private Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField]
    private Vector3 originalScale;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;

        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("マイクが見つかりません。");
            return;
        }

        string micName = Microphone.devices[0];
        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        audioSource.loop = true;

        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    void Update()
    {
        float loudness = GetAveragedVolume() * sensitivity;

        if (loudness > loudnessThreshold)
        {
            transform.localScale = maxScale;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}