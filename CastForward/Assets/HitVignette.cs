using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HitVignette : MonoBehaviour
{
    [SerializeField] private VolumeProfile profile;
    [SerializeField] private float hitTime = 1f;
    [SerializeField] private float maxIntenstity = 0.5f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerEntity.OnPlayerHit += PlayerHit;
    }
    private void OnDisable()
    {
        PlayerEntity.OnPlayerHit -= PlayerHit;
    }
    private void PlayerHit()
    {
        StartCoroutine(nameof(OnHit));
    }
    private void Start()
    {
        Vignette vignette;
        if (profile.TryGet(out vignette))
            vignette.intensity.value = 0;
    }

    private IEnumerator OnHit()
    {
        float startTime = Time.time;
        Vignette vignette;
        if (profile.TryGet(out vignette)) { 
            while (Time.time - startTime < hitTime)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, maxIntenstity, (Time.time - startTime) / hitTime);
                yield return new WaitForEndOfFrame(); 
            }
            startTime = Time.time;

            while (Time.time - startTime < hitTime)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, (Time.time - startTime) / hitTime);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }
}
