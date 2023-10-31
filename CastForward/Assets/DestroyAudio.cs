using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DestroyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    private void Update()
    {
        if (!_source.isPlaying) Destroy(gameObject);
    }
}
