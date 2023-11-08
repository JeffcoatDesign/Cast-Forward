using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private void OnEnable() => GameTimer.OnUpdateTimer += UpdateText;
    private void OnDisable() => GameTimer.OnUpdateTimer -= UpdateText;
    private void UpdateText(float currentTime, float startTime)
    {
        if (_text != null)
        {
            int minutes = (int)currentTime / 60;
            int seconds = (int)currentTime % 60;
            _text.text = (minutes.ToString("F0") + ":" + seconds.ToString("00"));
        }
    }
}
