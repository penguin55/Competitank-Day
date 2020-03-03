using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioLibrary library;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private Dictionary<string, AudioData> audiosDict;

    void Initialize()
    {
        audiosDict = new Dictionary<string, AudioData>();
        foreach (AudioData data in library.audios)
        {
            audiosDict.Add(data.audioName, data);
        } 
    }

    private void Start()
    {
        Initialize();
        if (instance == null)
        {
            instance = this;
            PlayBGM("BGM");
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }


    public void PlayOneShotSFX(string name)
    {
        sfxSource.volume = GetVolume(name);
        sfxSource.PlayOneShot(GetAudio(name));
    }

    public void PlayOneShotSFXWithSource(ref AudioSource audioSource, string name)
    {
        audioSource.volume = GetVolume(name);
        audioSource.PlayOneShot(GetAudio(name));
    }

    private void PlayBGM(string name)
    {
        bgmSource.clip = GetAudio(name);
        bgmSource.volume = GetVolume(name);
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private AudioClip GetAudio(string name)
    {
        return audiosDict[name].clip;
    }

    private float GetVolume(string name)
    {
        return audiosDict[name].offsetVolume;
    }

}
