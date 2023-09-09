using PlayerStates;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private Transform _vCamTransform;
    [SerializeField] private float _playerSpeed = 100f;
    [SerializeField] private float _strafeModifier = 0.6f;
    [SerializeField] private float _sprintSpeed = 200f;

    private Vector2 _movementInput;

    public float PlayerSpeed { get { return _playerSpeed; } }
    public float StrafeModifier { get { return _strafeModifier; } }
    public float SprintSpeed { get { return _sprintSpeed; } }
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
        transform.rotation = CameraForward;
        _currentState.HandleInput();
    }
}
