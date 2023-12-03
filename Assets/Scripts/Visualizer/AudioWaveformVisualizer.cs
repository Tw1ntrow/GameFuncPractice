using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioWaveformVisualizer : MonoBehaviour
{
    private AudioSource audioSource;
    private float[] samples;
    private LineRenderer lineRenderer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samples = new float[256];// ”gŒ`‚Ì’·‚³
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        SetupLineRenderer();
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
            DrawWaveform();
        }
    }

    void SetupLineRenderer()
    {
        lineRenderer.positionCount = samples.Length;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    void DrawWaveform()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            Vector3 position = new Vector3(i * 0.1f, samples[i] * 10, 0);
            lineRenderer.SetPosition(i, position);
        }
    }
}