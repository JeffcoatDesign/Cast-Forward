using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _minTurnAngle = -90.0f;
    [SerializeField] private float _maxTurnAngle = 90.0f;
    private readonly float _yOffset = .4f;
    private float _yScale;
    float _rotX;
    Vector2 _lookInput;

    private void Start()
    {
        _yScale = _playerTransform.localScale.y;
    }

    public void OnLook(InputAction.CallbackContext ctx) => _lookInput = ctx.ReadValue<Vector2>();

    private void Update()
    {
        _turnSpeed = PlayerPrefs.GetFloat("Camera Sensitivity", _turnSpeed);
        if (GameManager.instance.IsPaused) return;
        transform.position = _playerTransform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + (_yOffset + 1) * (_playerTransform.localScale.y / _yScale), transform.position.z);
        float rotY = _lookInput.x * _turnSpeed;
        _rotX += _lookInput.y * _turnSpeed;
        _rotX = Mathf.Clamp(_rotX, _minTurnAngle, _maxTurnAngle);
        transform.eulerAngles = new Vector3(-_rotX, transform.eulerAngles.y + rotY, 0);
    }
}
