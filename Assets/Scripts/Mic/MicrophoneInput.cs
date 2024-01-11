using UnityEngine;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    private AudioSource audioSource;
    private KeywordRecognizer keywordRecognizer;
    public string[] keywords = new string[] { "start", "stop", "pause" };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("認識されたフレーズ: " + args.text);
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.OnPhraseRecognized -= OnPhraseRecognized;
            keywordRecognizer.Stop();
        }
    }
}