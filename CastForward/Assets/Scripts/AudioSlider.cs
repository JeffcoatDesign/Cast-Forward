using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour {
    [SerializeField] AudioType audioType;
    [SerializeField] Slider slider;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(audioType.ToString()))
            slider.value = PlayerPrefs.GetFloat(audioType.ToString());
    }
    public void SetAudioValue(float value) {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.SetVolume(audioType, value);
            if (value <= -20f) AudioManager.Instance.SetMute(audioType, true);
            else AudioManager.Instance.SetMute(audioType, false);
        } 
    }
}