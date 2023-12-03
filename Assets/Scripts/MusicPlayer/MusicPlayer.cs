using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] musicTracks;
    [SerializeField]
    private Slider musicSlider;

    private int currentTrackIndex = 0;
    private bool isDragging = false;

    void Start()
    {
        if (musicTracks.Length > 0)
        {
            SetAudioClip(musicTracks[currentTrackIndex]);
        }
    }

    void Update()
    {
        if (!isDragging && audioSource.isPlaying)
        {
            musicSlider.value = audioSource.time;
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void HandleMusicSliderValueChanged(float value)
    {
        if (isDragging)
        {
            audioSource.time = value;
        }
    }

    public void OnSliderPointerDown()
    {
        isDragging = true;
    }

    public void OnSliderPointerUp()
    {
        isDragging = false;
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        SetAudioClip(musicTracks[currentTrackIndex]);
    }

    public void PreviousTrack()
    {
        if (audioSource.time > 3f || currentTrackIndex == 0)
        {
            ResetTrack();
        }
        else
        {
            currentTrackIndex = (currentTrackIndex - 1 + musicTracks.Length) % musicTracks.Length;
            SetAudioClip(musicTracks[currentTrackIndex]);
        }
    }

    private void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        ResetTrack();
    }

    private void ResetTrack()
    {
        musicSlider.maxValue = audioSource.clip.length;
        musicSlider.value = 0;
        audioSource.time = 0;
    }
}