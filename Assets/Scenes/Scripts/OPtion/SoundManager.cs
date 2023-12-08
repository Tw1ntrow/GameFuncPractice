using System.Data;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private AudioSource seSource;

    [SerializeField]
    private AudioClip[] bgmClips;

    [SerializeField]
    private AudioClip[] seClips;

    public float BgmVolume { get => bgmSource.volume;} 
    public float SEVolume { get => seSource.volume; }

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
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

    public void PlayBgm(int index)
    {
        bgmSource.clip = bgmClips[index];
        bgmSource.Play();
    }

    public void PlaySe(int index)
    {
        seSource.clip = seClips[index];
        seSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void StopSe()
    {
        seSource.Stop();
    }

    public void PauseBgm()
    {
        bgmSource.Pause();
    }

    public void PauseSe()
    {
        seSource.Pause();
    }

    public void UnPauseBgm()
    {
        bgmSource.UnPause();
    }

    public void UnPauseSe()
    {
        seSource.UnPause();
    }

    public void SetBgmLoop(bool isLoop)
    {
        bgmSource.loop = isLoop;
    }

    public void SetSeLoop(bool isLoop)
    {
        seSource.loop = isLoop;
    }

    public void SetBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSeVolume(float volume)
    {
        seSource.volume = volume;
    }
}
