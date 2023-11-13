using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatPlayerPrefSlider : MonoBehaviour
{
    public string playerPrefKey;
    public Slider slider;
    private void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefKey))
            slider.value = PlayerPrefs.GetFloat(playerPrefKey);
        else
            PlayerPrefs.SetFloat(playerPrefKey, slider.value);
    }
    public void ChangeSliderValue(float value)
    {
        PlayerPrefs.SetFloat(playerPrefKey, value);
    }
}
