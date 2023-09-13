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
    float _rotX;
    Vector2 _lookInput;

    public void OnLook(InputAction.CallbackContext ctx) => _lookInput = ctx.ReadValue<Vector2>();

    private void Update()
    {
        transform.position = _playerTransform.position;
        float rotY = _lookInput.x * _turnSpeed;
        _rotX += _lookInput.y * _turnSpeed;
        _rotX = Mathf.Clamp(_rotX, _minTurnAngle, _maxTurnAngle);
        transform.eulerAngles = new Vector3(-_rotX, transform.eulerAngles.y + rotY, 0);
    }
}
