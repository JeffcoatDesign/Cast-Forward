using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerationUI : MonoBehaviour
{
    public Slider slider;
    [SerializeField] GameObject screen;
    private void OnEnable()
    {
        LevelGenerator.OnLevelGenStarted += OnStartGenerating;
        LevelGenerator.OnLevelGenProgress += ProgressSlider;
        LevelGenerator.OnLevelGenerated += OnFinishGenerating;
    }
    private void OnDisable()
    {
        LevelGenerator.OnLevelGenStarted -= OnStartGenerating;
        LevelGenerator.OnLevelGenProgress -= ProgressSlider;
        LevelGenerator.OnLevelGenerated -= OnFinishGenerating;
    }
    private void ProgressSlider (float current, float max) {
        slider.maxValue = max;
        slider.value = current;
    }
    private void OnStartGenerating ()
    {
        screen.SetActive(true);
    }
    private void OnFinishGenerating (LevelGenerator levelGenerator)
    {
        screen.SetActive(false);
    }
}
