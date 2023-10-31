using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(AudioType.Master.ToString()))
            audioMixer.SetFloat(AudioType.Master.ToString(), PlayerPrefs.GetFloat(AudioType.Master.ToString()));
        if (PlayerPrefs.HasKey(AudioType.SFX.ToString()))
            audioMixer.SetFloat(AudioType.SFX.ToString(), PlayerPrefs.GetFloat(AudioType.SFX.ToString()));
        if (PlayerPrefs.HasKey(AudioType.Music.ToString()))
            audioMixer.SetFloat(AudioType.Music.ToString(), PlayerPrefs.GetFloat(AudioType.Music.ToString()));
        PlayerPrefs.Save();
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
}
