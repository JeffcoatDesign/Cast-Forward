using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : Singleton<GameTimer>
{
    public delegate void UpdateTimer(float currentTime, float startTime);
    public static event UpdateTimer OnUpdateTimer;
    private float _startTime;
    private float _currentTime;

    private void OnEnable()
    {
        LevelGenerator.OnLevelGenerated += StartTimer;
    }

    private void OnDisable()
    {
        LevelGenerator.OnLevelGenerated -= StartTimer;
    }

    void StartTimer(LevelGenerator levelGenerator)
    {
        _startTime = Time.time;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        OnUpdateTimer?.Invoke(_currentTime, _startTime);
        //if (_timerText != null)
        //{
        //    int minutes = (int)_currentTime / 60;
        //    int seconds = (int)_currentTime % 60;
        //    _timerText.text = (minutes.ToString("F0") + ":" + seconds.ToString("00"));
        //}
    }
}
