using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionReference;
    [SerializeField] GameObject pauseMenuObj;
    bool pauseMenuVisible = false;
    bool pauseLocked = false;
    public delegate void PauseMenuAppear(bool value);
    public static event PauseMenuAppear OnPauseMenu;

    private void OnEnable()
    {
        _actionReference.action.performed += Pause;
        PlayerEntity.OnPlayerDeath += LockPause;
    }
    private void OnDisable()
    {
        _actionReference.action.performed -= Pause;
        PlayerEntity.OnPlayerDeath -= LockPause;
    }

    private void Pause (InputAction.CallbackContext ctx)
    {
        if (pauseLocked) return;
        pauseMenuVisible = !pauseMenuVisible;
        OnPauseMenu?.Invoke(pauseMenuVisible);
        GameManager.instance.Pause(pauseMenuVisible);
        pauseMenuObj.SetActive(pauseMenuVisible);
        Cursor.lockState = pauseMenuVisible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseMenuVisible;
    }
    void LockPause ()
    {
        pauseLocked = true;
    }
}
