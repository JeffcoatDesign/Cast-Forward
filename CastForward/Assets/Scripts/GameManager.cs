using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _canCast = true;
    private bool _isPaused = false;
    private float _defaultTimeScale;
    public bool CanCast { get { return _canCast; } }
    public bool IsPaused { get { return _isPaused; } }

    public static GameManager instance;
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

    private void OnEnable()
    {
        PlayerEntity.OnPlayerDeath += GameOver;
    }
    private void OnDisable()
    {
        PlayerEntity.OnPlayerDeath -= GameOver;
    }
    public void PauseGame(bool value)
    {
        _isPaused = value;
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
    public void LockCasting(bool value)
    {
        _canCast = value;
    }

    void GameOver()
    {
        SceneManager.LoadScene("Menu");
        //TODO: Pause game and show summary
    }
}
