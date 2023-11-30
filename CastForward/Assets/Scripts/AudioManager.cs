using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public enum AudioType { 
    Master,
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float fadeSpeed = 0.1f;
    [SerializeField] private GameObject audioSourcePrefab;
    public static AudioManager Instance;

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
    
    void Start()
    {
        GetFloatFromPrefs(AudioType.Master);
        GetBoolFromPrefs(AudioType.Master);
        GetFloatFromPrefs(AudioType.Music);
        GetBoolFromPrefs(AudioType.Music);
        GetFloatFromPrefs(AudioType.SFX);
        GetBoolFromPrefs(AudioType.SFX);
        PlayerPrefs.Save();
    }

    private void GetFloatFromPrefs(AudioType audioType)
    {
        if (PlayerPrefs.HasKey(audioType.ToString()))
            audioMixer.SetFloat(audioType.ToString(), PlayerPrefs.GetFloat(audioType.ToString()));
    }
    private void GetBoolFromPrefs(AudioType audioType)
    {
        string key = audioType.ToString() + "Muted";
        if (PlayerPrefs.HasKey(key)) {
            if (PlayerPrefs.GetInt(key, 0) == 0)
                audioMixer.SetFloat(audioType.ToString(), -80f);
        }
    }

    public float PlaySound(string audioPath, AudioType audioType, Transform parent)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
        if (audioClip == null) return 0;
        AudioSource audioSource = Instantiate(audioSourcePrefab, parent).GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        return audioClip.length;
    }

    public float PlaySound(AudioClip audioClip, AudioType audioType, Transform parent)
    {
        if (audioClip == null) return 0;
        AudioSource audioSource = Instantiate(audioSourcePrefab, parent).GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        return audioClip.length;
    }

    public void SetVolume(AudioType audioType, float value)
    {
        PlayerPrefs.SetFloat(audioType.ToString(), value);
        audioMixer.SetFloat(audioType.ToString(), value);
        PlayerPrefs.Save();
    }

    public void PlaySong (string songPath)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(songPath);
        if (audioClip != null) StartCoroutine(nameof(FadeMusic), audioClip);
    }

    public void PlaySong (AudioClip audioClip)
    {
        if (audioClip != null) StartCoroutine(nameof(FadeMusic), audioClip);
    }

    public IEnumerator FadeMusic (AudioClip audioClip)
    {
        float volume = 1f;
        while (volume > 0f)
        {
            volume -= 0.1f;
            musicSource.volume = volume;
            yield return new WaitForSeconds(fadeSpeed);
        }
        musicSource.clip = audioClip;
        if (!musicSource.isPlaying) musicSource.Play();
        volume = 0f;
        musicSource.volume = volume;
        while (volume < 1f)
        {
            volume += 0.1f;
            musicSource.volume = volume;
            musicSource.volume = Mathf.Clamp(musicSource.volume, 0f, 1f);
            yield return new WaitForSeconds(fadeSpeed);
        }
        yield return null;
    }

     public void SetMute(AudioType audioType, bool v)
    {
        string key = audioType.ToString() + "Muted";
        if (v)
        {
            audioMixer.SetFloat(audioType.ToString(), -80);
            PlayerPrefs.SetInt(key, 0);
            PlayerPrefs.Save();
        }
        else
        {
            audioMixer.SetFloat(audioType.ToString(), PlayerPrefs.GetFloat(audioType.ToString()));
            PlayerPrefs.SetInt(key, 1);
            PlayerPrefs.Save();
        }
        GetBoolFromPrefs(audioType);
    }
}
