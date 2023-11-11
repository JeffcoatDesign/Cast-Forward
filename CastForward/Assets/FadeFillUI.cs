using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFillUI : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.7f;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private bool _fadeOnStart;

    private void Awake()
    {
        if (_fadeOnStart) {
            _fadeImage.fillAmount = 0;
            LevelGenerator.OnLevelGenerated += StartFadeIn;
        }
    }
    private void OnDisable()
    {
        if (_fadeOnStart) LevelGenerator.OnLevelGenerated -= StartFadeIn;
    }

    public void StartFadeIn () => StartCoroutine(FadeIn());
    public void StartFadeIn (LevelGenerator levelGenerator) => StartCoroutine(FadeIn());

    public void StartFadeOut() => StartCoroutine(FadeOut());

    private IEnumerator FadeIn()
    {
        float fadeAmount = 0f;
        float startTime = Time.time;
        while (fadeAmount < 1)
        {
            fadeAmount = Mathf.Lerp(0f, 1f, (Time.time - startTime) / _fadeTime);
            _fadeImage.fillAmount = fadeAmount;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator FadeOut()
    {
        float fadeAmount = 1f;
        float startTime = Time.time;
        while (fadeAmount > 0)
        {
            fadeAmount = Mathf.Lerp(1f, 0f, (Time.time - startTime) / _fadeTime);
            _fadeImage.fillAmount = fadeAmount;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
