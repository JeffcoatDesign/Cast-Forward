using PlayerStates;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private Transform _vCamTransform;
    [SerializeField] private float _playerSpeed = 100f;
    [SerializeField] private float _strafeModifier = 0.6f;
    [SerializeField] private float _sprintModifier = 2f;
    [SerializeField] private float _jumpPower = 200f;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _sprintActive = false;

    private Vector2 _movementInput;

    public bool isGrounded = true;
    public bool jumpPressed = false;

    public float PlayerSpeed { get { return _playerSpeed; } }
    public float StrafeModifier { get { return _strafeModifier; } }
    public float SprintSpeed { get {
            if (_sprintActive) return _sprintModifier;
            else return 1f;
        } }
    public float JumpPower { get { return _jumpPower; } }
    public LayerMask WhatIsWall { get { return _whatIsWall; } }
    public LayerMask WhatIsGround { get { return _whatIsGround; } }
    public Vector2 MovementInput { get { return _movementInput; } }
    public Quaternion CameraForward { get {
            Quaternion flattened = Quaternion.LookRotation(-Vector3.up, _vCamTransform.forward) * Quaternion.Euler(-90f, 0, 0);
            return flattened; } }

    IPlayerState _currentState = new IStandingState();
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _currentState.Enter(this);
    }

    public void OnMove(InputAction.CallbackContext ctx) => _movementInput = ctx.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext ctx) => jumpPressed = ctx.ReadValueAsButton();
    public void OnSprint(InputAction.CallbackContext ctx) => _sprintActive = ctx.ReadValueAsButton();

    public void SetState(IPlayerState playerState)
    {
        if (playerState != null)
        {
            _currentState.Exit();
            _currentState = playerState;
            _currentState.Enter(this);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.1f);
        transform.rotation = CameraForward;
        _currentState.HandleInput();
    }
}
