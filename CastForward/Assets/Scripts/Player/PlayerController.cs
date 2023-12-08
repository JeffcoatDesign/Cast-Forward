using PlayerStates;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public delegate void SetFocus(Interactable focus);
    public static event SetFocus OnSetFocus;

    public Rigidbody rb;
    [SerializeField] private PlayerVCamController _playerVCamController;
    [SerializeField] private float _playerSpeed = 100f;
    [SerializeField] private float _strafeModifier = 0.6f;
    [SerializeField] private float _reverseModifier = 0.5f;
    [SerializeField] private float _sprintModifier = 2f;
    [SerializeField] private float _crouchModifier = 0.6f;
    [SerializeField] private float _jumpPower = 200f;
    [SerializeField] private float _maxWallRunTime = 4f;
    [SerializeField] private float _wallDutchAngle = 10f;
    [SerializeField] private float _maxSlopeAngle = 10f;
    [SerializeField] private float _maxInteractDistance = 2f;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsInteractable;
    [SerializeField] private LayerMask _whatBlocksStanding;
    [SerializeField] private Transform _playerCursor;
    private bool _sprintActive = false;
    private bool _crouchActive = false;

    private Vector2 _movementInput;

    public bool isGrounded = true;
    public bool jumpPressed = false;
    public RaycastHit slopeHit;

    public float PlayerSpeed { get { return _playerSpeed * Time.deltaTime; } }
    public float ReverseModifier { get { return _reverseModifier; } }
    public float StrafeModifier { get { return _strafeModifier; } }
    public float CrouchModifier { get { return _crouchModifier; } }
    public float SprintSpeed { get {
            if (_sprintActive) return _sprintModifier;
            else return 1f;
        } }
    public bool IsCrouching { get { return _crouchActive; } }
    public float JumpPower { get { return _jumpPower; } }
    public float MaxWallRunTime { get { return _maxWallRunTime; } }
    public float WallDutchAngle { get { return _wallDutchAngle; } }
    public LayerMask WhatIsWall { get { return _whatIsWall; } }
    public LayerMask WhatIsGround { get { return _whatIsGround; } }
    public LayerMask WhatBlocksStanding { get { return _whatBlocksStanding; } }
    public Vector2 MovementInput { get { return _movementInput; } }
    public PlayerVCamController PlayerVCamController { get { return _playerVCamController; } }
    public Quaternion CameraForward { get {
            Quaternion flattened = Quaternion.LookRotation(-Vector3.up, _playerVCamController.transform.forward) * Quaternion.Euler(-90f, 0, 0);
            return flattened; } }

    IPlayerState _currentState = new PlayerStandingState();
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _currentState.Enter(this);
    }

    public void OnMove(InputAction.CallbackContext ctx) => _movementInput = ctx.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext ctx) => jumpPressed = ctx.ReadValueAsButton();
    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { 
            ToggleSprint(!_sprintActive);
        }
    }
    public void OnCrouch(InputAction.CallbackContext ctx) => _crouchActive = ctx.ReadValueAsButton();
    public void ToggleSprint(bool value)
    {
        _sprintActive = value;
        _playerVCamController.SetTargetFOV(_sprintActive ? 100 : 80);
    }
    public void ToggleCrouch(bool value)
    {
        _crouchActive = value;
    }

    public void SetState(IPlayerState playerState)
    {
        if (playerState != null)
        {
            _currentState.Exit();
            _currentState = playerState;
            _currentState.Enter(this);
        }
    }

    public bool OnSlope ()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.6f, WhatIsGround);
        transform.rotation = CameraForward;
        _currentState.HandleInput();

        Ray ray = new(_playerCursor.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _maxInteractDistance, _whatIsInteractable))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                OnSetFocus?.Invoke(interactable);
            }
            else
                OnSetFocus?.Invoke(null);
        }
        else
            OnSetFocus?.Invoke(null);
    }
}
