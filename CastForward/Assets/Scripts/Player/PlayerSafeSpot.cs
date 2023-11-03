using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafeSpot : MonoBehaviour
{
    [SerializeField] private float pollTime;
    [SerializeField] private PlayerController playerController;
    private float _lastPollTime;
    private Vector3 _lastSafeSpot;
    private bool isStarted = false;
    public Vector3 GetLastSafeSpot { get { return _lastSafeSpot; } }
    public static PlayerSafeSpot playerSafeSpot;
    private void Awake()
    {
        playerSafeSpot = this;
    }

    private void OnEnable()
    {
        LevelGenerator.OnLevelGenerated += StartPoll;
    }

    private void OnDisable()
    {
        LevelGenerator.OnLevelGenerated -= StartPoll;
    }

    private void StartPoll(LevelGenerator levelGenerator)
    {
        _lastPollTime = Time.time;
        isStarted = true;
        _lastSafeSpot = transform.position;
    }

    private void Update() {
        if (isStarted && Time.time - _lastPollTime > pollTime)
        {
            if (playerController.isGrounded)
            {
                _lastPollTime = Time.time;
                _lastSafeSpot = transform.position;
            }
        }
    }

}
