using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PauseGame(bool value);
    public static PauseGame OnPauseGame;
    private bool _canCast = true;
    private bool _isPaused = false;
    private int _pauseCount;
    private float _defaultTimeScale;
    public bool CanCast { get { return _canCast; } }
    public bool IsPaused { get { return _isPaused; } }

    public static GameManager instance;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetPause;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetPause;
    }
    private void Awake()
    {
        _defaultTimeScale = Time.timeScale;
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Pause(bool value)
    {
        if (value) _pauseCount++;
        else _pauseCount--;
        _isPaused = _pauseCount > 0;
        OnPauseGame?.Invoke(_isPaused);
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
    public void ResetPause(Scene scene, LoadSceneMode loadSceneMode)
    {
        _pauseCount = 0;
        _isPaused = false;
        Time.timeScale = _defaultTimeScale;

        if (scene.name == "Menu")
        {
            Destroy(gameObject);
        }
    }
    public void LockCasting(bool value)
    {
        _canCast = value;
    }
}
